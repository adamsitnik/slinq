using System;
using System.Collections.Generic;
using Slinq.Iterators;
using Slinq.Utils;

namespace Slinq.Extensions
{
// ReSharper disable UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments, MaximumChainedReferences this is an API && we just follow the existing convention
    public static class ListExtensions
    {
        public static WhereIterator<T> Where<T>(this List<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Where(predicate);
        }

        public static SelectIterator<TSource, TResult> Select<TSource, TResult>(
            this List<TSource> source,
            Func<TSource, TResult> selector)
        {
            return ArrayProvider<TSource>.Extract(source).Select(selector);
        }

        public static bool Any<T>(this List<T> source)
        {
            return ArrayProvider<T>.Extract(source).Any();
        }

        public static bool Any<T>(this List<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Any(predicate);
        }

        public static bool All<T>(this List<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).All(predicate);
        }

        public static int Count<T>(this List<T> source)
        {
            return ArrayProvider<T>.Extract(source).Count();
        }

        public static int Count<T>(this List<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Count(predicate);
        }

        public static bool Contains<T>(this List<T> source, T item)
        {
            return ArrayProvider<T>.Extract(source).Contains(item);
        }

        public static bool Contains<T>(this List<T> source, T item, IEqualityComparer<T> equalityComparer)
        {
            return ArrayProvider<T>.Extract(source).Contains(item, equalityComparer);
        }

        public static T Aggregate<T>(this List<T> source, Func<T, T, T> aggregator)
        {
            return ArrayProvider<T>.Extract(source).Aggregate(aggregator);
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
            this List<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> aggregator)
        {
            return ArrayProvider<TSource>.Extract(source).Aggregate(seed, aggregator);
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(
            this List<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> aggregator,
            Func<TAccumulate, TResult> resultSelector)
        {
            return ArrayProvider<TSource>.Extract(source).Aggregate(seed, aggregator, resultSelector);
        }

        public static T First<T>(this List<T> source)
        {
            return ArrayProvider<T>.Extract(source).First();
        }

        public static T First<T>(this List<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).First(predicate);
        }

        public static T FirstOrDefault<T>(this List<T> source)
        {
            return ArrayProvider<T>.Extract(source).FirstOrDefault();
        }

