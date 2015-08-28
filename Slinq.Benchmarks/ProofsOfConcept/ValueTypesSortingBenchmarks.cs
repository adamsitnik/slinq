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
            return SortingExtensions.Sort(GenerateRandomDates(), new DateTimeComparer());
        }

        [Benchmark]
        public DateTime[] SortDateTimes_FewLambdas_Slinq()
        {
            return SortingExtensions.OrderBy(GenerateRandomDates(), date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }

        [Benchmark]
        public DateTime[] SortDateTimes_FewLambdas_Linq()
        {
            return Enumerable.OrderBy(GenerateRandomDates(), date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();
        }

        private static DateTime[] GenerateRandomDates()
        {
            var random = new Random();

            return Enumerable.Range(1, 1000)
                .Select(_ => new DateTime(random.Next(2000, 2015), random.Next(1, 12), random.Next(1, 28)))
                .ToArray();
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