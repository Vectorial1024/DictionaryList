using System;
using System.Collections.Generic;

namespace DictionaryList
{
    /// <summary>
    /// Represents a strongly-typed list of objects that has a dictionary-like structure.
    /// Elements can be accessed by an index which is ordered but not guaranteed to be continuous.
    /// </summary>
    public class DictionaryList<TValue>
    {
        internal struct DataBox<TData>
        {
            internal TData Value;

            internal DataBox(TData value)
            {
                Value = value;
            }
        }

        internal List<DataBox<TValue>> _list = new List<DataBox<TValue>>();

        internal int _actualCount;

        /// <summary>
        /// Initializes a new instance of the `DictionaryList&lt;TValue&gt;` class that is empty and has the default capacity.
        /// </summary>
        public DictionaryList()
        {

        }

        /// <summary>
        /// Returns the number of active elements in this DictionaryList.
        /// </summary>
        public int Count => _actualCount;
    }
}