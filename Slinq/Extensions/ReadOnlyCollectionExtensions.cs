﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Slinq.Iterators;
using Slinq.Utils;

namespace Slinq.Extensions
{
    /// <summary>
    /// supports only Array/List based ReadOnlyCollections
    /// </summary>
    // ReSharper disable UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments, MaximumChainedReferences this is an API && we just follow the existing convention
    public static class ReadOnlyCollectionExtensions
    {
        public static WhereIterator<T> Where<T>(this ReadOnlyCollection<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Where(predicate);
        }

        public static SelectIterator<TSource, TResult> Select<TSource, TResult>(
            this ReadOnlyCollection<TSource> source,
            Func<TSource, TResult> selector)
        {
            return ArrayProvider<TSource>.Extract(source).Select(selector);
        }

        public static bool Any<T>(this ReadOnlyCollection<T> source)
        {
            return ArrayProvider<T>.Extract(source).Any();
        }

        public static bool Any<T>(this ReadOnlyCollection<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Any(predicate);
        }

        public static bool All<T>(this ReadOnlyCollection<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).All(predicate);
        }

        public static int Count<T>(this ReadOnlyCollection<T> source)
        {
            return ArrayProvider<T>.Extract(source).Count();
        }

        public static int Count<T>(this ReadOnlyCollection<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Count(predicate);
        }

        public static bool Contains<T>(this ReadOnlyCollection<T> source, T item)
        {
            return ArrayProvider<T>.Extract(source).Contains(item);
        }

        public static bool Contains<T>(this ReadOnlyCollection<T> source, T item, IEqualityComparer<T> equalityComparer)
        {
            return ArrayProvider<T>.Extract(source).Contains(item, equalityComparer);
        }

        public static T Aggregate<T>(this ReadOnlyCollection<T> source, Func<T, T, T> aggregator)
        {
            return ArrayProvider<T>.Extract(source).Aggregate(aggregator);
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
            this ReadOnlyCollection<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> aggregator)
        {
            return ArrayProvider<TSource>.Extract(source).Aggregate(seed, aggregator);
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(
            this ReadOnlyCollection<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> aggregator,
            Func<TAccumulate, TResult> resultSelector)
        {
            return ArrayProvider<TSource>.Extract(source).Aggregate(seed, aggregator, resultSelector);
        }

        public static T First<T>(this ReadOnlyCollection<T> source)
        {
            return ArrayProvider<T>.Extract(source).First();
        }

        public static T First<T>(this ReadOnlyCollection<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).First(predicate);
        }

        public static T FirstOrDefault<T>(this ReadOnlyCollection<T> source)
        {
            return ArrayProvider<T>.Extract(source).FirstOrDefault();
        }

