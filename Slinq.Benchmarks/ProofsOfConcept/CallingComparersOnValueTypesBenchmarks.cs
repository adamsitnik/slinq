using System.Runtime.CompilerServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class CallingComparersOnValueTypesBenchmarks
    {
        private static HugeValueType _first = new HugeValueType(100, 200, 33, 44);
        private static HugeValueType _second = new HugeValueType(100, 200, 33, 44);

        [Benchmark]
        public int CompareRef()
        {
            return CompareWithRef(ref _first, ref _second);
        }

        [Benchmark]
        public int CompareWithoutRef()
        {
            return CompareWithoutRef(_first, _second);
        }

        [Benchmark]
        public bool OperatorRef()
        {
            return OperatorWithRef(ref _first, ref _second);
        }

        [Benchmark]
        public bool OperatorWithoutRef()
        {
            return OperatorWithoutRef(_first, _second);
        }

        [Benchmark]
        public bool MixRef()
        {
            return MixWithRef(ref _first, ref _second);
        }

        [Benchmark]
        public bool MixWithoutRef()
        {
            return MixWithoutRef(_first, _second);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int CompareWithRef(ref HugeValueType left, ref HugeValueType right)
        {
            return left.CompareTo(right);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int CompareWithoutRef(HugeValueType left, HugeValueType right)
        {
            return left.CompareTo(right);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool OperatorWithRef(ref HugeValueType left, ref HugeValueType right)
        {
            return left > right;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool OperatorWithoutRef(HugeValueType left, HugeValueType right)
        {
            return left > right;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool MixWithRef(ref HugeValueType left, ref HugeValueType right)
        {
            return left.CompareTo(right) > 0;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool MixWithoutRef(HugeValueType left, HugeValueType right)
        {
            return left.CompareTo(right) > 0;
        }
    }
}