using System.Runtime.CompilerServices;

namespace Slinq.Utils
{
    internal class ArraySorterHelper
    {
        internal const int IntrosortSizeThreshold = 16;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int FloorLog2(int n)
        {
            int result = 0;
            while (n >= 1)
            {
                ++result;
                n = n / 2;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Swap<T>(T[] keys, int leftIndex, int rightIndex)
        {
            var copy = keys[leftIndex];
            keys[leftIndex] = keys[rightIndex];
            keys[rightIndex] = copy;
        }
    }
}