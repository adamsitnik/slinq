using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;

namespace Slinq.Benchmarks.Iterators
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class ArrayWhereSelectIteratorBenchmarks
    {
        private static readonly int[] Numbers = DataGenerator.GenerateRandomNumbers(1000).ToArray();

        [Benchmark]
        public int IterateOverWhereSelectResult_Slinq()
        {
            int sum = 0;
            foreach (var number in ArrayExtensions
                .Where(Numbers, number => number % 2 == 0)
                .Select(number => number * 2))
            {
                sum += number;
            }

            return sum;
        }

        [Benchmark]
        public int IterateOverWhereSelectResult_Linq()
        {
            int sum = 0;
            foreach (var number in Enumerable
                .Where(Numbers, number => number % 2 == 0)
                .Select(number => number * 2))
            {
                sum += number;
            }

            return sum;
        }
    }
}