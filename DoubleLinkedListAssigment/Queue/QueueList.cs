using System;
using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkedListAssigment
{
    internal class QueueList<T> : IEnumerable<T>
    {
        private readonly DoubleLinkedList<T> list;

        public QueueList() => list = new DoubleLinkedList<T>();

        public bool IsEmpty() => list.IsEmpty();

        public void EnQueue(T item) => list.AddLast(item);

        public bool DeQueue(out T removedItem) => list.RemoveFirst(out removedItem);

        public bool Peek(out T value) => list.GetAt(0, out value);

        public override string ToString() => list.ToString();

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
