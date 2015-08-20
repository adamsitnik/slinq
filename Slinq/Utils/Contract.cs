using System;
using System.Runtime.CompilerServices;

namespace Slinq.Utils
{
    internal static class Contract
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresInRange(int start, int length)
        {
            // explicit unchecked block is used in order to avoid OverflowException when casting 
            // negative int to unsigned int when the code is compiled with /checked option
            unchecked
            {
                // use unsigned int to reduce range check by one ( >= 0 )
                if ((uint)start >= (uint)length)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RequiresInInclusiveRange(int start, int length)
        {
            unchecked
            {
                if ((uint)start > (uint)length)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInRange(int start, int length)
        {
            unchecked
            {
                return (uint)start < (uint)length;
            }
        }
    }
}