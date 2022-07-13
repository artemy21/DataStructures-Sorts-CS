using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DoubleLinkedListAssigment
{
    internal class QueueArray<T> : IEnumerable<T>
    {
        private readonly T[] arr;
        private int firstInd = -1;
        private int lastInd = -1;

        public QueueArray(int capacity) => arr = new T[capacity];

        public bool IsFull() => firstInd == lastInd && firstInd != -1;

        public bool IsEmpty() => firstInd == -1;

        public bool EnQueue(T item)
        {
            if (IsFull()) return false;
            if (IsEmpty()) lastInd = firstInd = 0;
            arr[lastInd] = item;
            lastInd = (lastInd + 1) % arr.Length;
            return true;
        }

        public bool DeQueue(out T removedItem)
        {
            removedItem = default;
            if (IsEmpty()) return false;
            removedItem = arr[firstInd];
            firstInd = (firstInd + 1) % arr.Length;
            if (firstInd == lastInd) firstInd = lastInd = -1;
            return true;
        }

        public override string ToString()
        {
            if (IsEmpty()) return "";
            StringBuilder str = new StringBuilder();
            int i = firstInd;
            do
            {
                str.Append($"{arr[i]}, ");
                i = (i + 1) % arr.Length;
            }
            while (i != lastInd);
            return str.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty()) yield break;
            int i = firstInd;
            do
            {
                yield return arr[i];
                i = (i + 1) % arr.Length;
            }
            while (i != lastInd);
        }

        IEnumerator IEnumerable.GetEnumerator() => throw new System.NotImplementedException();
    }
}
