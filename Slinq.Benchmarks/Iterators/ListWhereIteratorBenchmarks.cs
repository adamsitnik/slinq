using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;

namespace Slinq.Benchmarks.Iterators
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class ListWhereIteratorBenchmarks
    {
        private static readonly List<int> Numbers = Enumerable.Range(1, 1000).ToList();

        [Benchmark]
        public int IterateOverWhereResult_Struct()
        {
            int sum = 0;
            foreach (var number in ListExtensions.Where(Numbers, number => number % 2 == 0))
            {
                sum += number;
            }

            return sum;
        }

        [Benchmark]
        public int IterateOverWhereResult_MS()
        {
            int sum = 0;
            foreach (var number in Enumerable.Where(Numbers, number => number % 2 == 0))
            {
                sum += number;
            }

            return sum;
        }
    }
}