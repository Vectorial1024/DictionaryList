using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Vectorial1024.Collections.Generic
{
    /// <summary>
    /// Represents a strongly-typed list of objects that has a dictionary-like structure.
    /// Elements can be accessed by an index which is ordered but not guaranteed to be continuous.
    /// </summary>
    public class DictionaryList<TValue> : IEnumerable<KeyValuePair<int,TValue>>
    {
        /// <summary>
        /// The data-box for storing DictionaryList elements. This is to distinguish between a genuine null and a "no data" entry.
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        internal struct DataBox<TData>
        {
            internal TData Value;

            internal DataBox(TData value)
            {
                Value = value;
            }
        }

        internal List<DataBox<TValue>?> _list = new List<DataBox<TValue>?>();

        internal int _actualCount;

        // the version of the DictionaryList, to ensure the enumerator can work correctly
        private int _version;

        /// <summary>
        /// Initializes a new instance of the `DictionaryList&lt;TValue&gt;` class that is empty and has the default capacity.
        /// </summary>
        public DictionaryList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the `DictionaryList&lt;TValue&gt;` class that contains elements copied from the
        /// specified collection with enough capacity to hold all such elements.
        /// </summary>
        /// <param name="collection">The collection to take copies from.</param>
        public DictionaryList(DictionaryList<TValue> collection)
        {
            _list = new List<DataBox<TValue>?>(collection._list);
            _actualCount = collection._actualCount;
        }

        /// <summary>
        /// Initializes a new instance of the `DictionaryList&lt;TValue&gt;` class that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity"></param>
        public DictionaryList(int capacity)
        {
            _list = new List<DataBox<TValue>?>(capacity);
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
        public int Capacity => _list.Capacity;

        public TValue this[int index]
        {
            get
            {
                var item = _list[index];
                if (item == null)
                {
                    throw new KeyNotFoundException($"The given index {index} was unset in the list.");
                }
                return item.Value.Value;
            }
            set
            {
                _list[index] = new DataBox<TValue>(value);
            }
        }

        /// <summary>
        /// Adds an object to the end of the DictionaryList.
        /// </summary>
        /// <param name="value">The object to be added.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(TValue value)
        {
            _list.Add(new DataBox<TValue>(value));
            _actualCount++;
            _version++;
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
            var box = _list[index];
            if (box == null)
            {
                return;
            }

            _list[index] = null;
            _actualCount--;
        }

        /// <summary>
        /// Determines whether the DictionaryList contains the specified index.
        /// </summary>
        /// <param name="index">The index to check existence for.</param>
        /// <returns>Returns true if the index is in use by an element; false otherwise (e.g. unused memory left behind by RemoveAt())</returns>
        public bool ContainsIndex(int index)
        {
            if (index < 0)
            {
                return false;
            }

            if (index > _list.Count)
            {
                return false;
            }
            // we might have it
            return _list[index].HasValue;
        }

        #region CompactAndTrim

        /// <summary>
        /// Removes all elements from the DictionaryList.
        /// </summary>
        public void Clear()
        {
            _list.Clear();
            _actualCount = 0;
        }

        /// <summary>
        /// Re-indexes all elements of the DictionaryList sequentially from 0,
        /// and sets the capacity to the actual number of elements in the DictionaryList,
        /// if that number is less than a threshold value.
        /// </summary>
        public void CompactAndTrimExcess()
        {
            var newList = new List<DataBox<TValue>?>(_actualCount);
            foreach (var item in _list)
            {
                if (item == null)
                {
                    continue;
                }
                newList.Add(item);
            }
            _list = newList;
            _actualCount = newList.Count;
            _version++;
        }

        #endregion

        #region Enumeration

        internal struct DictionaryListEnumerator : IEnumerator<KeyValuePair<int, TValue>>, IDictionaryEnumerator
        {
            private readonly DictionaryList<TValue> _dictList;
            private int _index;
            private KeyValuePair<int, TValue> _current;
            private readonly int _size;
            private readonly int _version;

            internal DictionaryListEnumerator(DictionaryList<TValue> dictList)
            {
                _index = 0;
                _dictList = dictList;
                _current = default;
                _size = _dictList._list.Count;
                _version = dictList._version;
            }

            public bool MoveNext()
            {
                var iterIndex = _index;
                var theDictList = _dictList;
                while (_version == theDictList._version && (uint)iterIndex < (uint)_size)
                {
                    if (!theDictList.ContainsIndex(iterIndex))
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
