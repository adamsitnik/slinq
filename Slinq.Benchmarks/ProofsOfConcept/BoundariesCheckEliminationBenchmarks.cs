using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 3)]
    public class BoundariesCheckEliminationBenchmarks
    {
        private static readonly int[] Numbers = Enumerable.Range(1, 1000).ToArray();

        [Benchmark]
        public int With()
        {
            return SumWithCheckElimination(Numbers);
        }

        [Benchmark]
        public int Without()
        {
            return SumWithOUTCheckElimination(Numbers, 1000);
        }

        private static int SumWithCheckElimination(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        private static int SumWithOUTCheckElimination(int[] numbers, int length)
        {
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }
    }
}