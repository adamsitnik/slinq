using System.Runtime.CompilerServices;

namespace Slinq.Utils
{
    /// <summary>
    /// the code is an micro optimized version of C++ IntroSort that is invoked from Array.Sort via extern mechanism
    /// </summary>
    public static class IntsArraySorter
    {
        internal static void IntrospectiveSort(int[] keys, int indexFrom, int indexTo)
        {
            int length = indexTo - indexFrom + 1;

            if (length < 2)
            {
                return;
            }

            IntroSort(keys, indexFrom, indexTo, 2 * ArraySorterHelper.FloorLog2(length));
        }

        private static void IntroSort(int[] keys, int lo, int hi, int depthLimit)
        {
            while (hi > lo)
            {
                int partitionSize = hi - lo + 1;
                if (partitionSize <= ArraySorterHelper.IntrosortSizeThreshold)
                {
                    if (partitionSize == 1)
                    {
                        return;
                    }

                    if (partitionSize == 2)
                    {
                        if (lo != hi)
                        {
                            SwapIfGreaterWithItems(keys, lo, hi);
                        }

                        return;
                    }

                    if (partitionSize == 3)
                    {
                        SwapIfGreaterWithItems(keys, lo, hi - 1);

                        if (lo != hi)
                        {
                            SwapIfGreaterWithItems(keys, lo, hi);
                        }

                        SwapIfGreaterWithItems(keys, hi - 1, hi);
                        return;
                    }

                    InsertionSort(keys, lo, hi);
                    return;
                }

                if (depthLimit == 0)
                {
                    Heapsort(keys, lo, hi);
                    return;
                }

                depthLimit--;

                int p = PickPivotAndPartition(keys, lo, hi);
                IntroSort(keys, p + 1, hi, depthLimit);
                hi = p - 1;
            }
        }

        private static int PickPivotAndPartition(int[] keys, int lo, int hi)
        {
            // Compute median-of-three.  But also partition them, since we've done the comparison.
            int median = lo + ((hi - lo) / 2);

            //// Sort lo, mid and hi appropriately, then pick mid as the pivot.

            if (lo != median)
            {
                SwapIfGreaterWithItems(keys, lo, median);
            }

            if (lo != hi)
            {
                SwapIfGreaterWithItems(keys, lo, hi);
            }

            if (median != hi)
            {
                SwapIfGreaterWithItems(keys, median, hi);
            }

            int pivot = keys[median];
            ArraySorterHelper.Swap(keys, median, hi - 1);

            int left = lo, right = hi - 1;

            // We already partitioned lo and hi and put the pivot in hi - 1.  And we pre-increment & decrement below.
            while (left < right)
            {
                while (left < (hi - 1) && keys[++left] < pivot)
                {
                }

                while (right > lo && pivot < keys[--right])
                {
                }

                if (left >= right)
                {
                    break;
                }

                ArraySorterHelper.Swap(keys, left, right);
            }

            // Put pivot in the right location.
            ArraySorterHelper.Swap(keys, left, hi - 1);
            return left;
        }

        private static void Heapsort(int[] keys, int lo, int hi)
        {
            int n = hi - lo + 1;
            for (int i = n / 2; i >= 1; i = i - 1)
            {
                DownHeap(keys, i, n, lo);
            }

            for (int i = n; i > 1; i = i - 1)
            {
                ArraySorterHelper.Swap(keys, lo, lo + i - 1);
                DownHeap(keys, 1, i - 1, lo);
            }
        }

        private static void DownHeap(int[] keys, int i, int n, int lo)
        {
            int d = keys[lo + i - 1];
            int child;

            while (i <= n / 2)
            {
                child = 2 * i;
                if (child < n && keys[lo + child - 1] < keys[lo + child])
                {
                    child++;
                }

                if (!(d < keys[lo + child - 1]))
                {
                    break;
                }

                keys[lo + i - 1] = keys[lo + child - 1];

                i = child;
            }

            keys[lo + i - 1] = d;
        }

        private static void InsertionSort(int[] keys, int lo, int hi)
        {
            int i, j, t;
            for (i = lo; i < hi; i++)
            {
                j = i;
                t = keys[i + 1];
                while (j >= lo && t < keys[j])
                {
                    keys[j + 1] = keys[j];
                    j--;
                }

                keys[j + 1] = t;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SwapIfGreaterWithItems(int[] keys, int leftIndex, int rightIndex)
        {
            var leftKeyValue = keys[leftIndex];
            var rightKeyValue = keys[rightIndex];
            if (leftKeyValue > rightKeyValue)
            {
                keys[leftIndex] = rightKeyValue;
                keys[rightIndex] = leftKeyValue;
            }
        }
    }
}