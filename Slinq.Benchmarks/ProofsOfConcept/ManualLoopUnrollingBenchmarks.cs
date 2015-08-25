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
        private static readonly int[] Numbers = Enumerable.Range(1, (16 * 100) + 5).ToArray();

        [Benchmark]
        public int Without()
        {
            return SumWithoutManualLoopUnrolling(Numbers);
        }

        [Benchmark]
        public int WithManualLoopUnrolling_IndexerIncrement_Inside_CheckedBlock_4()
        {
            return SumWithManualLoopUnrolling_IndexerIncrement_Inside_CheckedBlock_4(Numbers);
        }

        [Benchmark]
        public int WithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_4()
        {
            return SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_4(Numbers);
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
        private static int SumWithManualLoopUnrolling_IndexerIncrement_Inside_CheckedBlock_4(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length - 4; i += 4)
            {
                checked
                {
                    sum += numbers[i];
                    sum += numbers[i + 1];
                    sum += numbers[i + 2];
                    sum += numbers[i + 3];
                }
            }

            for (int i = (numbers.Length / 4) * 4; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_4(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length - 4; i += 4)
            {
                checked
                {
                    sum += numbers[i];
                    sum += numbers[unchecked(i + 1)];
                    sum += numbers[unchecked(i + 2)];
                    sum += numbers[unchecked(i + 3)];
                }
            }

            for (int i = (numbers.Length / 4) * 4; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }
    }
}