using System;
using System.Diagnostics.CodeAnalysis;
using Slinq.Extensions;

namespace Slinq.Benchmarks
{
    [SuppressMessage("Microsoft.Performance",
        "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Not going to be compared for equality")]
    public struct HugeValueType : IComparable<HugeValueType>
    {
        internal readonly int Integer;

        internal readonly long HugeInteger;

        internal readonly long AnotherHugeInteger;

        internal readonly long LastHugeInteger;

        public HugeValueType(int integer, long hugeInteger, long anotherHugeInteger, long lastHugeInteger)
        {
            Integer = integer;
            HugeInteger = hugeInteger;
            AnotherHugeInteger = anotherHugeInteger;
            LastHugeInteger = lastHugeInteger;
        }

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Readability")]
        public static bool operator >(HugeValueType left, HugeValueType right)
        {
            if (left.Integer > right.Integer)
            {
                return true;
            }
            if (left.Integer < right.Integer)
            {
                return false;
            }
            if (left.HugeInteger > right.HugeInteger)
            {
                return true;
            }
            if (left.HugeInteger < right.HugeInteger)
            {
                return false;
            }
            if (left.AnotherHugeInteger > right.AnotherHugeInteger)
            {
                return true;
            }
            if (left.AnotherHugeInteger < right.AnotherHugeInteger)
            {
                return false;
            }
            if (left.LastHugeInteger > right.LastHugeInteger)
            {
                return true;
            }
            if (left.LastHugeInteger < right.LastHugeInteger)
            {
                return false;
            }

            return false;
        }

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Readability")]
        public static bool operator <(HugeValueType left, HugeValueType right)
        {
            if (left.Integer < right.Integer)
            {
                return true;
            }
            if (left.Integer > right.Integer)
            {
                return false;
            }
            if (left.HugeInteger < right.HugeInteger)
            {
                return true;
            }
            if (left.HugeInteger > right.HugeInteger)
            {
                return false;
            }
            if (left.AnotherHugeInteger < right.AnotherHugeInteger)
            {
                return true;
            }
            if (left.AnotherHugeInteger > right.AnotherHugeInteger)
            {
                return false;
            }
            if (left.LastHugeInteger < right.LastHugeInteger)
            {
                return true;
            }
            if (left.LastHugeInteger > right.LastHugeInteger)
            {
                return false;
            }

            return false;
        }

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Readability")]
        public int CompareTo(HugeValueType other)
        {
            if (Integer > other.Integer)
            {
                return 1;
            }
            if (Integer < other.Integer)
            {
                return -1;
            }
            if (HugeInteger > other.HugeInteger)
            {
                return 1;
            }
            if (HugeInteger < other.HugeInteger)
            {
                return -1;
            }
            if (AnotherHugeInteger > other.AnotherHugeInteger)
            {
                return 1;
            }
            if (AnotherHugeInteger < other.AnotherHugeInteger)
            {
                return -1;
            }
            if (LastHugeInteger > other.LastHugeInteger)
            {
                return 1;
            }
            if (LastHugeInteger < other.LastHugeInteger)
            {
                return -1;
            }

            return 0;
        }
    }

    public static class DataGenerator
    {
        /// <summary>
        /// same seed for all ensures that the values are going to be the same
        /// </summary>
        private const int InitialRandomSeed = 124506642;

        public static int[] GenerateRandomNumbers(int count = 300)
        {
            var random = new Random(InitialRandomSeed); 

            return StrongEnumerable.Range(1, count)
                .Select(_ => random.Next()).ToArray();
        }

        public static DateTime[] GenerateRandomDates(int count = 300)
        {
            var random = new Random(InitialRandomSeed);

            return StrongEnumerable.Range(1, count)
                .Select(_ => 
                    new DateTime(
                        random.Next(2000, 2015), 
                        random.Next(1, 12), 
                        random.Next(1, 28)))
                .ToArray();
        }

        public static HugeValueType[] GenerateHugeValueTypes(int count = 300)
        {
            var random = new Random(InitialRandomSeed);

            return StrongEnumerable.Range(1, count)
                .Select(_ => new HugeValueType(random.Next(), random.Next(), random.Next(), random.Next()))
                .ToArray();
        }
    }
}