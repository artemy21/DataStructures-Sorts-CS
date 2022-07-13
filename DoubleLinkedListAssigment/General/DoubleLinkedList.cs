using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkedListAssigment
{
    internal class DoubleLinkedList<T> : IEnumerable<T>
    {
        private Node first = null;
        private Node last = null;

        private class Node
        {
            public T Value;
            public Node Next;
            public Node Prev;
            public Node(T value)
            {
                Value = value;
                Next = null;
                Prev = null;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Node tmp = first;

            while (tmp != null)
            {
                sb.Append($"{tmp.Value}, ");
                tmp = tmp.Next;
            }
            return sb.ToString();
        }

        public string ToStringReverse()
        {
            StringBuilder sb = new StringBuilder();
            Node tmp = last;

            while (tmp != null)
            {
                sb.Append($"{tmp.Value}, ");
                tmp = tmp.Prev;
            }
            return sb.ToString();
        }

        public void AddFirst(T value)
        {
            Node node = new Node(value);
            if (first == null)
            {
                first = node;
                last = node;
            }
            else
            {
                node.Next = first;
                first.Prev = node;
                first = node;
            }
        }

        public void AddLast(T value)
        {
            if (first == null) AddFirst(value);
            else
            {
                Node node = new Node(value);
                last.Next = node;
                node.Prev = last;
                last = node;
            }
        }

        public bool RemoveFirst(out T removedValue)
        {
            removedValue = default;
            if (first == null) return false;
            removedValue = first.Value;
            first = first.Next;
            if (first == null) last = null;
            else first.Prev = null;
            return true;
        }

        public bool RemoveLast(out T removedValue)
        {
            removedValue = default;
            if (first == null) return false;
            removedValue = last.Value;
            last = last.Prev;
            if (last == null) first = null;
            else last.Next = null;
            return true;
        }

        public bool GetAt(int position, out T value)
        {
            value = default;
            if (first == null) return false;

            int i = 0;
            Node tmp = first;
            while (tmp != null)
            {
                if (i == position)
                {
                    value = tmp.Value;
                    return true;
                }
                tmp = tmp.Next;
                i++;
            }
            return false;
        }

        public bool AddAt(int position, T value)
        {
            int i = 0;
            Node tmp = first;
            while (i != position)
            {
                if (tmp == null) return false;
                tmp = tmp.Next;
                i++;
            }

            if (tmp == null) AddLast(value);
            else if (tmp.Prev == null) AddFirst(value);
            else
            {
                Node node = new Node(value);
                node.Next = tmp;
                node.Prev = tmp.Prev;
                tmp.Prev.Next = node;
                tmp.Prev = node;
            }
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node tmp = first;
            while (tmp != null)
            {
                yield return tmp.Value;
                tmp = tmp.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => throw new System.NotImplementedException();

        public bool IsEmpty() => first == null;

        public bool GetLastItem(out T value)
        {
            value = default;
            if (IsEmpty()) return false;
            value = last.Value;
            return true;
        }

        public bool DeleteItem(T item)
        {
            if (!IsExist(item, out Node tmp)) return false;
            if (tmp.Prev == null) RemoveFirst(out _);
            else if (tmp.Next == null) RemoveLast(out _);
            else
            {
                tmp.Prev.Next = tmp.Next;
                tmp.Next.Prev = tmp.Prev;
            }
            return true;
        }

        public bool IsExist(T item) => IsExist(item, out _);
        private bool IsExist(T item, out Node node)
        {
            node = first;
            while (node != null && !node.Value.Equals(item)) node = node.Next;
            if (node == null) return false;
            return true;
        }

        public void MoveToFirst(T item)
        {
            if (!IsExist(item, out Node node) || node == first) return;
            DeleteItem(item);
            AddFirst(item);
        }

        public void ReverseList()
        {
            Node prev = null;
            Node curr = first;
            Node next;

            while (curr != null)
            {
                next = curr.Next;
                curr.Next = prev;
                prev = curr;
                curr = next;
            }

            last = first;
            first = prev;
        }
    }
}
