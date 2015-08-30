using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 3)]
    public class BoundariesCheckEliminationBenchmarks
    {
        private static readonly int[] Numbers = DataGenerator.GenerateRandomNumbers(1000);

        [Benchmark]
        public int With()
        {
            return SumWithCheckElimination(Numbers);
        }

        [Benchmark]
        public int Without()
        {
            return SumWithoutCheckElimination(Numbers, 1000);
        }

        [Benchmark]
        public int TryToEliminateWithMathMin()
        {
            return SumTryToEliminateCheckWithMathMin(Numbers, 1000);
        }

        [Benchmark]
        public int TryToEliminateWithDoubleCheck()
        {
            return SumTryToEliminateWithDoubleCheck(Numbers, 1000);
        }

        [Benchmark]
        public int TryToEliminateWithContractGuard()
        {
            return SumTryToEliminateWithContractGuard(Numbers, 1000);
        }

        [Benchmark]
        public int TryToEliminateWithBranchPrediction()
        {
            return SumTryToEliminateWithBranchPrediction(Numbers, 1000);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumWithCheckElimination(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumWithoutCheckElimination(int[] numbers, int length)
        {
            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumTryToEliminateCheckWithMathMin(int[] numbers, int length)
        {
            int sum = 0;
            int min = Math.Min(length, numbers.Length);
            for (int i = 0; i < min; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumTryToEliminateWithDoubleCheck(int[] numbers, int length)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length && i < length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumTryToEliminateWithContractGuard(int[] numbers, int length)
        {
            if (length > numbers.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumTryToEliminateWithBranchPrediction(int[] numbers, int length)
        {
            int sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (i < length)
                {
                    sum += numbers[i];
                }
            }

            return sum;
        }
    }
}