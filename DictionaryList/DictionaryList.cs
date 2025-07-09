using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Vectorial1024.Collections.Generic
{
    /// <summary>
    /// Represents a strongly-typed list of objects that has a dictionary-like structure.
    /// Elements can be accessed by an index which is ordered but not guaranteed to be continuous.
    /// </summary>
    public class DictionaryList<TValue> : IEnumerable<KeyValuePair<int,TValue>>
    {
        // the version of the DictionaryList, to ensure the enumerator can work correctly
        private int _version;

        #region Backing Storage

        private static readonly TValue[] EmptyArray = Array.Empty<TValue>();

        private static readonly bool[] EmptyLookupArray = Array.Empty<bool>();

        private const int DefaultCapacity = 4;

        /// <summary>
        /// Actual usable capacity of the backing storage. Indexes smaller than this value are usable.
        /// <para/>
        /// Value increased by Add(); value decreased by CompactAndTrimExcess().
        /// </summary>
        internal int _size;

        internal int _actualCount;

        /// <summary>
        /// The backing storage of this DictionaryList.
        /// </summary>
        internal TValue[] _items;

        /// <summary>
        /// The index existence lookup array of this DictionaryList.
        /// </summary>
        internal bool[] _issetLookup;

        #endregion

        /// <summary>
        /// Initializes a new instance of the `DictionaryList&lt;TValue&gt;` class that is empty and has the default capacity.
        /// </summary>
        public DictionaryList()
        {
            _items = EmptyArray;
            _issetLookup = EmptyLookupArray;
        }

        /// <summary>
        /// Initializes a new instance of the `DictionaryList&lt;TValue&gt;` class that contains elements copied from the
        /// specified collection with enough capacity to hold all such elements.
        /// </summary>
        /// <param name="collection">The collection to take copies from.</param>
        public DictionaryList(DictionaryList<TValue> collection)
        {
            _items = (TValue[]) collection._items.Clone();
            _actualCount = collection._actualCount;
            _issetLookup = (bool[]) collection._issetLookup.Clone();
            _size = collection._size;
        }

        /// <summary>
        /// Initializes a new instance of the `DictionaryList&lt;TValue&gt;` class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity"></param>
        public DictionaryList(int capacity)
        {
            _items = new TValue[capacity];
            _issetLookup = new bool[capacity];
        }

        /// <summary>
        /// Gets the number of elements contained in this DictionaryList.
        /// <para/>
        /// Indexes that are unset are not counted.
        /// </summary>
        public int Count => _actualCount;

        /// <summary>
        /// Gets the total number of elements the internal data structure can hold without resizing.
        /// <para/>
        /// Indexes that are unset are still counted towards the usage of the internal data structure.
        /// </summary>
        /// <seealso cref="CompactAndTrimExcess"/>
        public int Capacity => _size;

        public TValue this[int index]
        {
            get
            {
                if ((uint)index >= (uint)_size)
                {
                    // out of range!
                    ThrowHelper.ThrowArgumentOutOfRangeException(index);
                }
                // definitely in range
                if (!IndexIsSet(index))
                {
                    throw new KeyNotFoundException($"The given index {index} was unset in the list.");
                }
                return _items[index];
            }
            set
            {
                if ((uint)index >= (uint)_size)
                {
                    // out of range!
                    ThrowHelper.ThrowArgumentOutOfRangeException(index);
                }
                if (!IndexIsSet(index))
                {
                    // adding items during iteration is not allowed!
                    _version++;
                }
                _items[index] = value;
                _issetLookup[index] = true;
            }
        }

        /// <summary>
        /// Adds an object to the end of the DictionaryList.
        /// </summary>
        /// <param name="value">The object to be added.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(TValue value)
        {
            var nextIndex = _size;
            if (nextIndex == _items.Length)
            {
                // we need a larger backing storage
                GrowBackingStorage();
            }
            _items[nextIndex] = value;
            _issetLookup[nextIndex] = true;
            _actualCount++;
            _size++;
            _version++;
        }

        private void GrowBackingStorage()
        {
            // what should be our next array length?
            var nextLength = _items.Length == 0 ? DefaultCapacity : _items.Length * 2;
            // then, grow it
            Array.Resize(ref _items, nextLength);
            Array.Resize(ref _issetLookup, nextLength);
        }

        /// <summary>
        /// Unsets and removes the element at the specified index of the DictionaryList.
        /// <para/>
        /// This leaves behind unused memory, which may be reclaimed by compacting the DictionaryList.
        /// </summary>
        /// <param name="index">The index at which to remove an item.</param>
        /// <seealso cref="CompactAndTrimExcess"/>
        public void UnsetAt(int index)
        {
            if ((uint)index >= (uint)_size)
            {
                ThrowHelper.ThrowArgumentOutOfRangeException(index);
            }
            if (!IndexIsSet(index))
            {
                return;
            }

            _items[index] = default!;
            _actualCount--;
            _issetLookup[index] = false;
        }

        /// <summary>
        /// Determines whether the DictionaryList contains the specified index.
        /// </summary>
        /// <param name="index">The index to check existence for.</param>
        /// <returns>Returns true if the index is in use by an element; false otherwise (e.g. unused memory left behind by RemoveAt())</returns>
        public bool ContainsIndex(int index)
        {
            if ((uint)index >= (uint)_size)
            {
                return false;
            }
            // we might have it
            return IndexIsSet(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool IndexIsSet(int index)
        {
            // does not check for index range, for internal use where we are very sure it does not cause range overflow
            // we force-inline it to waive this method call
            return _issetLookup[index];
        }

        #region CompactAndTrim

        /// <summary>
        /// Removes all elements from the DictionaryList.
        /// </summary>
        public void Clear()
        {
            _version++;
            var oldSize = _size;
            if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
            {
                // Clear the elements so that the gc can reclaim the references.
                _size = 0;
                if (oldSize > 0)
                {
                    Array.Clear(_items, 0, oldSize);
                }
            }
            else
            {
                _size = 0;
            }
            _actualCount = 0;
            Array.Clear(_issetLookup, 0, oldSize);
        }

        /// <summary>
        /// Re-indexes all elements of the DictionaryList sequentially from 0,
        /// and sets the capacity to the actual number of elements in the DictionaryList,
        /// if that number is less than a threshold value.
        /// </summary>
        public void CompactAndTrimExcess()
        {
            _version++;
            if (_actualCount == 0)
            {
                // somehow reset this
                _items = EmptyArray;
                _issetLookup = EmptyLookupArray;
                _size = 0;
                return;
            }

            // prepare the new backing storage
            var newItems = new TValue[_actualCount];
            var newIndex = 0;
            for (var index = 0; index < _size; index++)
            {
                if (!IndexIsSet(index))
                {
                    // this is an empty slot; skip
                    continue;
                }
                // this has items; move it over!
                newItems[newIndex] = _items[index];
                newIndex++;
                if (newIndex == _actualCount)
                {
                    // we have moved everything; early finish!
                    break;
                }
            }

            _items = newItems;
            _issetLookup = Enumerable.Repeat(true, _actualCount).ToArray();
            _size = _actualCount;
        }

        #endregion

        #region Enumeration

        internal struct DictionaryListEnumerator : IEnumerator<KeyValuePair<int, TValue>>, IDictionaryEnumerator
        {
            private readonly DictionaryList<TValue> _dictList;
            private int _index;
            private KeyValuePair<int, TValue> _current;
            private readonly int _version;

            internal DictionaryListEnumerator(DictionaryList<TValue> dictList)
            {
                _index = 0;
                _dictList = dictList;
                _current = default;
                _version = dictList._version;
            }

            public bool MoveNext()
            {
                var iterIndex = _index;
                var theDictList = _dictList;
                while (_version == theDictList._version && (uint)iterIndex < (uint)theDictList._size)
                {
                    if (!theDictList.IndexIsSet(iterIndex))
                    {
                        // not set; find the next one!
                        iterIndex++;
                        continue;
                    }
                    _current = new KeyValuePair<int, TValue>(iterIndex, theDictList[iterIndex]);
                    _index = iterIndex + 1;
                    return true;
                }

                if (_version != _dictList._version)
                {
                    // DictionaryList was modified during enumeration; not allowed!
                    ThrowHelper.ThrowBadEnumerationException();
                }
                // end of list
                _current = default;
                return false;
            }

            public void Reset()
            {
                if (_version != _dictList._version)
                {
                    // DictionaryList was modified during enumeration; not allowed!
                    ThrowHelper.ThrowBadEnumerationException();
                }
                _index = 0;
                _current = default;
            }

            KeyValuePair<int, TValue> IEnumerator<KeyValuePair<int, TValue>>.Current => _current;

            object? IEnumerator.Current => _current;

            public void Dispose()
            {
            }

            public DictionaryEntry Entry => new DictionaryEntry(_current.Key, _current.Value);
            public object Key => _current.Key;
            public object? Value => _current.Value;
        }

        public IEnumerator<KeyValuePair<int,TValue>> GetEnumerator() => new DictionaryListEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
