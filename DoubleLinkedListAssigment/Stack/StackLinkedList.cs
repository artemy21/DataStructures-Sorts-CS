using System;
using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkedListAssigment
{
    internal class StackLinkedList<T> : IEnumerable<T>
    {
        private readonly DoubleLinkedList<T> list;

        public StackLinkedList() => list = new DoubleLinkedList<T>();

        public void Push(T item) => list.AddLast(item);

        public bool Pop(out T removedItem) => list.RemoveLast(out removedItem);

        public bool Peek(out T topItem) => list.GetLastItem(out topItem);

        public bool IsEmpty() => list.IsEmpty();

        public override string ToString() => list.ToStringReverse();

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
