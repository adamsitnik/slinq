using System;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;

namespace Slinq.Benchmarks.Extensions
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class SortingExtensionsBenchmarks
    {
        [Benchmark]
        public int[] SortSimpleIntegers_Ordered_Slinq()
        {
            return SortingExtensions.OrderBy(StrongEnumerable.Range(1, 1000).ToArray(), number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Ordered_Linq()
        {
            return Enumerable.OrderBy(StrongEnumerable.Range(1, 1000).ToArray(), number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Reversed_Slinq()
        {
            return SortingExtensions.OrderBy(Enumerable.Range(1, 1000).Reverse().ToArray(), number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Reversed_Linq()
        {
            return Enumerable.OrderBy(Enumerable.Range(1, 1000).Reverse().ToArray(), number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Random_Slinq()
        {
            return SortingExtensions.OrderBy(DataGenerator.GenerateRandomNumbers(1000), number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Random_Linq()
        {
            return Enumerable.OrderBy(DataGenerator.GenerateRandomNumbers(1000), number => number).ToArray();
        }

        [Benchmark]
        public DateTime[] SortDatetimes_Random_Slinq()
        {
            return SortingExtensions.OrderBy(DataGenerator.GenerateRandomDates(1000), date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }

        [Benchmark]
        public DateTime[] SortDatetimes_Random_Linq()
        {
            return Enumerable.OrderBy(DataGenerator.GenerateRandomDates(1000), date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }
    }
}