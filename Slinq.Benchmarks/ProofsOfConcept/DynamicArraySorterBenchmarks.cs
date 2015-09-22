using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Abstract;
using Slinq.Utils;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class DynamicArraySorterBenchmarks
    {
        private static readonly IArraySorter<DateTime> DynamicDateTimeSorter = DedicatedSortersFactory.CreateDedicatedSorter<DateTime>();
        private static readonly IArraySorter<int> DynamicIntegerSorter = DedicatedSortersFactory.CreateDedicatedSorter<int>();
        private static readonly IArraySorter<HugeValueType> DynamicHugeValueTypeSorter = DedicatedSortersFactory.CreateDedicatedSorter<HugeValueType>();

        /// <summary>
        /// https://github.com/dotnet/coreclr/blob/4cf8a6b082d9bb1789facd996d8265d3908757b2/src/classlibnative/bcltype/arrayhelpers.cpp
        /// </summary>
        /// <returns></returns>
        [Benchmark]
        public DateTime[] DateTime_ArraySort()
        {
            var dates = DataGenerator.GenerateRandomDates();

            Array.Sort(dates);

            return dates;
        }

        [Benchmark]
        public DateTime[] DateTime_CopiedSort()
        {
            var dates = DataGenerator.GenerateRandomDates();

            new SystemSorter<DateTime>().Sort(dates, 0, dates.Length - 1);

            return dates;
        }

        [Benchmark]
        public DateTime[] DateTime_DedicatedDynamicSorter()
        {
            var dates = DataGenerator.GenerateRandomDates();

            DynamicDateTimeSorter.Sort(dates);

            return dates;
        }

        [Benchmark]
        public int[] Int_ArraySort()
        {
            var numbers = DataGenerator.GenerateRandomNumbers();

            Array.Sort(numbers);

            return numbers;
        }

        [Benchmark]
        public int[] Int_SystemSort()
        {
            var numbers = DataGenerator.GenerateRandomNumbers();

            new SystemSorter<int>().Sort(numbers, 0, numbers.Length - 1);

            return numbers;
        }

        [Benchmark]
        public int[] Int_DedicatedDynamicSorter()
        {
            var numbers = DataGenerator.GenerateRandomNumbers();

            DynamicIntegerSorter.Sort(numbers);

            return numbers;
        }

        [Benchmark]
        public HugeValueType[] HugeValueType_ArraySort()
        {
            var numbers = DataGenerator.GenerateHugeValueTypes();

            Array.Sort(numbers);

            return numbers;
        }

        [Benchmark]
        public HugeValueType[] HugeValueType_SystemSort()
        {
            var numbers = DataGenerator.GenerateHugeValueTypes();

            new SystemSorter<HugeValueType>().Sort(numbers, 0, numbers.Length - 1);

            return numbers;
        }

        [Benchmark]
        public HugeValueType[] HugeValueType_DedicatedDynamicSorter()
        {
            var numbers = DataGenerator.GenerateHugeValueTypes();

            DynamicHugeValueTypeSorter.Sort(numbers);

            return numbers;
        }
    }
}