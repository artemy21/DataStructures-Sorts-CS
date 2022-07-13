using System;
using System.Collections.Generic;

namespace DoubleLinkedListAssigment
{
    internal class HashTableSC<TKey, TValue>
    {
        private LinkedList<Data>[] hashArray;
        private int itemsCount;
        private const int DEFAULT_SIZE = 10;
        private const float LOAD_FACTOR = 0.75f;
        private class Data
        {
            public TKey key;
            public TValue value;
            public Data(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }
        }

        public HashTableSC(int capacity)
        {
            if (capacity <= 0) capacity = DEFAULT_SIZE;
            hashArray = new LinkedList<Data>[capacity];
            itemsCount = 0;
        }

        private int HashFunction(TKey key) => key.GetHashCode() % hashArray.Length;

        public void Add(TKey key, TValue value)
        {
            if (IsKeyExists(key, out int index)) throw new ArgumentException("key already exists");
            if (hashArray[index] == null) hashArray[index] = new LinkedList<Data>();
            hashArray[index].AddFirst(new Data(key, value));
            itemsCount++;
            if (itemsCount > hashArray.Length * LOAD_FACTOR) ReHash();
        }

        public bool Delete(TKey keyToDelete, out TValue value)
        {
            value = default;
            if (IsKeyExists(keyToDelete, out int index))
            {
                foreach (var item in hashArray[index])
                {
                    if (item.key.Equals(keyToDelete))
                    {
                        hashArray[index].Remove(item);
                        return true;
                    }
                }
            }
            return false;
        }

        private bool IsKeyExists(TKey key, out int index)
        {
            index = HashFunction(key);
            foreach (Data d in hashArray[index])
            {
                if (d.key.Equals(key)) return true;
            }
            return false;
        }
        public bool IsKeyExists(TKey key) => IsKeyExists(key, out _);

        public bool GetValue(TKey key, out TValue value)
        {
            value = default;
            if (IsKeyExists(key, out int index)) return false;
            foreach (Data d in hashArray[index])
            {
                if (d.key.Equals(key))
                {
                    value = d.value;
                    return true;
                }
            }
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                GetValue(key, out TValue value);
                return value;
            }
        }

        private void ReHash()
        {
            var tmpArray = hashArray;
            hashArray = new LinkedList<Data>[hashArray.Length * 2];
            itemsCount = 0;
            foreach (var lst in tmpArray)
            {
                if (lst != null)
                {
                    foreach (Data data in lst) Add(data.key, data.value);
                }
            }
        }
    }
}
