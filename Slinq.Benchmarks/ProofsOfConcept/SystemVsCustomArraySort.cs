using System;
using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Utils;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class SystemArraySort
    {
        private readonly int[] _smallValueTypes = DataGenerator.GenerateRandomNumbers(1000);

        [Benchmark]
        public int[] Integers()
        {
            Array.Sort(_smallValueTypes);

            return _smallValueTypes;
        }
    }

    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Better readability")]
    public class OptimizedSystemArraySort
    {
        private readonly int[] _smallValueTypes = DataGenerator.GenerateRandomNumbers(1000);

        [Benchmark]
        public int[] Integers()
        {
            IntsArraySorter.IntrospectiveSort(_smallValueTypes, 0, _smallValueTypes.Length - 1);

            return _smallValueTypes;
        }
    }
}