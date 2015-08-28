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
        private static readonly Random Random = new Random();

        [Benchmark]
        public int[] SortSimpleIntegers_Ordered_Slinq()
        {
            return SortingExtensions.OrderBy(Enumerable.Range(1, 1000).ToArray(), number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Ordered_Linq()
        {
            return Enumerable.OrderBy(Enumerable.Range(1, 1000).ToArray(), number => number).ToArray();
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
            return SortingExtensions.OrderBy(Enumerable.Range(1, 1000).Select(_ => Random.Next()).ToArray(), number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Random_Linq()
        {
            return Enumerable.OrderBy(Enumerable.Range(1, 1000).Select(_ => Random.Next()).ToArray(), number => number).ToArray();
        }

        [Benchmark]
        public DateTime[] SortDatetimes_Random_Slinq()
        {
            return SortingExtensions.OrderBy(GenerateRandomDates(), date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }

        [Benchmark]
        public DateTime[] SortDatetimes_Random_Linq()
        {
            return Enumerable.OrderBy(GenerateRandomDates(), date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }

        private static DateTime[] GenerateRandomDates()
        {
            return Enumerable.Range(1, 1000)
                .Select(_ => new DateTime(Random.Next(2000, 2015), Random.Next(1, 12), Random.Next(1, 28)))
                .ToArray();
        }
    }
}