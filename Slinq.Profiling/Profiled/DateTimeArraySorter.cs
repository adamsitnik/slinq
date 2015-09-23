using System;
using System.Runtime.CompilerServices;
using Slinq.Abstract;

namespace Slinq.Profiling.Profiled
{
    public class DateTimeArraySorter : IArraySorter<DateTime>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Sort(DateTime[] array)
        {
            IntrospectiveSort(array, 0, array.Length - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsGreaterThan(ref DateTime ptr, ref DateTime ptr2)
        {
            return ptr.CompareTo(ptr2) > 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsLessThan(ref DateTime ptr, ref DateTime ptr2)
        {
            return ptr.CompareTo(ptr2) < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int FloorLog2(int i)
        {
            int num = 0;
            while (i >= 1)
            {
                num++;
                i /= 2;
            }

            return num;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Swap(DateTime[] array, int num, int num2)
        {
            DateTime dateTime = array[num];
            array[num] = array[num2];
            array[num2] = dateTime;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SwapIfGreaterWithItems(DateTime[] array, int num, int num2)
        {
            DateTime dateTime = array[num];
            DateTime dateTime2 = array[num2];
            if (IsGreaterThan(ref dateTime, ref dateTime2))
            {
                array[num] = dateTime2;
                array[num2] = dateTime;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DownHeap(DateTime[] array, int i, int num, int num2)
        {
            DateTime dateTime = array[num2 + i - 1];
            while (i <= num / 2)
            {
                int num3 = 2 * i;
                if (num3 < num && IsLessThan(ref array[num2 + num3 - 1], ref array[num2 + num3]))
                {
                    num3++;
                }

                if (!IsLessThan(ref dateTime, ref array[num2 + num3 - 1]))
                {
                    break;
                }

                array[num2 + i - 1] = array[num2 + num3 - 1];
                i = num3;
            }

            array[num2 + i - 1] = dateTime;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Heapsort(DateTime[] array, int num, int num2)
        {
            int num3 = num2 - num + 1;
            for (int i = num3 / 2; i >= 1; i--)
            {
                DownHeap(array, i, num3, num);
            }

            for (int j = num3; j > 1; j--)
            {
                Swap(array, num, num + j - 1);
                DownHeap(array, 1, j - 1, num);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void InsertionSort(DateTime[] array, int num, int num2)
        {
            for (int i = num; i < num2; i++)
            {
                int num3 = i;
                DateTime dateTime = array[i + 1];
                while (num3 >= num && !IsGreaterThan(ref dateTime, ref array[num3]))
                {
                    array[num3 + 1] = array[num3];
                    num3--;
                }

                array[num3 + 1] = dateTime;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int PickPivotAndPartition(DateTime[] array, int num, int num2)
        {
            int num3 = num + (num2 - num) / 2;
            if (num != num3)
            {
                SwapIfGreaterWithItems(array, num, num3);
            }

            if (num != num2)
            {
                SwapIfGreaterWithItems(array, num, num2);
            }

            if (num3 != num2)
            {
                SwapIfGreaterWithItems(array, num3, num2);
            }

            DateTime dateTime = array[num3];
            Swap(array, num3, num2 - 1);
            int i = num;
            int num4 = num2 - 1;
            while (i < num4)
            {
                while (i < num2 - 1 && IsLessThan(ref array[++i], ref dateTime))
                {
                }

                while (num4 > num && IsLessThan(ref dateTime, ref array[--num4]))
                {
                }

                if (i >= num4)
                {
                    break;
                }

                Swap(array, i, num4);
            }

            Swap(array, i, num2 - 1);
            return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IntroSort(DateTime[] array, int num, int i, int num2)
        {
            while (i > num)
            {
                int num3 = i - num + 1;
                if (num3 <= 16)
                {
                    if (num3 == 1)
                    {
                        return;
                    }

                    if (num3 == 2)
                    {
                        if (num != i)
                        {
                            SwapIfGreaterWithItems(array, num, i);
                        }

                        return;
                    }

                    if (num3 == 3)
                    {
                        SwapIfGreaterWithItems(array, num, i - 1);
                        if (num != i)
                        {
                            SwapIfGreaterWithItems(array, num, i);
                        }

                        SwapIfGreaterWithItems(array, i - 1, i);
                        return;
                    }

                    InsertionSort(array, num, i);
                    return;
                }
                else
                {
                    if (num2 == 0)
                    {
                        Heapsort(array, num, i);
                        return;
                    }

                    num2--;
                    int num4 = PickPivotAndPartition(array, num, i);
                    IntroSort(array, num4 + 1, i, num2);
                    i = num4 - 1;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IntrospectiveSort(DateTime[] array, int num, int num2)
        {
            int num3 = num2 - num + 1;
            if (num3 < 2)
            {
                return;
            }

            IntroSort(array, num, num2, 2 * FloorLog2(num3));
        }
    }
}