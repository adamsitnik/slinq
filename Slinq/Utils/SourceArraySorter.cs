using System.Runtime.CompilerServices;
using Slinq.Abstract;

namespace Slinq.Utils
{
    /// <summary>
    /// this class is just a template that is going to be fullfilled during runtime
    /// gains: no boxing, no copying, no interface method invoking cost
    /// </summary>
    public class SourceArraySorter<T> : IArraySorter<T>
    {
        public void Sort(T[] array)
        {
            IntrospectiveSort(array, 0, array.Length - 1);
        }
        
        /// <summary>
        /// this method is going to be implemented during run time
        /// </summary>
        private bool IsGreaterThan(ref T value, ref T than)
        {
            return false;
        }

        /// <summary>
        /// this method is going to be implemented during run time
        /// </summary>
        private bool IsLessThan(ref T value, ref T than)
        {
            return false;
        }

        private void IntrospectiveSort(T[] keys, int indexFrom, int indexTo)
        {
            int length = indexTo - indexFrom + 1;

            if (length < 2)
            {
                return;
            }

            IntroSort(keys, indexFrom, indexTo, 2 * ArraySorterHelper.FloorLog2(length));
        }

        private void IntroSort(T[] keys, int lo, int hi, int depthLimit)
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

        private int PickPivotAndPartition(T[] keys, int lo, int hi)
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

            T pivot = keys[median];
            ArraySorterHelper.Swap(keys, median, hi - 1);

            int left = lo, right = hi - 1;

            // We already partitioned lo and hi and put the pivot in hi - 1.  And we pre-increment & decrement below.
            while (left < right)
            {
                while (left < (hi - 1) && IsLessThan(ref keys[++left], ref pivot))
                {
                }

                while (right > lo && IsLessThan(ref pivot, ref keys[--right]))
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

        private void Heapsort(T[] keys, int lo, int hi)
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

        private void DownHeap(T[] keys, int i, int n, int lo)
        {
            T d = keys[lo + i - 1];
            int child;

            while (i <= n / 2)
            {
                child = 2 * i;
                if (child < n && IsLessThan(ref keys[lo + child - 1], ref keys[lo + child]))
                {
                    child++;
                }

                if (!(IsLessThan(ref d, ref keys[lo + child - 1])))
                {
                    break;
                }

                keys[lo + i - 1] = keys[lo + child - 1];

                i = child;
            }

            keys[lo + i - 1] = d;
        }

        private void InsertionSort(T[] keys, int lo, int hi)
        {
            int i, j;
            T t;
            for (i = lo; i < hi; i++)
            {
                j = i;
                t = keys[i + 1];
                while (j >= lo && !IsGreaterThan(ref t, ref keys[j]))
                {
                    keys[j + 1] = keys[j];
                    j--;
                }

                keys[j + 1] = t;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SwapIfGreaterWithItems(T[] keys, int leftIndex, int rightIndex)
        {
            var leftKeyValue = keys[leftIndex];
            var rightKeyValue = keys[rightIndex];
            if (IsGreaterThan(ref leftKeyValue, ref rightKeyValue))
            {
                keys[leftIndex] = rightKeyValue;
                keys[rightIndex] = leftKeyValue;
            }
        }
    }
}