        public static T FirstOrDefault<T>(this ReadOnlyCollection<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).FirstOrDefault(predicate);
        }

        public static T Last<T>(this ReadOnlyCollection<T> source)
        {
            return ArrayProvider<T>.Extract(source).Last();
        }

        public static T Last<T>(this ReadOnlyCollection<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).Last(predicate);
        }

        public static T LastOrDefault<T>(this ReadOnlyCollection<T> source)
        {
            return ArrayProvider<T>.Extract(source).LastOrDefault();
        }

        public static T LastOrDefault<T>(this ReadOnlyCollection<T> source, Predicate<T> predicate)
        {
            return ArrayProvider<T>.Extract(source).LastOrDefault(predicate);
        }

        public static T ElementAt<T>(this ReadOnlyCollection<T> source, int index)
        {
            return ArrayProvider<T>.Extract(source).ElementAt(index);
        }

        public static T ElementAtOrDefault<T>(this ReadOnlyCollection<T> source, int index)
        {
            return ArrayProvider<T>.Extract(source).ElementAtOrDefault(index);
        }

        public static short Sum(this ReadOnlyCollection<short> source)
        {
            return ArrayProvider<short>.Extract(source).Sum();
        }

        public static short? Sum(this ReadOnlyCollection<short?> source)
        {
            return ArrayProvider<short?>.Extract(source).Sum();
        }

        public static short Min(this ReadOnlyCollection<short> source)
        {
            return ArrayProvider<short>.Extract(source).Min();
        }

        public static short? Min(this ReadOnlyCollection<short?> source)
        {
            return ArrayProvider<short?>.Extract(source).Min();
        }

        public static short Max(this ReadOnlyCollection<short> source)
        {
            return ArrayProvider<short>.Extract(source).Max();
        }

        public static short? Max(this ReadOnlyCollection<short?> source)
        {
            return ArrayProvider<short?>.Extract(source).Max();
        }

        public static double Average(this ReadOnlyCollection<short> source)
        {
            return ArrayProvider<short>.Extract(source).Average();
        }

        public static double? Average(this ReadOnlyCollection<short?> source)
        {
            return ArrayProvider<short?>.Extract(source).Average();
        }

        public static int Sum(this ReadOnlyCollection<int> source)
        {
            return ArrayProvider<int>.Extract(source).Sum();
        }

        public static int? Sum(this ReadOnlyCollection<int?> source)
        {
            return ArrayProvider<int?>.Extract(source).Sum();
        }

        public static int Min(this ReadOnlyCollection<int> source)
        {
            return ArrayProvider<int>.Extract(source).Min();
        }

        public static int? Min(this ReadOnlyCollection<int?> source)
        {
            return ArrayProvider<int?>.Extract(source).Min();
        }

        public static int Max(this ReadOnlyCollection<int> source)
        {
            return ArrayProvider<int>.Extract(source).Max();
        }

        public static int? Max(this ReadOnlyCollection<int?> source)
        {
            return ArrayProvider<int?>.Extract(source).Max();
        }

        public static double Average(this ReadOnlyCollection<int> source)
        {
            return ArrayProvider<int>.Extract(source).Average();
        }

        public static double? Average(this ReadOnlyCollection<int?> source)
        {
            return ArrayProvider<int?>.Extract(source).Average();
        }

        public static long Sum(this ReadOnlyCollection<long> source)
        {
            return ArrayProvider<long>.Extract(source).Sum();
        }

        public static long? Sum(this ReadOnlyCollection<long?> source)
        {
            return ArrayProvider<long?>.Extract(source).Sum();
        }

        public static long Min(this ReadOnlyCollection<long> source)
        {
            return ArrayProvider<long>.Extract(source).Min();
        }

        public static long? Min(this ReadOnlyCollection<long?> source)
        {
            return ArrayProvider<long?>.Extract(source).Min();
        }

        public static long Max(this ReadOnlyCollection<long> source)
        {
            return ArrayProvider<long>.Extract(source).Max();
        }

        public static long? Max(this ReadOnlyCollection<long?> source)
        {
            return ArrayProvider<long?>.Extract(source).Max();
        }

        public static double Average(this ReadOnlyCollection<long> source)
        {
            return ArrayProvider<long>.Extract(source).Average();
        }

        public static double? Average(this ReadOnlyCollection<long?> source)
        {
            return ArrayProvider<long?>.Extract(source).Average();
        }

        public static float Sum(this ReadOnlyCollection<float> source)
        {
            return ArrayProvider<float>.Extract(source).Sum();
        }

        public static float? Sum(this ReadOnlyCollection<float?> source)
        {
            return ArrayProvider<float?>.Extract(source).Sum();
        }

        public static float Min(this ReadOnlyCollection<float> source)
        {
            return ArrayProvider<float>.Extract(source).Min();
        }

        public static float? Min(this ReadOnlyCollection<float?> source)
        {
            return ArrayProvider<float?>.Extract(source).Min();
        }

        public static float Max(this ReadOnlyCollection<float> source)
        {
            return ArrayProvider<float>.Extract(source).Max();
        }

        public static float? Max(this ReadOnlyCollection<float?> source)
        {
            return ArrayProvider<float?>.Extract(source).Max();
        }

        public static double Average(this ReadOnlyCollection<float> source)
        {
            return ArrayProvider<float>.Extract(source).Average();
        }

        public static double? Average(this ReadOnlyCollection<float?> source)
        {
            return ArrayProvider<float?>.Extract(source).Average();
        }

        public static double Sum(this ReadOnlyCollection<double> source)
        {
            return ArrayProvider<double>.Extract(source).Sum();
        }

        public static double? Sum(this ReadOnlyCollection<double?> source)
        {
            return ArrayProvider<double?>.Extract(source).Sum();
        }

        public static double Min(this ReadOnlyCollection<double> source)
        {
            return ArrayProvider<double>.Extract(source).Min();
        }

        public static double? Min(this ReadOnlyCollection<double?> source)
        {
            return ArrayProvider<double?>.Extract(source).Min();
        }

        public static double Max(this ReadOnlyCollection<double> source)
        {
            return ArrayProvider<double>.Extract(source).Max();
        }

        public static double? Max(this ReadOnlyCollection<double?> source)
        {
            return ArrayProvider<double?>.Extract(source).Max();
        }

        public static double Average(this ReadOnlyCollection<double> source)
        {
            return ArrayProvider<double>.Extract(source).Average();
        }

        public static double? Average(this ReadOnlyCollection<double?> source)
        {
            return ArrayProvider<double?>.Extract(source).Average();
        }
    }
// ReSharper restore UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments, MaximumChainedReferences
}
