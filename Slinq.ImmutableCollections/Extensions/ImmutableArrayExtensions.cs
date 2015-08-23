using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Slinq.ImmutableCollections.Utils;
using Slinq.Iterators;

namespace Slinq.Extensions
{
// ReSharper disable UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments, MaximumChainedReferences this is an API && we just follow the existing convention
    public static class ImmutableArrayExtensions
    {
        public static WhereIterator<T> Where<T>(this ImmutableArray<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Where(predicate);
        }

        public static SelectIterator<TSource, TResult> Select<TSource, TResult>(
            this ImmutableArray<TSource> source,
            Func<TSource, TResult> selector)
        {
            return ArrayProvider<TSource>.Extract(source).Select(selector);
        }

        public static bool Any<T>(this ImmutableArray<T> source)
        {
            return ArrayProvider<T>.Extract(source).Any();
        }

        public static bool Any<T>(this ImmutableArray<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Any(predicate);
        }

        public static bool All<T>(this ImmutableArray<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).All(predicate);
        }

        public static int Count<T>(this ImmutableArray<T> source)
        {
            return ArrayProvider<T>.Extract(source).Count();
        }

        public static int Count<T>(this ImmutableArray<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Count(predicate);
        }

        public static bool Contains<T>(this ImmutableArray<T> source, T item)
        {
            return ArrayProvider<T>.Extract(source).Contains(item);
        }

        public static bool Contains<T>(this ImmutableArray<T> source, T item, IEqualityComparer<T> equalityComparer)
        {
            return ArrayProvider<T>.Extract(source).Contains(item, equalityComparer);
        }

        public static T Aggregate<T>(this ImmutableArray<T> source, Func<T, T, T> aggregator)
        {
            return ArrayProvider<T>.Extract(source).Aggregate(aggregator);
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
            this ImmutableArray<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> aggregator)
        {
            return ArrayProvider<TSource>.Extract(source).Aggregate(seed, aggregator);
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(
            this ImmutableArray<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> aggregator,
            Func<TAccumulate, TResult> resultSelector)
        {
            return ArrayProvider<TSource>.Extract(source).Aggregate(seed, aggregator, resultSelector);
        }

        public static T First<T>(this ImmutableArray<T> source)
        {
            return ArrayProvider<T>.Extract(source).First();
        }

        public static T First<T>(this ImmutableArray<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).First(predicate);
        }

        public static T FirstOrDefault<T>(this ImmutableArray<T> source)
        {
            return ArrayProvider<T>.Extract(source).FirstOrDefault();
        }

