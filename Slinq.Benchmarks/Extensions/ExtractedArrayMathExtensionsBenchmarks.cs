using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;
using Slinq.Models;

namespace Slinq.Benchmarks.Extensions
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class ExtractedArrayMathExtensionsBenchmarks
    {
        private static readonly int[] Array = Enumerable.Range(1, 10000).ToArray();

        [Benchmark]
        public int Sum()
        {
            return new ExtractedArray<int>(Array, Array.Length).Sum();
        }
    }
}