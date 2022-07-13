using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DoubleLinkedListAssigment
{
    internal class StackArray<T> : IEnumerable<T>
    {
        private int index = 0;
        private readonly T[] arr;

        public StackArray(int size) => arr = new T[size];

        public bool Push(T item)
        {
            if (IsFull()) return false;
            arr[index++] = item;
            return true;
        }

        public bool Pop(out T removedItem)
        {
            removedItem = default;
            if (IsEmpty()) return false;
            removedItem = arr[--index];
            return true;
        }

        public bool Peek(out T topItem)
        {
            topItem = default;
            if (IsEmpty()) return false;
            topItem = arr[index - 1];
            return true;
        }

        public bool IsFull() => index == arr.Length;

        public bool IsEmpty() => index == 0;

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            int i = index;
            while (i != 0) { str.Append($"{arr[--i]}, "); }
            return str.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            int i = 0;
            while (i != index) { yield return arr[i++]; }
        }

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