        public static T FirstOrDefault<T>(this ImmutableArray<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).FirstOrDefault(predicate);
        }

        public static T Last<T>(this ImmutableArray<T> source)
        {
            return ArrayProvider<T>.Extract(source).Last();
        }

        public static T Last<T>(this ImmutableArray<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Last(predicate);
        }

        public static T LastOrDefault<T>(this ImmutableArray<T> source)
        {
            return ArrayProvider<T>.Extract(source).LastOrDefault();
        }

        public static T LastOrDefault<T>(this ImmutableArray<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).LastOrDefault(predicate);
        }

        public static T ElementAt<T>(this ImmutableArray<T> source, int index)
        {
            return ArrayProvider<T>.Extract(source).ElementAt(index);
        }

        public static T ElementAtOrDefault<T>(this ImmutableArray<T> source, int index)
        {
            return ArrayProvider<T>.Extract(source).ElementAtOrDefault(index);
        }

        public static short Sum(this ImmutableArray<short> source)
        {
            var extractedArray = ArrayProvider<short>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static short? Sum(this ImmutableArray<short?> source)
        {
            var extractedArray = ArrayProvider<short?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static short Min(this ImmutableArray<short> source)
        {
            var extractedArray = ArrayProvider<short>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static short? Min(this ImmutableArray<short?> source)
        {
            var extractedArray = ArrayProvider<short?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static short Max(this ImmutableArray<short> source)
        {
            var extractedArray = ArrayProvider<short>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static short? Max(this ImmutableArray<short?> source)
        {
            var extractedArray = ArrayProvider<short?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static double Average(this ImmutableArray<short> source)
        {
            var extractedArray = ArrayProvider<short>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }

        public static double? Average(this ImmutableArray<short?> source)
        {
            var extractedArray = ArrayProvider<short?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }

        public static int Sum(this ImmutableArray<int> source)
        {
            var extractedArray = ArrayProvider<int>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static int? Sum(this ImmutableArray<int?> source)
        {
            var extractedArray = ArrayProvider<int?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static int Min(this ImmutableArray<int> source)
        {
            var extractedArray = ArrayProvider<int>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static int? Min(this ImmutableArray<int?> source)
        {
            var extractedArray = ArrayProvider<int?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static int Max(this ImmutableArray<int> source)
        {
            var extractedArray = ArrayProvider<int>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static int? Max(this ImmutableArray<int?> source)
        {
            var extractedArray = ArrayProvider<int?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static double Average(this ImmutableArray<int> source)
        {
            var extractedArray = ArrayProvider<int>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }

        public static double? Average(this ImmutableArray<int?> source)
        {
            var extractedArray = ArrayProvider<int?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }

        public static long Sum(this ImmutableArray<long> source)
        {
            var extractedArray = ArrayProvider<long>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static long? Sum(this ImmutableArray<long?> source)
        {
            var extractedArray = ArrayProvider<long?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static long Min(this ImmutableArray<long> source)
        {
            var extractedArray = ArrayProvider<long>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static long? Min(this ImmutableArray<long?> source)
        {
            var extractedArray = ArrayProvider<long?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static long Max(this ImmutableArray<long> source)
        {
            var extractedArray = ArrayProvider<long>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static long? Max(this ImmutableArray<long?> source)
        {
            var extractedArray = ArrayProvider<long?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static double Average(this ImmutableArray<long> source)
        {
            var extractedArray = ArrayProvider<long>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }

        public static double? Average(this ImmutableArray<long?> source)
        {
            var extractedArray = ArrayProvider<long?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }

        public static float Sum(this ImmutableArray<float> source)
        {
            var extractedArray = ArrayProvider<float>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static float? Sum(this ImmutableArray<float?> source)
        {
            var extractedArray = ArrayProvider<float?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static float Min(this ImmutableArray<float> source)
        {
            var extractedArray = ArrayProvider<float>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static float? Min(this ImmutableArray<float?> source)
        {
            var extractedArray = ArrayProvider<float?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static float Max(this ImmutableArray<float> source)
        {
            var extractedArray = ArrayProvider<float>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static float? Max(this ImmutableArray<float?> source)
        {
            var extractedArray = ArrayProvider<float?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static double Average(this ImmutableArray<float> source)
        {
            var extractedArray = ArrayProvider<float>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }

        public static double? Average(this ImmutableArray<float?> source)
        {
            var extractedArray = ArrayProvider<float?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }

        public static double Sum(this ImmutableArray<double> source)
        {
            var extractedArray = ArrayProvider<double>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static double? Sum(this ImmutableArray<double?> source)
        {
            var extractedArray = ArrayProvider<double?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Sum();
            }

            return ArrayExtensions.Sum(extractedArray.Array);
        }

        public static double Min(this ImmutableArray<double> source)
        {
            var extractedArray = ArrayProvider<double>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static double? Min(this ImmutableArray<double?> source)
        {
            var extractedArray = ArrayProvider<double?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Min();
            }

            return ArrayExtensions.Min(extractedArray.Array);
        }

        public static double Max(this ImmutableArray<double> source)
        {
            var extractedArray = ArrayProvider<double>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static double? Max(this ImmutableArray<double?> source)
        {
            var extractedArray = ArrayProvider<double?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Max();
            }

            return ArrayExtensions.Max(extractedArray.Array);
        }

        public static double Average(this ImmutableArray<double> source)
        {
            var extractedArray = ArrayProvider<double>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }

        public static double? Average(this ImmutableArray<double?> source)
        {
            var extractedArray = ArrayProvider<double?>.Extract(source);
            if (!extractedArray.IsAsLongAsSourceArray)
            {
                return extractedArray.Average();
            }

            return ArrayExtensions.Average(extractedArray.Array);
        }
    }
// ReSharper restore UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments, MaximumChainedReferences
}