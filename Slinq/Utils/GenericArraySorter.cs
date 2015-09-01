using System.Runtime.CompilerServices;
using Slinq.Abstract;

namespace Slinq.Utils
{
    /// <summary>
    /// the code is an micro optimized version of C++ IntroSort that is invoked from Array.Sort via extern mechanism
    /// </summary>
    public static class GenericArraySorter
    {
        internal static void IntrospectiveSort<T, TComparer>(T[] keys, int indexFrom, int indexTo, TComparer comparer)
            where T : struct 
            where TComparer : ICopyFreeComparer<T>
        {
            int length = indexTo - indexFrom + 1;

            if (length < 2)
            {
                return;
            }

            IntroSort(keys, indexFrom, indexTo, 2 * ArraySorterHelper.FloorLog2(length), comparer);
        }

        private static void IntroSort<T, TComparer>(T[] keys, int lo, int hi, int depthLimit, TComparer comparer)
            where T : struct
            where TComparer : ICopyFreeComparer<T>
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
                            SwapIfGreaterWithItems(keys, lo, hi, comparer);
                        }

                        return;
                    }

                    if (partitionSize == 3)
                    {
                        SwapIfGreaterWithItems(keys, lo, hi - 1, comparer);

                        if (lo != hi)
                        {
                            SwapIfGreaterWithItems(keys, lo, hi, comparer);
                        }

                        SwapIfGreaterWithItems(keys, hi - 1, hi, comparer);
                        return;
                    }

                    InsertionSort(keys, lo, hi, comparer);
                    return;
                }

                if (depthLimit == 0)
                {
                    Heapsort(keys, lo, hi, comparer);
                    return;
                }

                depthLimit--;

                int p = PickPivotAndPartition(keys, lo, hi, comparer);
                IntroSort(keys, p + 1, hi, depthLimit, comparer);
                hi = p - 1;
            }
        }

        private static int PickPivotAndPartition<T, TComparer>(T[] keys, int lo, int hi, TComparer comparer)
            where T : struct
            where TComparer : ICopyFreeComparer<T>
        {
            // Compute median-of-three.  But also partition them, since we've done the comparison.
            int median = lo + ((hi - lo) / 2);

            //// Sort lo, mid and hi appropriately, then pick mid as the pivot.

            if (lo != median)
            {
                SwapIfGreaterWithItems(keys, lo, median, comparer);
            }

            if (lo != hi)
            {
                SwapIfGreaterWithItems(keys, lo, hi, comparer);
            }

            if (median != hi)
            {
                SwapIfGreaterWithItems(keys, median, hi, comparer);
            }

            T pivot = keys[median];
            ArraySorterHelper.Swap(keys, median, hi - 1);

            int left = lo, right = hi - 1;

            // We already partitioned lo and hi and put the pivot in hi - 1.  And we pre-increment & decrement below.
            while (left < right)
            {
                while (left < (hi - 1) && comparer.Compare(ref keys[++left], ref pivot) < 0)
                {
                }

                while (right > lo && comparer.Compare(ref pivot, ref keys[--right]) < 0)
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

        private static void Heapsort<T, TComparer>(T[] keys, int lo, int hi, TComparer comparer)
            where T : struct
            where TComparer : ICopyFreeComparer<T>
        {
            int n = hi - lo + 1;
            for (int i = n / 2; i >= 1; i = i - 1)
            {
                DownHeap(keys, i, n, lo, comparer);
            }

            for (int i = n; i > 1; i = i - 1)
            {
                ArraySorterHelper.Swap(keys, lo, lo + i - 1);
                DownHeap(keys, 1, i - 1, lo, comparer);
            }
        }

        private static void DownHeap<T, TComparer>(T[] keys, int i, int n, int lo, TComparer comparer)
            where T : struct
            where TComparer : ICopyFreeComparer<T>
        {
            T d = keys[lo + i - 1];
            int child;

            while (i <= n / 2)
            {
                child = 2 * i;
                if (child < n && comparer.Compare(ref keys[lo + child - 1], ref keys[lo + child]) < 0)
                {
                    child++;
                }

                if (!(comparer.Compare(ref d, ref keys[lo + child - 1]) < 0))
                {
                    break;
                }

                keys[lo + i - 1] = keys[lo + child - 1];

                i = child;
            }

            keys[lo + i - 1] = d;
        }

        private static void InsertionSort<T, TComparer>(T[] keys, int lo, int hi, TComparer comparer)
            where T : struct
            where TComparer : ICopyFreeComparer<T>
        {
            int i, j;
            T t;
            for (i = lo; i < hi; i++)
            {
                j = i;
                t = keys[i + 1];
                while (j >= lo && comparer.Compare(ref t, ref keys[j]) < 0)
                {
                    keys[j + 1] = keys[j];
                    j--;
                }

                keys[j + 1] = t;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void SwapIfGreaterWithItems<T, TComparer>(T[] keys, int leftIndex, int rightIndex, TComparer comparer)
            where T : struct
            where TComparer : ICopyFreeComparer<T>
        {
            var leftKeyValue = keys[leftIndex];
            var rightKeyValue = keys[rightIndex];
            if (comparer.Compare(ref leftKeyValue, ref rightKeyValue) > 0)
            {
                keys[leftIndex] = rightKeyValue;
                keys[rightIndex] = leftKeyValue;
            }
        }
    }
}