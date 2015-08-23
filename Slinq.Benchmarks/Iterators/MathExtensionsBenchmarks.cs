using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;

namespace Slinq.Benchmarks.Iterators
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class MathExtensionsBenchmarks
    {
        private static readonly int[] Array = Enumerable.Range(1, 1000).ToArray();
        private static readonly List<int> List = Enumerable.Range(1, 1000).ToArray().ToList();
        private static readonly ReadOnlyCollection<int> ReadOnlyCollection = new ReadOnlyCollection<int>(Enumerable.Range(1, 1000).ToArray());

        [Benchmark]
        public int SumArray_Linq()
        {
            return Enumerable.Sum(Array);
        }

        [Benchmark]
        public int SumArray_Slinq()
        {
            return ArrayExtensions.Sum(Array);
        }

        [Benchmark]
        public int SumList_Linq()
        {
            return Enumerable.Sum(List);
        }

        [Benchmark]
        public int SumList_Slinq()
        {
            return ListExtensions.Sum(List);
        }

        [Benchmark]
        public int SumReadOnlyCollection_Linq()
        {
            return Enumerable.Sum(ReadOnlyCollection);
        }

        [Benchmark]
        public int SumReadOnlyCollection_Slinq()
        {
            return ReadOnlyCollectionExtensions.Sum(ReadOnlyCollection);
        }

        [Benchmark]
        public int MinArray_Linq()
        {
            return Enumerable.Min(Array);
        }

        [Benchmark]
        public int MinArray_Slinq()
        {
            return ArrayExtensions.Min(Array);
        }

        [Benchmark]
        public int MinList_Linq()
        {
            return Enumerable.Min(List);
        }

        [Benchmark]
        public int MinList_Slinq()
        {
            return ListExtensions.Min(List);
        }

        [Benchmark]
        public int MinReadOnlyCollection_Linq()
        {
            return Enumerable.Min(ReadOnlyCollection);
        }

        [Benchmark]
        public int MinReadOnlyCollection_Slinq()
        {
            return ReadOnlyCollectionExtensions.Min(ReadOnlyCollection);
        }
    }
}