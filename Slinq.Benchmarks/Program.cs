using BenchmarkDotNet;
using Slinq.Benchmarks.Extensions;
using Slinq.Benchmarks.Iterators;
using Slinq.Benchmarks.ProofsOfConcept;
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
                typeof(ListWhereIteratorBenchmarks),
                typeof(ReadOnlyCollectionWhereIteratorBenchmark),
                typeof(ArrayWhereSelectIteratorBenchmarks),
                typeof(ListWhereIteratorBenchmarks),
                typeof(ReadOnlyCollectionWhereSelectIteratorBenchmark),
                typeof(RangeSelectIteratorBenchmarks),
                typeof(ArraySelectWhereIteratorBenchmarks),
                typeof(ArrayProviderBenchmarks),
                typeof(MathExtensionsBenchmarks),
                typeof(ReferencingToFieldVsReferencingToLocalVariableAndReadOnlyVsMutable),
                typeof(BoundariesCheckEliminationBenchmarks),
                typeof(ExtractedArrayMathExtensionsBenchmarks),
                typeof(ManualLoopUnrollingBenchmarks),
                typeof(MinManualLoopingBenchmarks),
                typeof(ForLoopVsWhileBenchmarks),
                typeof(WhereLastBenchmark),
                typeof(SortingExtensionsBenchmarks)
            });
            competitionSwitch.Run(args);
        }
    }
}
