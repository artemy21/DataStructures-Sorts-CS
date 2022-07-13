using System;
using System.Collections;
using System.Collections.Generic;

namespace DoubleLinkedListAssigment
{
    internal class BinarySearchTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node root;

        private class Node : IEnumerable<T>
        {
            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node(T value) => Value = value;

            public IEnumerator<T> GetEnumerator()
            {
                if (Left != null)
                {
                    foreach (var node in Left) yield return node;
                }
                yield return Value;
                if (Right != null)
                {
                    foreach (var node in Right) yield return node;
                }
            }
            IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
        }

        public void Add(T value)
        {
            if (root == null)
            {
                root = new Node(value);
                return;
            }

            Node prev = null;
            Node curr = root;
            while (curr != null)
            {
                prev = curr;
                if (value.CompareTo(curr.Value) < 0) curr = curr.Left;
                else curr = curr.Right;
            }
            if (value.CompareTo(prev.Value) < 0) prev.Left = new Node(value);
            else prev.Right = new Node(value);
        }

        public int GetTreeHeight() => GetTreeHeight(root);
        private int GetTreeHeight(Node n)
        {
            if (n == null) return 0;
            int leftHeight = GetTreeHeight(n.Left);
            int rightHeight = GetTreeHeight(n.Right);
            return Math.Max(leftHeight, rightHeight) + 1;
        }

        public void ScanPreOrder(Action<T> act) => ScanPreOrder(root, act);
        private void ScanPreOrder(Node n, Action<T> act)
        {
            if (n == null) return;
            act(n.Value);
            ScanPreOrder(n.Left, act);
            ScanPreOrder(n.Right, act);
        }

        public void ScanInOrder(Action<T> act) => ScanInOrder(root, act);
        private void ScanInOrder(Node n, Action<T> act)
        {
            if (n == null) return;
            ScanInOrder(n.Left, act);
            act(n.Value);
            ScanInOrder(n.Right, act);
        }

        public void ScanPostOrder(Action<T> act) => ScanPostOrder(root, act);
        private void ScanPostOrder(Node n, Action<T> act)
        {
            if (n == null) return;
            ScanPostOrder(n.Left, act);
            ScanPostOrder(n.Right, act);
            act(n.Value);
        }

        public bool SearchRecursive(T value, out T item) => SearchRecursive(root, value, out item);
        private bool SearchRecursive(Node node, T value, out T item)
        {
            item = default;
            if (node == null) return false;

            if (value.CompareTo(node.Value) == 0)
            {
                item = node.Value;
                return true;
            }

            if (value.CompareTo(node.Value) < 0) return SearchRecursive(node.Left, value, out item);
            else return SearchRecursive(node.Right, value, out item);
        }

        public bool SearchIterative(T value, out T item)
        {
            item = default;
            Node tmp = root;
            while (tmp != null)
            {
                if (value.CompareTo(tmp.Value) < 0) tmp = tmp.Left;
                else if (value.CompareTo(tmp.Value) > 0) tmp = tmp.Right;
                else
                {
                    item = tmp.Value;
                    return true;
                }
            }
            return false;
        }
        private bool SearchIterative(T value, out Node node, out Node prev)
        {
            node = prev = null;
            Node tmp = root;
            while (tmp != null)
            {
                prev = tmp;
                if (value.CompareTo(tmp.Value) < 0) tmp = tmp.Left;
                else if (value.CompareTo(tmp.Value) > 0) tmp = tmp.Right;
                else
                {
                    node = tmp;
                    return true;
                }
            }
            return false;
        }

        public bool DeleteItem(T value)
        {
            if (!SearchIterative(value, out Node curr, out Node prev)) return false;

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
                curr.Value = tmp.Value;
            }
            return true;
        }

        public void AllGreaterValues(T value, Action<T> act) => AllGreaterValues(value, act, root);
        private void AllGreaterValues(T value, Action<T> act, Node n)
        {
            if (n == null) return;
            if (n.Value.CompareTo(value) > 0)
            {
                AllGreaterValues(value, act, n.Left);
                act(n.Value);
            }
            AllGreaterValues(value, act, n.Right);
        }

        public int DistanceBetweenValues(T value1, T value2)
        {
            Node lca = GetLowestCommonAncestorIterative(value1, value2);
            bool isVal1Exist = DistanceFromNode(lca, value1, out int val1Height);
            bool isVal2Exist = DistanceFromNode(lca, value2, out int val2Height);
            return isVal1Exist && isVal2Exist ? val1Height + val2Height : -1;
        }

        private bool DistanceFromNode(Node rootNode, T value, out int distance)
        {
            distance = 0;
            while (rootNode != null)
            {
                if (value.CompareTo(rootNode.Value) < 0) rootNode = rootNode.Left;
                else if (value.CompareTo(rootNode.Value) > 0) rootNode = rootNode.Right;
                else return true;
                distance++;
            }
            return false;
        }
        private int DistanceFromRootIterative(Node node)
        {
            Node tmp = root;
            int h = 0;
            while (tmp != null)
            {
                if (tmp == node) return h;
                if (node.Value.CompareTo(tmp.Value) < 0) tmp = tmp.Left;
                else tmp = tmp.Right;
                h++;
            }
            return -1;
        }
        private int DistanceFromRootRecursive(Node rootNode, Node node)
        {
            if (rootNode == node) return 0;
            if (node.Value.CompareTo(rootNode.Value) < 0)
                return DistanceFromRootRecursive(rootNode.Left, node) + 1;
            else
                return DistanceFromRootRecursive(rootNode.Right, node) + 1;
        }

        private Node GetLowestCommonAncestorRecursive(Node n1, Node n2, Node node)
        {
            if (node == null) return null;
            if (node == n1 || node == n2) return node;

            Node left_lca = GetLowestCommonAncestorRecursive(n1, n2, node.Left);
            Node right_lca = GetLowestCommonAncestorRecursive(n1, n2, node.Right);

            if (left_lca != null && right_lca != null) return node;
            return left_lca ?? right_lca;
        }
        private Node GetLowestCommonAncestorIterative(T value1, T value2)
        {
            Node lca = root;
            while (true)
            {
                if (value1.CompareTo(lca.Value) < 0 && value2.CompareTo(lca.Value) < 0)
                    lca = lca.Left;
                else if (value1.CompareTo(lca.Value) > 0 && value2.CompareTo(lca.Value) > 0)
                    lca = lca.Right;
                else return lca;
            }
        }

        public bool GetInOrderSuccesor(T value, out T succesor)
        {
            succesor = default;
            Node node = GetInOrderSuccesor(value);
            if (node == null) return false;
            succesor = node.Value;
            return true;
        }
        private Node GetInOrderSuccesor(T value)
        {
            Node curr = root, nextInOrder = root;
            while (curr != null)
            {
                if (value.CompareTo(curr.Value) < 0)
                {
                    if (curr.Value.CompareTo(nextInOrder.Value) < 0 || nextInOrder.Value.CompareTo(value) < 0)
                        nextInOrder = curr;
                    curr = curr.Left;
                }
                else if (value.CompareTo(curr.Value) > 0) curr = curr.Right;
                else return curr;
            }
            return nextInOrder?.Value.CompareTo(value) < 0 ? null : nextInOrder;
        }

        public IEnumerator<T> GetEnumerator() => root.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
