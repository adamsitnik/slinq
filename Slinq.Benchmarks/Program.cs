using BenchmarkDotNet;
using Slinq.Benchmarks.Extensions;
using Slinq.Benchmarks.Iterators;
using Slinq.Benchmarks.ProofsOfConcept;

namespace Slinq.Benchmarks
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var competitionSwitch = new BenchmarkCompetitionSwitch(new[] 
            {
                /*
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
                 */
                typeof(SortingExtensionsBenchmarks),
                typeof(ValueTypesSortingBenchmarks),
                typeof(SendingHugeValueTypesAsParametersBenchmarks),
                typeof(ReferenceTypesSortingBenchmarks),
                typeof(ArraySortersBenchmarks)
            });
            competitionSwitch.Run(args);
        }
    }
}
