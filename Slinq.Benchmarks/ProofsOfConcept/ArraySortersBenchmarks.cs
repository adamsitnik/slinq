using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;
using Slinq.Utils;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class ArraySortersBenchmarks
    {
        /// <summary>
        /// https://github.com/dotnet/coreclr/blob/4cf8a6b082d9bb1789facd996d8265d3908757b2/src/classlibnative/bcltype/arrayhelpers.cpp
        /// </summary>
        /// <returns></returns>
        [Benchmark]
        public int[] ArraySort_OnBCLPrimitiveTypeThatIsImplementedByCLRinCppAsExternMethod()
        {
            int[] randomNumbers = DataGenerator.GenerateRandomNumbers();

            Array.Sort(randomNumbers);

            return randomNumbers;
        }

        [Benchmark]
        public IntWrapper[] ArraySort_OnSimpleIntWrapperThatDoesNotGetExtraHelpFromExternMethod()
        {
            IntWrapper[] randomNumbers = GenerateRandomWrappedNumbers();

            Array.Sort(randomNumbers);

            return randomNumbers;
        }

        [Benchmark]
        public int[] ArraySort_Comparison()
        {
            int[] randomNumbers = DataGenerator.GenerateRandomNumbers();

            Array.Sort(randomNumbers, (left, right) => left.CompareTo(right));

            return randomNumbers;
        }

        [Benchmark]
        public int[] ArraySort_NonDefaultComparer()
        {
            int[] randomNumbers = DataGenerator.GenerateRandomNumbers();

            Array.Sort(randomNumbers, new NonDefaultComparer());

            return randomNumbers;
        }

        [Benchmark]
        public int[] OptimizedVersion()
        {
            int[] randomNumbers = DataGenerator.GenerateRandomNumbers();

            ArraySorter.IntrospectiveSort(randomNumbers, 0, randomNumbers.Length - 1);

            return randomNumbers;
        }

        [Benchmark]
        public int[] SlinqExtensions()
        {
            return SortingExtensions.OrderBy(DataGenerator.GenerateRandomNumbers(), number => number).ToArray();
        }

        [Benchmark]
        public int[] EnumerableExtensions()
        {
            return Enumerable.OrderBy(DataGenerator.GenerateRandomNumbers(), number => number).ToArray();
        }

        private static IntWrapper[] GenerateRandomWrappedNumbers()
        {
            var random = new Random(1834607); // same seed for all ensures that the values are going to be the same

            return StrongEnumerable.Range(1, 300).Select(_ =>  new IntWrapper(random.Next())).ToArray();
        }
    }

    public struct IntWrapper : IComparable<IntWrapper>, IEquatable<IntWrapper>
    {
        private int _myValue;

        public IntWrapper(int myValue)
        {
            _myValue = myValue;
        }

        public static bool operator >(IntWrapper left, IntWrapper right)
        {
            return left._myValue > right._myValue;
        }

        public static bool operator <(IntWrapper left, IntWrapper right)
        {
            return left._myValue < right._myValue;
        }

        public static bool operator ==(IntWrapper left, IntWrapper right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(IntWrapper left, IntWrapper right)
        {
            return !left.Equals(right);
        }

        public int CompareTo(IntWrapper other)
        {
            if (_myValue > other._myValue)
            {
                return 1;
            }

            if (_myValue < other._myValue)
            {
                return -1;
            }

            return 0;
        }

        public bool Equals(IntWrapper other)
        {
            return _myValue == other._myValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is IntWrapper && Equals((IntWrapper)obj);
        }

        public override int GetHashCode()
        {
            return _myValue;
        }
    }

    internal class NonDefaultComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return x.CompareTo(y);
        }
    }
}