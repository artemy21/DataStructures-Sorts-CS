using System;

namespace DoubleLinkedListAssigment.HashTables
{
    internal class DoubleHashing<TKey, TValue>
    {
        private Data[] hashArray;
        private int itemsCount;
        private const int DEFAULT_SIZE = 10;
        private const float LOAD_FACTOR = 0.75f;
        private class Data
        {
            public TKey key;
            public TValue value;
            public bool isDeleted;
            public Data(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
                isDeleted = false;
            }
        }

        public DoubleHashing(int capacity)
        {
            if (capacity <= 0) capacity = DEFAULT_SIZE;
            hashArray = new Data[FindClosestPrimeNumber(capacity)];
            itemsCount = 0;
        }

        private int FindClosestPrimeNumber(int num)
        {
            while (true)
            {
                bool isPrime = true;
                num++;
                int squaredNumber = (int)Math.Sqrt(num);
                for (int i = 2; i <= squaredNumber; i++)
                {
                    if (num % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime) return num;
            }
        }

        private int HashFunction(TKey key) => key.GetHashCode() % hashArray.Length;
        private int HashFunctionSecondary(TKey key) => key.GetHashCode() % (hashArray.Length - 2) + 1;

        public TValue this[TKey key]
        {
            get => IsKeyExists(key, out int index) ? hashArray[index].value : default;
            set { if (IsKeyExists(key, out int index)) hashArray[index].value = value; }
        }

        public void Add(TKey key, TValue value)
        {
            if (IsKeyExists(key, out int index)) throw new ArgumentException("key already exists");
            hashArray[index] = new Data(key, value);
            itemsCount++;
            if (itemsCount > hashArray.Length * LOAD_FACTOR) ReHash();
        }

        private bool IsKeyExists(TKey key, out int index)
        {
            index = HashFunction(key);
            while (hashArray[index] != null && (!hashArray[index].key.Equals(key) || hashArray[index].isDeleted))
            {
                if (hashArray[index].isDeleted) return false;
                index = (index + HashFunctionSecondary(key)) % hashArray.Length;
            }
            return hashArray[index] != null;
        }
        public bool IsKeyExists(TKey key) => IsKeyExists(key, out _);

        public bool Delete(TKey keyToDelete, out TValue value)
        {
            value = default;
            if (!IsKeyExists(keyToDelete, out int index)) return false;
            value = hashArray[index].value;
            hashArray[index].isDeleted = true;
            itemsCount--;
            return true;
        }

        private void ReHash()
        {
            var tmpArray = hashArray;
            int newSize = FindClosestPrimeNumber(hashArray.Length * 2);
            hashArray = new Data[newSize];
            itemsCount = 0;
            foreach (var data in tmpArray)
            {
                if (data != null && !data.isDeleted) Add(data.key, data.value);
            }
        }
    }
}

