using BenchmarkDotNet;
using Slinq.Benchmarks.Iterators;
using Slinq.Benchmarks.Utils;

namespace Slinq.Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var competitionSwitch = new BenchmarkCompetitionSwitch(new[] 
            {
                typeof(ArrayWhereIteratorBenchmarks),
                typeof(ArrayWhereSelectIteratorBenchmarks),
                typeof(ListWhereIteratorBenchmarks),
                typeof(ReadOnlyCollectionWhereIteratorBenchmark),
                typeof(ArrayProviderBenchmarks)
            });
            competitionSwitch.Run(args);
        }
    }
}
