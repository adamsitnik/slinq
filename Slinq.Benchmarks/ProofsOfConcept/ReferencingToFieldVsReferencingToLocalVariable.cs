using System.Linq;
using BenchmarkDotNet;
using BenchmarkDotNet.Tasks;

namespace Slinq.Benchmarks.ProofsOfConcept
{
    [BenchmarkTask(platform: BenchmarkPlatform.X86, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.LegacyJit, warmupIterationCount: 2, targetIterationCount: 3)]
    [BenchmarkTask(platform: BenchmarkPlatform.X64, jitVersion: BenchmarkJitVersion.RyuJit, warmupIterationCount: 2, targetIterationCount: 3)]
    public class ReferencingToFieldVsReferencingToLocalVariableAndReadOnlyVsMutable
    {
        private static readonly int[] Numbers = Enumerable.Range(1, 100).ToArray();

        [Benchmark]
        public int AccessReadOnlyViaField()
        {
            return AccessViaField(new ReadOnlyArrayWrapper<int>(Numbers, Numbers.Length));
        }

        [Benchmark]
        public int AccessReadOnlyViaLocalVariable()
        {
            return AccessViaLocalVariable(new ReadOnlyArrayWrapper<int>(Numbers, Numbers.Length));
        }

        [Benchmark]
        public int AccessMutableViaField()
        {
            return AccessViaField(new MutableArrayWrapper<int>(Numbers, Numbers.Length));
        }

        [Benchmark]
        public int AccessMutableViaLocalVariable()
        {
            return AccessViaLocalVariable(new MutableArrayWrapper<int>(Numbers, Numbers.Length));
        }

        private static int AccessViaField(ReadOnlyArrayWrapper<int> arrayWrapper)
        {
            int sum = 0;
            for (int i = 0; i < arrayWrapper.Length; i++)
            {
                sum += arrayWrapper.Array[i];
            }

            return sum;
        }

        private static int AccessViaLocalVariable(ReadOnlyArrayWrapper<int> arrayWrapper)
        {
            int sum = 0;
            int length = arrayWrapper.Length;
            int[] array = arrayWrapper.Array;
            for (int i = 0; i < length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        private static int AccessViaField(MutableArrayWrapper<int> arrayWrapper)
        {
            int sum = 0;
            for (int i = 0; i < arrayWrapper.Length; i++)
            {
                sum += arrayWrapper.Array[i];
            }

            return sum;
        }

        private static int AccessViaLocalVariable(MutableArrayWrapper<int> arrayWrapper)
        {
            int sum = 0;
            int length = arrayWrapper.Length;
            int[] array = arrayWrapper.Array;
            for (int i = 0; i < length; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        private struct ReadOnlyArrayWrapper<T>
        {
            internal readonly T[] Array;
            internal readonly int Length;

            public ReadOnlyArrayWrapper(T[] array, int length)
            {
                Array = array;
                Length = length;
            }
        }

        private struct MutableArrayWrapper<T>
        {
            internal T[] Array;
            internal int Length;

            public MutableArrayWrapper(T[] array, int length)
            {
                Array = array;
                Length = length;
            }
        }
    }
}