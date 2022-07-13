using System;
using System.Collections.Generic;
using System.IO;

namespace DoubleLinkedListAssigment.General
{
    internal class FuncAndRecursions
    {
        public static bool IsBracketsBalanced(string str)
        {
            StackLinkedList<char> stack = new StackLinkedList<char>();
            foreach (char c in str)
            {
                if (IsOpeningBracket(c)) stack.Push(c);
                else if (IsClosingBracket(c))
                {
                    if (!stack.Peek(out char topChar) || !IsBracketsMatching(topChar, c)) return false;
                    stack.Pop(out _);
                }
            }
            return stack.IsEmpty();
        }
        private static bool IsOpeningBracket(char c) => c == '(' || c == '[' || c == '{' || c == '<';
        private static bool IsClosingBracket(char c) => c == ')' || c == ']' || c == '}' || c == '>';
        private static bool IsBracketsMatching(char c1, char c2)
        {
            switch (c1)
            {
                case '(': return c2 == ')';
                case '[': return c2 == ']';
                case '{': return c2 == '}';
                case '<': return c2 == '>';
            }
            return false;
        }

        public static void Partioning(int num) => Partioning(num, new List<int>());
        private static void Partioning(int num, List<int> list)
        {
            if (num == 0)
            {
                Console.WriteLine(string.Join("  ", list));
                return;
            }

            for (int i = 1; i <= num; i++)
            {
                list.Add(i);
                Partioning(num - i, list);
                list.RemoveAt(list.Count - 1);
            }
        }

        public static int Fibonacci(int num) => num <= 2 ? 1 : Fibonacci(num - 1) + Fibonacci(num - 2);

        public static void PrintDirs(DirectoryInfo root) => PrintDirs(root, 0);
        private static void PrintDirs(DirectoryInfo root, int level)
        {
            DirectoryInfo[] infos = root.GetDirectories();
            if (infos.Length == 0) return;
            else
            {
                foreach (DirectoryInfo info in infos)
                {
                    Console.WriteLine($"{new string(' ', level)}{info.Name}");
                    PrintDirs(info, level + 4);
                }
            }
        }

        public static int BinarySearchArray(int[] arr, int num) => BinarySearchArray(arr, num, 0, arr.Length - 1);
        private static int BinarySearchArray(int[] arr, int num, int start, int end)
        {
            if (arr == null || start == end) return start;
            int mid = (end + start) / 2;
            if (arr[mid] == num) return mid;
            else if (arr[mid] < num) return BinarySearchArray(arr, num, mid + 1, end);
            else return BinarySearchArray(arr, num, start, mid - 1); ;
        }
    }
}
