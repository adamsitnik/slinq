using Slinq.Models;
using Slinq.Utils;

namespace Slinq.Extensions
{
// ReSharper disable UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments this is an API && we just follow the existing convention
    public static class ExtractedArrayExtensions
    {
        internal static short Sum(this ExtractedArray<short> source)
        {
            Contract.RequiresNotDefault(source, "source");

            short sum = 0;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    sum += source.Array[i];
                }
            }

            return sum;
        }

        internal static short? Sum(this ExtractedArray<short?> source)
        {
            Contract.RequiresNotDefault(source, "source");

            short sum = 0;
            short? current;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    current = source.Array[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        internal static short Min(this ExtractedArray<short> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var min = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (min > source.Array[i])
                {
                    min = source.Array[i];
                }
            }

            return min;
        }

        internal static short? Min(this ExtractedArray<short?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            short? min = null;
            short? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        internal static short Max(this ExtractedArray<short> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var max = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (max < source.Array[i])
                {
                    max = source.Array[i];
                }
            }

            return max;
        }

        internal static short? Max(this ExtractedArray<short?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            short? max = null;
            short? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        internal static double Average(this ExtractedArray<short> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            return (double)Sum(source) / source.ActualLength;
        }

        internal static double? Average(this ExtractedArray<short?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.ActualLength;
        }

        internal static int Sum(this ExtractedArray<int> source)
        {
            Contract.RequiresNotDefault(source, "source");

            int sum = 0;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    sum += source.Array[i];
                }
            }

            return sum;
        }

        internal static int? Sum(this ExtractedArray<int?> source)
        {
            Contract.RequiresNotDefault(source, "source");

            int sum = 0;
            int? current;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    current = source.Array[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        internal static int Min(this ExtractedArray<int> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var min = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (min > source.Array[i])
                {
                    min = source.Array[i];
                }
            }

            return min;
        }

        internal static int? Min(this ExtractedArray<int?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            int? min = null;
            int? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        internal static int Max(this ExtractedArray<int> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var max = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (max < source.Array[i])
                {
                    max = source.Array[i];
                }
            }

            return max;
        }

        internal static int? Max(this ExtractedArray<int?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            int? max = null;
            int? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        internal static double Average(this ExtractedArray<int> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            return (double)Sum(source) / source.ActualLength;
        }

        internal static double? Average(this ExtractedArray<int?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.ActualLength;
        }

        internal static long Sum(this ExtractedArray<long> source)
        {
            Contract.RequiresNotDefault(source, "source");

            long sum = 0;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    sum += source.Array[i];
                }
            }

            return sum;
        }

        internal static long? Sum(this ExtractedArray<long?> source)
        {
            Contract.RequiresNotDefault(source, "source");

            long sum = 0;
            long? current;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    current = source.Array[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        internal static long Min(this ExtractedArray<long> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var min = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (min > source.Array[i])
                {
                    min = source.Array[i];
                }
            }

            return min;
        }

        internal static long? Min(this ExtractedArray<long?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            long? min = null;
            long? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        internal static long Max(this ExtractedArray<long> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var max = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (max < source.Array[i])
                {
                    max = source.Array[i];
                }
            }

            return max;
        }

        internal static long? Max(this ExtractedArray<long?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            long? max = null;
            long? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        internal static double Average(this ExtractedArray<long> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            return (double)Sum(source) / source.ActualLength;
        }

        internal static double? Average(this ExtractedArray<long?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.ActualLength;
        }

        internal static float Sum(this ExtractedArray<float> source)
        {
            Contract.RequiresNotDefault(source, "source");

            float sum = 0;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    sum += source.Array[i];
                }
            }

            return sum;
        }

        internal static float? Sum(this ExtractedArray<float?> source)
        {
            Contract.RequiresNotDefault(source, "source");

            float sum = 0;
            float? current;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    current = source.Array[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        internal static float Min(this ExtractedArray<float> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var min = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (min > source.Array[i])
                {
                    min = source.Array[i];
                }
            }

            return min;
        }

        internal static float? Min(this ExtractedArray<float?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            float? min = null;
            float? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        internal static float Max(this ExtractedArray<float> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var max = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (max < source.Array[i])
                {
                    max = source.Array[i];
                }
            }

            return max;
        }

        internal static float? Max(this ExtractedArray<float?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            float? max = null;
            float? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        internal static double Average(this ExtractedArray<float> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            return (double)Sum(source) / source.ActualLength;
        }

        internal static double? Average(this ExtractedArray<float?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.ActualLength;
        }

        internal static double Sum(this ExtractedArray<double> source)
        {
            Contract.RequiresNotDefault(source, "source");

            double sum = 0;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    sum += source.Array[i];
                }
            }

            return sum;
        }

        internal static double? Sum(this ExtractedArray<double?> source)
        {
            Contract.RequiresNotDefault(source, "source");

            double sum = 0;
            double? current;
            checked
            {
                for (int i = 0; i < source.ActualLength; i++)
                {
                    current = source.Array[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        internal static double Min(this ExtractedArray<double> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var min = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (min > source.Array[i])
                {
                    min = source.Array[i];
                }
            }

            return min;
        }

        internal static double? Min(this ExtractedArray<double?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            double? min = null;
            double? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        internal static double Max(this ExtractedArray<double> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var max = source.Array[0];
            for (int i = 1; i < source.ActualLength; i++)
            {
                if (max < source.Array[i])
                {
                    max = source.Array[i];
                }
            }

            return max;
        }

        internal static double? Max(this ExtractedArray<double?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            double? max = null;
            double? current;
            int i = 0;
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.ActualLength; i++)
            {
                current = source.Array[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        internal static double Average(this ExtractedArray<double> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            return (double)Sum(source) / source.ActualLength;
        }

        internal static double? Average(this ExtractedArray<double?> source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.ActualLength);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.ActualLength;
        }
// ReSharper restore UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments
    }
}