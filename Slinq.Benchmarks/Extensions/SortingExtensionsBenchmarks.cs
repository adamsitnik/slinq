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
        private static readonly int[] OrderedIntegers = Enumerable.Range(1, 1000).ToArray();
        private static readonly int[] ReversedOrderedIntegers = Enumerable.Range(1, 1000).Reverse().ToArray();
        private static readonly Random Random = new Random();
        private static readonly int[] RandomIntegers = Enumerable.Range(1, 1000).Select(_ => Random.Next()).ToArray();
        private static readonly DateTime[] RandomDates = GenerateRandomDates();

        [Benchmark]
        public int[] SortSimpleIntegers_Ordered_Slinq()
        {
            return SortingExtensions.OrderBy(OrderedIntegers, number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Ordered_Linq()
        {
            return Enumerable.OrderBy(OrderedIntegers, number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Reversed_Slinq()
        {
            return SortingExtensions.OrderBy(ReversedOrderedIntegers, number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Reversed_Linq()
        {
            return Enumerable.OrderBy(ReversedOrderedIntegers, number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Random_Slinq()
        {
            return SortingExtensions.OrderBy(RandomIntegers, number => number).ToArray();
        }

        [Benchmark]
        public int[] SortSimpleIntegers_Random_Linq()
        {
            return Enumerable.OrderBy(RandomIntegers, number => number).ToArray();
        }

        [Benchmark]
        public DateTime[] SortDatetimes_Random_Slinq()
        {
            return SortingExtensions.OrderBy(RandomDates, date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }

        [Benchmark]
        public DateTime[] SortDatetimes_Random_Linq()
        {
            return Enumerable.OrderBy(RandomDates, date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }

        private static DateTime[] GenerateRandomDates()
        {
            var random = new Random();

            return Enumerable.Range(1, 1000)
                .Select(_ => new DateTime(random.Next(2000, 2015), random.Next(1, 12), random.Next(1, 28)))
                .ToArray();
        }
    }
}