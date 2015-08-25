using System.Linq;
using System.Runtime.CompilerServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 3)]
    public class ManualLoopUnrollingBenchmarks
    {
        private static readonly int[] Numbers = Enumerable.Range(1, 1000).ToArray();

        [Benchmark]
        public int Without()
        {
            return SumWithoutManualLoopUnrolling(Numbers);
        }

        [Benchmark]
        public int With()
        {
            return SumWithManualLoopUnrolling(Numbers);
        }

        [Benchmark]
        public int WithButIndexerIncrementOutsideCheckedBlock()
        {
            return SumWithManualLoopUnrollingButIndexerIncrementOutsideCheckedBlock(Numbers);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumWithoutManualLoopUnrolling(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                checked
                {
                    sum += numbers[i];
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumWithManualLoopUnrolling(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length / 4; i += 4)
            {
                checked
                {
                    sum += numbers[i];
                    sum += numbers[i + 1];
                    sum += numbers[i + 2];
                    sum += numbers[i + 3];
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SumWithManualLoopUnrollingButIndexerIncrementOutsideCheckedBlock(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length / 4; i += 4)
            {
                checked
                {
                    sum += numbers[i];
                    sum += numbers[unchecked(i + 1)];
                    sum += numbers[unchecked(i + 2)];
                    sum += numbers[unchecked(i + 3)];
                }
            }

            return sum;
        }
    }
}