using System;

namespace DoubleLinkedListAssigment.Trees
{
    internal class BSTKeyValue<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node root;

        private class Node
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (root == null)
            {
                root = new Node(key, value);
                return;
            }

            Node prev = null;
            Node curr = root;
            while (curr != null)
            {
                prev = curr;
                if (key.CompareTo(curr.Key) < 0) curr = curr.Left;
                else curr = curr.Right;
            }
            if (key.CompareTo(prev.Key) < 0) prev.Left = new Node(key, value);
            else prev.Right = new Node(key, value);
        }

        public bool Search(TKey key, out TValue item)
        {
            item = default;
            Node tmp = root;
            while (tmp != null)
            {
                if (key.CompareTo(tmp.Key) < 0) tmp = tmp.Left;
                else if (key.CompareTo(tmp.Key) > 0) tmp = tmp.Right;
                else
                {
                    item = tmp.Value;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteItem(TKey key)
        {
            Node curr = root, prev = null;

            //finding the node
            while (curr != null && curr.Key.CompareTo(key) != 0)
            {
                prev = curr;
                if (key.CompareTo(curr.Key) < 0) curr = curr.Left;
                else curr = curr.Right;
            }
            if (curr == null) return false;

            if (curr.Left == null || curr.Right == null)
            {
                Node newCurr;

                if (curr.Left == null) newCurr = curr.Right;
                else newCurr = curr.Left;

                if (prev == null) root = newCurr;
                else
                {
                    if (curr == prev.Left) prev.Left = newCurr;
                    else prev.Right = newCurr;
                }
            }
            else
            {
                Node tmp = curr.Right, tmpPrev = null;

                //find closest value equal or above
                while (tmp.Left != null)
                {
                    tmpPrev = tmp;
                    tmp = tmp.Left;
                }

                if (tmpPrev != null) tmpPrev.Left = tmp.Right;
                else curr.Right = tmp.Right;
                curr.Key = tmp.Key;
            }
            return true;
        }

        public bool GetInOrderSuccesor(TKey key, out TValue succesor)
        {
            succesor = default;
            Node node = GetInOrderSuccesor(key);
            if (node == null) return false;
            succesor = node.Value;
            return true;
        }
        private Node GetInOrderSuccesor(TKey key)
        {
            Node curr = root, minSuccNode = root;
            while (curr != null)
            {
                if (key.CompareTo(curr.Key) < 0)
                {
                    if (curr.Key.CompareTo(minSuccNode.Key) < 0 || minSuccNode.Key.CompareTo(key) < 0)
                        minSuccNode = curr;
                    curr = curr.Left;
                }
                else if (key.CompareTo(curr.Key) > 0) curr = curr.Right;
                else return curr;
            }
            return minSuccNode?.Key.CompareTo(key) < 0 ? null : minSuccNode;
        }

        public bool IsEmpty() => root == null;
    }
}
