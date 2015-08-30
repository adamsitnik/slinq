using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;

namespace Slinq.Benchmarks.Iterators
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class WhereLastBenchmark
    {
        private static readonly int[] Numbers = DataGenerator.GenerateRandomNumbers(1000);

        [Benchmark]
        public int OverArray_Slinq()
        {
            return ArrayExtensions
                .Where(Numbers, number => number % 29 == 0)
                .Last();
        }

        [Benchmark]
        public int OverArray_Linq()
        {
            return Enumerable
                .Where(Numbers, number => number % 29 == 0)
                .Last();
        }
    }
}