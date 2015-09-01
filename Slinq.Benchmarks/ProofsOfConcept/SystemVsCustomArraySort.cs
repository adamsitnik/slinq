using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Utils;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class SystemArraySort
    {
        private readonly int[] _smallValueTypes = DataGenerator.GenerateRandomNumbers(1000);

        [Benchmark]
        public int[] Integers()
        {
            Array.Sort(_smallValueTypes);

            return _smallValueTypes;
        }
    }

    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Better readability")]
    public class OptimizedSystemArraySort
    {
        private readonly int[] _smallValueTypes = DataGenerator.GenerateRandomNumbers(1000);

        [Benchmark]
        public int[] Integers()
        {
            GenericArraySorter.IntrospectiveSort(_smallValueTypes, 0, _smallValueTypes.Length - 1, new ArraySortersBenchmarks.ExperimentalComparer());

            return _smallValueTypes;
        }
    }

    /// <summary>
    /// MS has optimization for Array.Sort(int[]) that calls the extern method implemented in C++, this benchmarks show the difference
    /// </summary>
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class SystemArraySortUsingCustomComparer
    {
        private readonly int[] _smallValueTypes = DataGenerator.GenerateRandomNumbers(1000);

        [Benchmark]
        public int[] Integers()
        {
            Array.Sort(_smallValueTypes, new CustomIntComparer());

            return _smallValueTypes;
        }

        private class CustomIntComparer : IComparer<int>
        {
            public int Compare(int left, int right)
            {
                if (left > right)
                {
                    return 1;
                }

                if (left < right)
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}