        public static T FirstOrDefault<T>(this List<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).FirstOrDefault(predicate);
        }

        public static T Last<T>(this List<T> source)
        {
            return ArrayProvider<T>.Extract(source).Last();
        }

        public static T Last<T>(this List<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Last(predicate);
        }

        public static T LastOrDefault<T>(this List<T> source)
        {
            return ArrayProvider<T>.Extract(source).LastOrDefault();
        }

        public static T LastOrDefault<T>(this List<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).LastOrDefault(predicate);
        }

        public static T ElementAt<T>(this List<T> source, int index)
        {
            return ArrayProvider<T>.Extract(source).ElementAt(index);
        }

        public static T ElementAtOrDefault<T>(this List<T> source, int index)
        {
            return ArrayProvider<T>.Extract(source).ElementAtOrDefault(index);
        }

        public static short Sum(this List<short> source)
        {
            return ArrayProvider<short>.Extract(source).Sum();
        }

        public static short? Sum(this List<short?> source)
        {
            return ArrayProvider<short?>.Extract(source).Sum();
        }

        public static short Min(this List<short> source)
        {
            return ArrayProvider<short>.Extract(source).Min();
        }

        public static short? Min(this List<short?> source)
        {
            return ArrayProvider<short?>.Extract(source).Min();
        }

        public static short Max(this List<short> source)
        {
            return ArrayProvider<short>.Extract(source).Max();
        }

        public static short? Max(this List<short?> source)
        {
            return ArrayProvider<short?>.Extract(source).Max();
        }

        public static double Average(this List<short> source)
        {
            return ArrayProvider<short>.Extract(source).Average();
        }

        public static double? Average(this List<short?> source)
        {
            return ArrayProvider<short?>.Extract(source).Average();
        }

        public static int Sum(this List<int> source)
        {
            return ArrayProvider<int>.Extract(source).Sum();
        }

        public static int? Sum(this List<int?> source)
        {
            return ArrayProvider<int?>.Extract(source).Sum();
        }

        public static int Min(this List<int> source)
        {
            return ArrayProvider<int>.Extract(source).Min();
        }

        public static int? Min(this List<int?> source)
        {
            return ArrayProvider<int?>.Extract(source).Min();
        }

        public static int Max(this List<int> source)
        {
            return ArrayProvider<int>.Extract(source).Max();
        }

        public static int? Max(this List<int?> source)
        {
            return ArrayProvider<int?>.Extract(source).Max();
        }

        public static double Average(this List<int> source)
        {
            return ArrayProvider<int>.Extract(source).Average();
        }

        public static double? Average(this List<int?> source)
        {
            return ArrayProvider<int?>.Extract(source).Average();
        }

        public static long Sum(this List<long> source)
        {
            return ArrayProvider<long>.Extract(source).Sum();
        }

        public static long? Sum(this List<long?> source)
        {
            return ArrayProvider<long?>.Extract(source).Sum();
        }

        public static long Min(this List<long> source)
        {
            return ArrayProvider<long>.Extract(source).Min();
        }

        public static long? Min(this List<long?> source)
        {
            return ArrayProvider<long?>.Extract(source).Min();
        }

        public static long Max(this List<long> source)
        {
            return ArrayProvider<long>.Extract(source).Max();
        }

        public static long? Max(this List<long?> source)
        {
            return ArrayProvider<long?>.Extract(source).Max();
        }

        public static double Average(this List<long> source)
        {
            return ArrayProvider<long>.Extract(source).Average();
        }

        public static double? Average(this List<long?> source)
        {
            return ArrayProvider<long?>.Extract(source).Average();
        }

        public static float Sum(this List<float> source)
        {
            return ArrayProvider<float>.Extract(source).Sum();
        }

        public static float? Sum(this List<float?> source)
        {
            return ArrayProvider<float?>.Extract(source).Sum();
        }

        public static float Min(this List<float> source)
        {
            return ArrayProvider<float>.Extract(source).Min();
        }

        public static float? Min(this List<float?> source)
        {
            return ArrayProvider<float?>.Extract(source).Min();
        }

        public static float Max(this List<float> source)
        {
            return ArrayProvider<float>.Extract(source).Max();
        }

        public static float? Max(this List<float?> source)
        {
            return ArrayProvider<float?>.Extract(source).Max();
        }

        public static double Average(this List<float> source)
        {
            return ArrayProvider<float>.Extract(source).Average();
        }

        public static double? Average(this List<float?> source)
        {
            return ArrayProvider<float?>.Extract(source).Average();
        }

        public static double Sum(this List<double> source)
        {
            return ArrayProvider<double>.Extract(source).Sum();
        }

        public static double? Sum(this List<double?> source)
        {
            return ArrayProvider<double?>.Extract(source).Sum();
        }

        public static double Min(this List<double> source)
        {
            return ArrayProvider<double>.Extract(source).Min();
        }

        public static double? Min(this List<double?> source)
        {
            return ArrayProvider<double?>.Extract(source).Min();
        }

        public static double Max(this List<double> source)
        {
            return ArrayProvider<double>.Extract(source).Max();
        }

        public static double? Max(this List<double?> source)
        {
            return ArrayProvider<double?>.Extract(source).Max();
        }

        public static double Average(this List<double> source)
        {
            return ArrayProvider<double>.Extract(source).Average();
        }

        public static double? Average(this List<double?> source)
        {
            return ArrayProvider<double?>.Extract(source).Average();
        }
    }
// ReSharper restore UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments, MaximumChainedReferences
}