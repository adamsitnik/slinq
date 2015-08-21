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
    public class RangeSelectIteratorBenchmarks
    {
        [Benchmark]
        public int[] ToArray_Linq()
        {
            return Enumerable.Range(100, 200).Select(number => number - 50).ToArray();
        }

        [Benchmark]
        public int[] ToArray_Slinq()
        {
            return StrongEnumerable.Range(100, 200).Select(number => number - 50).ToArray();
        }

        [Benchmark]
        public List<int> ToList_Linq()
        {
            return Enumerable.Range(100, 200).Select(number => number - 50).ToList();
        }

        [Benchmark]
        public List<int> ToList_Slinq()
        {
            return StrongEnumerable.Range(100, 200).Select(number => number - 50).ToList();
        }
    }
}