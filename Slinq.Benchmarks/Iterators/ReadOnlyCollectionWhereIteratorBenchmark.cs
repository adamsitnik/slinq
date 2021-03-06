﻿using System.Collections.ObjectModel;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;

namespace Slinq.Benchmarks.Iterators
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class ReadOnlyCollectionWhereIteratorBenchmark
    {
        private static readonly ReadOnlyCollection<int> Numbers = new ReadOnlyCollection<int>(DataGenerator.GenerateRandomNumbers(1000));

        [Benchmark]
        public int IterateOverWhereResult_Struct()
        {
            int sum = 0;
            foreach (var number in ReadOnlyCollectionExtensions.Where(Numbers, number => number % 2 == 0))
            {
                sum += number;
            }

            return sum;
        }

        [Benchmark]
        public int IterateOverWhereResult_MS()
        {
            int sum = 0;
            foreach (var number in Enumerable.Where(Numbers, number => number % 2 == 0))
            {
                sum += number;
            }

            return sum;
        }
    }
}