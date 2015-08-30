using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 3)]
    public class ValueTypesSortingBenchmarks
    {
        [Benchmark]
        public DateTime[] SortDateTimes_SingleComparerThatUsesFewFields_Slinq()
        {
            return SortingExtensions.Sort(DataGenerator.GenerateRandomDates(), new DateTimeComparer());
        }

        [Benchmark]
        public DateTime[] SortDateTimes_FewLambdas_Slinq()
        {
            return SortingExtensions.OrderBy(DataGenerator.GenerateRandomDates(), date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }

        [Benchmark]
        public DateTime[] SortDateTimes_FewLambdas_Linq()
        {
            return Enumerable.OrderBy(DataGenerator.GenerateRandomDates(), date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }

        private class DateTimeComparer : IComparer<DateTime>
        {
            public int Compare(DateTime x, DateTime y)
            {
                int years = x.Year.CompareTo(y.Year);
                if (years != 0)
                {
                    return years;
                }

                int months = x.Month.CompareTo(y.Month);
                if (months != 0)
                {
                    return months;
                }

                int days = x.Day.CompareTo(y.Day);
                if (days != 0)
                {
                    return days;
                }

                return 0;
            }
        }
    }
}