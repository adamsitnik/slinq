using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Utils;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class ArrayProviderBenchmarks
    {
        private static readonly List<int> ListOfSmallValueTypes = Enumerable.Range(1, 5).ToList();

        [Benchmark]
        public int[] GetArrayFromListReflection()
        {
#pragma warning disable 618 // I just want to show that CilGeneration Is WAY better (33x times in avg)
            return ArrayProvider<int>.GetWrappedArrayWithReflection(ListOfSmallValueTypes);
#pragma warning restore 618
        }

        [Benchmark]
        public int[] GetArrayFromListDynamicCIL()
        {
            return ArrayProvider<int>.GetWrappedArray(ListOfSmallValueTypes);
        }
    }
}