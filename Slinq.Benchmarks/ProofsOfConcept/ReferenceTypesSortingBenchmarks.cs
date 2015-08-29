using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;
using Slinq.Extensions;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 5)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 5)]
    public class ReferenceTypesSortingBenchmarks
    {
        [Benchmark]
        public MyDateTime[] SortDateTimes_SingleComparerThatUsesFewFields_Slinq()
        {
            return SortingExtensions.Sort(GenerateRandomDates(), new MyDateTimeComparer());
        }

        [Benchmark]
        public MyDateTime[] SortDateTimes_FewLambdas_Slinq()
        {
            return
                SortingExtensions.OrderBy(GenerateRandomDates(), date => date.Year)
                                 .ThenBy(date => date.Month)
                                 .ThenBy(date => date.Day)
                                 .ToArray();
        }

        [Benchmark]
        public MyDateTime[] SortDateTimes_FewLambdas_Linq()
        {
            return
                Enumerable.OrderBy(GenerateRandomDates(), date => date.Year)
                          .ThenBy(date => date.Month)
                          .ThenBy(date => date.Day)
                          .ToArray();
        }

        private static MyDateTime[] GenerateRandomDates()
        {
            var random = new Random();

            return Enumerable.Range(1, 1000)
                             .Select(
                                 _ => new MyDateTime(random.Next(2000, 2015), random.Next(1, 12), random.Next(1, 28)))
                             .ToArray();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "It is visible because Benchmark must return public types")]
        public class MyDateTime
        {
            public MyDateTime(int year, int month, int day)
            {
                Year = year;
                Month = month;
                Day = day;
            }

            internal int Year { get; private set; }

            internal int Month { get; private set; }

            internal int Day { get; private set; }
        }

        private class MyDateTimeComparer : IComparer<MyDateTime>
        {
            public int Compare(MyDateTime x, MyDateTime y)
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