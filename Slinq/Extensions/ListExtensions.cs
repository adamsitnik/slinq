using System;
using System.Collections.Generic;
using Slinq.Iterators;
using Slinq.Utils;

namespace Slinq.Extensions
{
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
    }
}