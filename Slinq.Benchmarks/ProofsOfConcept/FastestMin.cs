using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    /// <summary>
    /// this benchmarks prove that only x64 JITers support loop unrolling but only for COST-size for loops which just sucks
    /// </summary>
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class FastestMin
    {
        private const int ElementsCount = 8 * 50;
        private static readonly int[] Numbers = DataGenerator.GenerateRandomNumbers(ElementsCount);

        [Benchmark]
        public int SimpleForLoop_If()
        {
            return SimpleForLoop_If(Numbers);
        }

        [Benchmark]
        public int SimpleForLoop_MathMin()
        {
            return SimpleForLoop_MathMin(Numbers);
        }

        [Benchmark]
        public int DataDependencyElimination_MathMin()
        {
            return DataDependencyElimination_MathMin(Numbers);
        }

        [Benchmark]
        public int ManualLoopUnrolling_8_MathMin()
        {
            return ManualLoopUnrolling_8_MathMin(Numbers);
        }

        [Benchmark]
        public int SimpleForLoop_MathMin_NoBoundariesCheckElimination()
        {
            return SimpleForLoop_MathMin_NoBoundariesCheckElimination(Numbers);
        }

        [Benchmark]
        public int DataDependencyElimination_MathMin_NoBoundariesCheckElimination()
        {
            return DataDependencyElimination_MathMin_NoBoundariesCheckElimination(Numbers);
        }

        [Benchmark]
        public int SimpleForLoop_MathMin_NoBoundariesCheckElimination_LocalVariable()
        {
            return SimpleForLoop_MathMin_NoBoundariesCheckElimination_LocalVariable(Numbers);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SimpleForLoop_If(int[] numbers)
        {
            int min = int.MaxValue;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] < min)
                {
                    min = numbers[i];
                }
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SimpleForLoop_MathMin(int[] numbers)
        {
            int min = int.MaxValue;
            for (int i = 0; i < numbers.Length; i++)
            {
                min = Math.Min(min, numbers[i]);
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int DataDependencyElimination_MathMin(int[] numbers)
        {
            int min2, min1 = min2 = int.MaxValue;
            for (int i = 0; i < numbers.Length - 1; i += 2)
            {
                min1 = Math.Min(min1, numbers[i]);
                min2 = Math.Min(min2, numbers[i + 1]);
            }

            min1 = Math.Min(min1, numbers[numbers.Length - 1]);

            return Math.Min(min1, min2);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int ManualLoopUnrolling_8_MathMin(int[] numbers)
        {
            int min = int.MaxValue;
            for (int i = 0; i < numbers.Length - 7; i += 8)
            {
                min = Math.Min(min, numbers[i]);
                min = Math.Min(min, numbers[i + 1]);
                min = Math.Min(min, numbers[i + 2]);
                min = Math.Min(min, numbers[i + 3]);
                min = Math.Min(min, numbers[i + 4]);
                min = Math.Min(min, numbers[i + 5]);
                min = Math.Min(min, numbers[i + 6]);
                min = Math.Min(min, numbers[i + 7]);
            }

            for (int i = (numbers.Length / 8) * 8; i < numbers.Length; i++)
            {
                min = Math.Min(min, numbers[i]);
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SimpleForLoop_MathMin_NoBoundariesCheckElimination(int[] numbers)
        {
            int min = int.MaxValue;
            for (int i = 0; i < ElementsCount; i++)
            {
                min = Math.Min(min, numbers[i]);
            }

            return min;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int DataDependencyElimination_MathMin_NoBoundariesCheckElimination(int[] numbers)
        {
            int min2, min1 = min2 = int.MaxValue;
            for (int i = 0; i < ElementsCount - 1; i += 2)
            {
                min1 = Math.Min(min1, numbers[i]);
                min2 = Math.Min(min2, numbers[i + 1]);
            }

            min1 = Math.Min(min1, numbers[ElementsCount - 1]);

            return Math.Min(min1, min2);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SimpleForLoop_MathMin_NoBoundariesCheckElimination_LocalVariable(int[] numbers)
        {
            int elementsCount = numbers.Length;
            int min = int.MaxValue;
            for (int i = 0; i < elementsCount; i++)
            {
                min = Math.Min(min, numbers[i]);
            }

            return min;
        }
    }
}