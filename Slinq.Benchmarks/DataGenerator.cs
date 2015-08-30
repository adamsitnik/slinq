using System;
using Slinq.Extensions;

namespace Slinq.Benchmarks
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Not going to be compared for equality")]
    public struct HugeValueType
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
    }

    internal static class DataGenerator
    {
        /// <summary>
        /// same seed for all ensures that the values are going to be the same
        /// </summary>
        private const int InitialRandomSeed = 124506642;

        internal static int[] GenerateRandomNumbers(int count = 300)
        {
            var random = new Random(InitialRandomSeed); 

            return StrongEnumerable.Range(1, count)
                .Select(_ => random.Next()).ToArray();
        }

        internal static DateTime[] GenerateRandomDates(int count = 300)
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

        internal static HugeValueType[] GenerateHugeValueTypes(int count = 300)
        {
            var random = new Random(InitialRandomSeed);

            return StrongEnumerable.Range(1, count)
                .Select(_ => new HugeValueType(random.Next(), random.Next(), random.Next(), random.Next()))
                .ToArray();
        }
    }
}