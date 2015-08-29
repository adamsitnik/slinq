using System.Runtime.CompilerServices;

namespace Slinq.Utils
{
    /// <summary>
    /// the code is an micro optimized version of C++ IntroSort that is invoked from Array.Sort via extern mechanism
    /// </summary>
    public static class ArraySorter
    {
        private const int IntrosortSizeThreshold = 16;

        internal static void IntrospectiveSort(int[] keys, int indexFrom, int indexTo)
        {
            int length = indexTo - indexFrom + 1;

            if (length < 2)
            {
                return;
            }

            IntroSort(keys, indexFrom, indexTo, 2 * FloorLog2(length));
        }

        private static void IntroSort(int[] keys, int lo, int hi, int depthLimit)
        {
            while (hi > lo)
            {
                int partitionSize = hi - lo + 1;
                if (partitionSize <= IntrosortSizeThreshold)
                {
                    if (partitionSize == 1)
                    {
                        return;
                    }

                    if (partitionSize == 2)
                    {
                        if (lo != hi)
                        {
                            SwapIfGreaterWithItems(ref keys[lo], ref keys[hi]);
                        }

                        return;
                    }

                    if (partitionSize == 3)
                    {
                        SwapIfGreaterWithItems(ref keys[lo], ref keys[hi - 1]);

                        if (lo != hi)
                        {
                            SwapIfGreaterWithItems(ref keys[lo], ref keys[hi]);
                        }

                        SwapIfGreaterWithItems(ref keys[hi - 1], ref keys[hi]);
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int PickPivotAndPartition(int[] keys, int lo, int hi)
        {
            // Compute median-of-three.  But also partition them, since we've done the comparison.
            int median = lo + (hi - lo) / 2;

            // Sort lo, mid and hi appropriately, then pick mid as the pivot.

            if (lo != median)
            {
                SwapIfGreaterWithItems(ref keys[lo], ref keys[median]);
            }

            if (lo != hi)
            {
                SwapIfGreaterWithItems(ref keys[lo], ref keys[hi]);
            }

            if (median != hi)
            {
                SwapIfGreaterWithItems(ref keys[median], ref keys[hi]);
            }

            int pivot = keys[median];
            Swap(ref keys[median], ref keys[hi - 1]);

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

                Swap(ref keys[left], ref keys[right]);
            }

            // Put pivot in the right location.
            Swap(ref keys[left], ref keys[hi - 1]);
            return left;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Heapsort(int[] keys, int lo, int hi)
        {
            int n = hi - lo + 1;
            for (int i = n / 2; i >= 1; i = i - 1)
            {
                DownHeap(keys, i, n, lo);
            }

            for (int i = n; i > 1; i = i - 1)
            {
                Swap(ref keys[lo], ref keys[lo + i - 1]);
                DownHeap(keys, 1, i - 1, lo);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        private static void SwapIfGreaterWithItems(ref int keyA, ref int keyB)
        {
            if (keyA > keyB)
            {
                int key = keyA;
                keyA = keyB;
                keyB = key;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Swap(ref int keyI, ref int keyJ)
        {
            int copy = keyI;
            keyI = keyJ;
            keyJ = copy;
        }

        private static int FloorLog2(int n)
        {
            int result = 0;
            while (n >= 1)
            {
                ++result;
                n = n / 2;
            }

            return result;
        }
    }
}