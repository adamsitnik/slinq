using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 3)]
    public class MinManualLoopingBenchmarks
    {
        private static readonly int[] Numbers = DataGenerator.GenerateRandomNumbers(8 * 100);

        [Benchmark]
        public int Without()
        {
            return MinWithoutManualLoopUnrolling(Numbers);
        }

        [Benchmark]
        public int With_8()
        {
            return MinWithManualLoopUnrolling_If_8(Numbers);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int MinWithoutManualLoopUnrolling(int[] numbers)
        {
            int min = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                if (numbers[i] < min)
                {
                    min = numbers[i];
                }
            }

            return min;
        }

        [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Readability"),
            MethodImpl(MethodImplOptions.NoInlining)]
        private int MinWithManualLoopUnrolling_If_8(int[] numbers)
        {
            int min = numbers[0];
            for (int i = 0; i < numbers.Length - 8; i += 8)
            {
                if (numbers[i] < min)
                {
                    min = numbers[i];
                }
                if (numbers[i + 1] < min)
                {
                    min = numbers[i + 1];
                }
                if (numbers[i + 2] < min)
                {
                    min = numbers[i + 2];
                }
                if (numbers[i + 3] < min)
                {
                    min = numbers[i + 3];
                }
                if (numbers[i + 4] < min)
                {
                    min = numbers[i + 4];
                }
                if (numbers[i + 5] < min)
                {
                    min = numbers[i + 5];
                }
                if (numbers[i + 6] < min)
                {
                    min = numbers[i + 6];
                }
                if (numbers[i + 7] < min)
                {
                    min = numbers[i + 7];
                }
                if (numbers[i + 8] < min)
                {
                    min = numbers[i + 8];
                }
            }
            for (int i = (numbers.Length / 8) * 8; i < numbers.Length; i++)
            {
                if (min > numbers[i])
                {
                    min = numbers[i];
                }
            }

            return min;
        }
    }
}