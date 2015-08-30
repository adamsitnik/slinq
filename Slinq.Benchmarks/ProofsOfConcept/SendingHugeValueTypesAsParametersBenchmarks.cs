using System.Runtime.CompilerServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class SendingHugeValueTypesAsParametersBenchmarks
    {
        private const int Iterations = 1000;

        private static readonly HugeValueType[] Dates = { new HugeValueType(1, 100, 200, 300), new HugeValueType(2, 200, 300, 400) };

        [Benchmark]
        public int AsReference()
        {
            int sum = 0;
            for (int i = 0; i < Iterations; i++)
            {
                sum += SendParametersAsReference(ref Dates[0], ref Dates[1]);
            }
            return sum;
        }

        [Benchmark]
        public int AsCopy()
        {
            int sum = 0;
            for (int i = 0; i < Iterations; i++)
            {
                sum += SendParametersAsCopy(Dates[0], Dates[1]);
            }
            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SendParametersAsReference(ref HugeValueType x, ref HugeValueType y)
        {
            return x.Integer + y.Integer;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private int SendParametersAsCopy(HugeValueType x, HugeValueType y)
        {
            return x.Integer + y.Integer;
        }
    }
}