using System;

namespace DoubleLinkedListAssigment
{
    internal class Sorts
    {
        public static void BubbleSort(int[] a)
        {
            int tmp;
            for (int i = a.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        tmp = a[j];
                        a[j] = a[j + 1];
                        a[j + 1] = tmp;
                    }
                }
            }
        }

        public static void MaxSort(int[] a)
        {
            int tmp, maxIndex;
            for (int i = a.Length - 1; i > 0; i--)
            {
                maxIndex = 0;
                for (int j = 1; j <= i; j++)
                {
                    if (a[j] > a[maxIndex]) maxIndex = j;
                }
                if (i != maxIndex)
                {
                    tmp = a[i];
                    a[i] = a[maxIndex];
                    a[maxIndex] = tmp;
                }
            }
        }

        public static void QuickSort(int[] a) => QuickSort(a, 0, a.Length - 1);
        private static void QuickSort(int[] a, int leftIndex, int rightIndex)
        {
            int pivotIndex;
            if (leftIndex < rightIndex)
            {
                pivotIndex = ReArrangeSection(a, leftIndex, rightIndex);
                QuickSort(a, leftIndex, pivotIndex - 1);
                QuickSort(a, pivotIndex + 1, rightIndex);
            }
        }
        private static int ReArrangeSection(int[] a, int leftIndex, int rightIndex)
        {
            Random rand = new Random();
            int randomInd = rand.Next(leftIndex, rightIndex + 1);
            Swap(a, randomInd, leftIndex);
            int pivotValue = a[leftIndex];
            while (true)
            {
                while (a[rightIndex] >= pivotValue && leftIndex != rightIndex) rightIndex--;
                if (leftIndex == rightIndex) return leftIndex;
                else Swap(a, leftIndex, rightIndex);

                while (a[leftIndex] <= pivotValue && leftIndex != rightIndex) leftIndex++;
                if (leftIndex == rightIndex) return leftIndex;
                else Swap(a, leftIndex, rightIndex);
            }
        }
        private static void Swap(int[] a, int ind1, int ind2) => (a[ind2], a[ind1]) = (a[ind1], a[ind2]);

        public static void MergeSort(int[] a) => MergeSort(a, 0, a.Length - 1);
        private static void MergeSort(int[] a, int leftIndex, int rightIndex)
        {
            int midIndex;
            if (leftIndex < rightIndex)
            {
                midIndex = (leftIndex + rightIndex) / 2;
                MergeSort(a, leftIndex, midIndex);
                MergeSort(a, midIndex + 1, rightIndex);
                Merge(a, leftIndex, midIndex, midIndex + 1, rightIndex);
            }
        }
        private static void Merge(int[] a, int leftStartIndex, int leftEndIndex, int rightStartIndex, int rightEndIndex)
        {
            int i, j, k;
            int[] tmp = new int[rightEndIndex - leftStartIndex + 1];
            for (i = leftStartIndex, j = rightStartIndex, k = 0; i <= leftEndIndex && j <= rightEndIndex; k++)
            {
                tmp[k] = a[i] < a[j] ? a[i++] : a[j++];
            }
            for (; i <= leftEndIndex; i++, k++) { tmp[k] = a[i]; }
            for (; j <= rightEndIndex; j++, k++) { tmp[k] = a[j]; }
            for (k = 0; k < tmp.Length; k++) { a[leftStartIndex + k] = tmp[k]; }
        }
    }
}
