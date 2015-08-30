using System.Runtime.CompilerServices;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    /// <summary>
    /// the purpose of this benchmark is to show in which direction every Iterator should go: simple while(MoveNext()) or for loop
    /// </summary>
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 3)]
    public class ForLoopVsWhileBenchmarks
    {
        private static readonly int[] Numbers = DataGenerator.GenerateRandomNumbers(1000);

        [Benchmark]
        public int For()
        {
            return SumFor(Numbers);
        }

        [Benchmark]
        public int While_PreIncrement()
        {
            return SumWhile_PreIncrement(Numbers);
        }

        [Benchmark]
        public int While_PostIncrement()
        {
            return SumWhile_PostIncrement(Numbers);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumFor(int[] numbers)
        {
            int sum = 0;

            // I did not use numbers.Length on purpose because iterators are not going to be dependent on length, but on some custom int
            for (int i = 0; i < 1000; i++)
            {
                sum += numbers[i];
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumWhile_PreIncrement(int[] numbers)
        {
            int sum = 0;
            int i = 0;
            while (i < 1000)
            {
                sum += numbers[i];
                ++i;
            }

            return sum;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static int SumWhile_PostIncrement(int[] numbers)
        {
            int sum = 0;
            int i = 0;
            while (i < 1000)
            {
                sum += numbers[i++];
            }

            return sum;
        }
    }
}