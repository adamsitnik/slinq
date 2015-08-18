using BenchmarkDotNet;

namespace Slinq.Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var competitionSwitch = new BenchmarkCompetitionSwitch(new[] 
            {
                typeof(ArrayWhereIteratorBenchmarks),
            });
            competitionSwitch.Run(args);
        }
    }
}
