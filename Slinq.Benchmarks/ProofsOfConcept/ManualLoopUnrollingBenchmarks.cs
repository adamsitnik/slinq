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

        [Benchmark]
        public int WithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_8()
        {
            return SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_8(Numbers);
        }

        [Benchmark]
        public int WithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_16_OnlyOnDivisibleSize()
        {
            return SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_16_OnlyOnDivisibleSize(Numbers);
        }

        [Benchmark]
        public int SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_16_SingleIteratorVariable()
        {
            return SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_16_SingleIteratorVariable(Numbers);
        }

        [Benchmark]
        public int SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_16_TwoLoops()
        {
            return SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_16_TwoLoops(Numbers);
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
        private int SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_4(int[] numbers)
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

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_8(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length / 8; i += 8)
            {
                checked
                {
                    sum += numbers[i];
                    sum += numbers[unchecked(i + 1)];
                    sum += numbers[unchecked(i + 2)];
                    sum += numbers[unchecked(i + 3)];
                    sum += numbers[unchecked(i + 4)];
                    sum += numbers[unchecked(i + 5)];
                    sum += numbers[unchecked(i + 6)];
                    sum += numbers[unchecked(i + 7)];
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_16_OnlyOnDivisibleSize(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length / 16; i += 16)
            {
                checked
                {
                    sum += numbers[i];
                    sum += numbers[unchecked(i + 1)];
                    sum += numbers[unchecked(i + 2)];
                    sum += numbers[unchecked(i + 3)];
                    sum += numbers[unchecked(i + 4)];
                    sum += numbers[unchecked(i + 5)];
                    sum += numbers[unchecked(i + 6)];
                    sum += numbers[unchecked(i + 7)];
                    sum += numbers[unchecked(i + 8)];
                    sum += numbers[unchecked(i + 9)];
                    sum += numbers[unchecked(i + 10)];
                    sum += numbers[unchecked(i + 11)];
                    sum += numbers[unchecked(i + 12)];
                    sum += numbers[unchecked(i + 13)];
                    sum += numbers[unchecked(i + 14)];
                    sum += numbers[unchecked(i + 15)];
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_16_SingleIteratorVariable(int[] numbers)
        {
            int sum = 0;
            int i = 0;
            for (; i < numbers.Length / 16; i += 16)
            {
                checked
                {
                    sum += numbers[i];
                    sum += numbers[unchecked(i + 1)];
                    sum += numbers[unchecked(i + 2)];
                    sum += numbers[unchecked(i + 3)];
                    sum += numbers[unchecked(i + 4)];
                    sum += numbers[unchecked(i + 5)];
                    sum += numbers[unchecked(i + 6)];
                    sum += numbers[unchecked(i + 7)];
                    sum += numbers[unchecked(i + 8)];
                    sum += numbers[unchecked(i + 9)];
                    sum += numbers[unchecked(i + 10)];
                    sum += numbers[unchecked(i + 11)];
                    sum += numbers[unchecked(i + 12)];
                    sum += numbers[unchecked(i + 13)];
                    sum += numbers[unchecked(i + 14)];
                    sum += numbers[unchecked(i + 15)];
                }
            }

            for (; i < numbers.Length; i++)
            {
                checked
                {
                    sum += numbers[i];
                }
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SumWithManualLoopUnrolling_IndexerIncrement_Outside_CheckedBlock_16_TwoLoops(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length / 16; i += 16)
            {
                checked
                {
                    sum += numbers[i];
                    sum += numbers[unchecked(i + 1)];
                    sum += numbers[unchecked(i + 2)];
                    sum += numbers[unchecked(i + 3)];
                    sum += numbers[unchecked(i + 4)];
                    sum += numbers[unchecked(i + 5)];
                    sum += numbers[unchecked(i + 6)];
                    sum += numbers[unchecked(i + 7)];
                    sum += numbers[unchecked(i + 8)];
                    sum += numbers[unchecked(i + 9)];
                    sum += numbers[unchecked(i + 10)];
                    sum += numbers[unchecked(i + 11)];
                    sum += numbers[unchecked(i + 12)];
                    sum += numbers[unchecked(i + 13)];
                    sum += numbers[unchecked(i + 14)];
                    sum += numbers[unchecked(i + 15)];
                }
            }

            for (int i = (numbers.Length / 16) * 16; i < numbers.Length; i++)
            {
                checked
                {
                    sum += numbers[i];
                }
            }

            return sum;
        }
    }
}