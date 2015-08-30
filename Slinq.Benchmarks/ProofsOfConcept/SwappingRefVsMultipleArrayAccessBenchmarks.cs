using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 3)]
    public class SwappingRefVsMultipleArrayAccessBenchmarks
    {
        private static readonly int[] SmallValueTypes = DataGenerator.GenerateRandomNumbers(1000);
        private static readonly DateTime[] MediumValueTypes = DataGenerator.GenerateRandomDates(1000);
        private static readonly HugeValueType[] HugeValueTypes = DataGenerator.GenerateHugeValueTypes(1000);

        [Benchmark]
        public int[] SwapSmallValueTypes_ByRef()
        {
            return SwapByRef(SmallValueTypes);
        }

        [Benchmark]
        public int[] SwapSmallValueTypes_ByMultipleArrayAccess()
        {
            return SwapByMultipleArrayAccess(SmallValueTypes);
        }

        [Benchmark]
        public DateTime[] SwapMediumValueTypes_ByRef()
        {
            return SwapByRef(MediumValueTypes);
        }

        [Benchmark]
        public DateTime[] SwapMediumValueTypes_ByMultipleArrayAccess()
        {
            return SwapByMultipleArrayAccess(MediumValueTypes);
        }

        [Benchmark]
        public HugeValueType[] SwapHugeValueTypes_ByRef()
        {
            return SwapByRef(HugeValueTypes);
        }

        [Benchmark]
        public HugeValueType[] SwapHugeValueTypes_ByMultipleArrayAccess()
        {
            return SwapByMultipleArrayAccess(HugeValueTypes);
        }

        private T[] SwapByRef<T>(T[] source)
        {
            for (int i = 0; i < source.Length / 2; i += 2)
            {
                SwapByReference(ref source[i], ref source[i + 1]);
            }

            return source;
        }

        private T[] SwapByMultipleArrayAccess<T>(T[] source)
        {
            for (int i = 0; i < source.Length / 2; i += 2)
            {
                SwapWithMultipleArrayAccess(source, i, i + 1);
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SwapWithMultipleArrayAccess<T>(T[] source, int leftIndex, int rightIndex)
        {
            var copy = source[leftIndex];
            source[leftIndex] = source[rightIndex];
            source[rightIndex] = copy;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void SwapByReference<T>(ref T left, ref T right)
        {
            var copy = left;
            left = right;
            right = copy;
        }
    }
}