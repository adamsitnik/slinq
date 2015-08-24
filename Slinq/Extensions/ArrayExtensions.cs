﻿using System;
using System.Collections.Generic;
using Slinq.Iterators;
using Slinq.Utils;

namespace Slinq.Extensions
{
// ReSharper disable UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments this is an API && we just follow the existing convention
    public static class ArrayExtensions
    {
        public static WhereIterator<T> Where<T>(this T[] array, Predicate<T> predicate)
        {
            return new WhereIterator<T>(array, array.Length, predicate);
        }

        public static SelectIterator<T, TResult> Select<T, TResult>(this T[] array, Func<T, TResult> selector)
        {
            return new SelectIterator<T, TResult>(array, array.Length, selector);
        }

        public static bool Any<T>(this T[] array)
        {
            return array.Length > 0;
        }

        public static bool Any<T>(this T[] array, Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate(array[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool All<T>(this T[] array, Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (!predicate(array[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static int Count<T>(this T[] array)
        {
            return array.Length;
        }

        public static int Count<T>(this T[] array, Predicate<T> predicate)
        {
            var count = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate(array[i]))
                {
                    checked
                    {
                        ++count;
                    }
                }
            }

            return count;
        }

        public static bool Contains<T>(this T[] array, T item)
        {
            // can not use Array.Contains because it could give false-positive 
            // if i.e. this was array taken from List and somebody would query for 0 (default(int))
            // and at the end there would be some values that match but they are not in the List for real
            return Contains(array, item, EqualityComparer<T>.Default);
        }

        public static bool Contains<T>(this T[] array, T item, IEqualityComparer<T> equalityComparer)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (equalityComparer.Equals(array[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        public static T Aggregate<T>(this T[] array, Func<T, T, T> aggregator)
        {
            if (array.Length == 0)
            {
                throw Error.NoElements();
            }

            var aggregate = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                aggregate = aggregator(aggregate, array[i]);
            }

            return aggregate;
        }

        public static TAccumulate Aggregate<T, TAccumulate>(
            this T[] array, 
            TAccumulate seed,
            Func<TAccumulate, T, TAccumulate> aggregator)
        {
            var aggregate = seed;
            for (int i = 1; i < array.Length; i++)
            {
                aggregate = aggregator(aggregate, array[i]);
            }

            return aggregate;
        }

        public static TResult Aggregate<T, TAccumulate, TResult>(
            this T[] array, 
            TAccumulate seed,
            Func<TAccumulate, T, TAccumulate> aggregator,
            Func<TAccumulate, TResult> resultSelector)
        {
            return resultSelector(Aggregate(array, seed, aggregator));
        }

        public static T First<T>(this T[] array)
        {
            if (array.Length > 0)
            {
                return array[0];
            }

            throw Error.NoElements();
        }

        public static T First<T>(this T[] array, Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate(array[i]))
                {
                    return array[i];
                }
            }

            throw Error.NoElements();
        }

        public static T FirstOrDefault<T>(this T[] array)
        {
            if (array.Length > 0)
            {
                return array[0];
            }

            return default(T);
        }

        public static T FirstOrDefault<T>(this T[] array, Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate(array[i]))
                {
                    return array[i];
                }
            }

            return default(T);
        }

        public static T Last<T>(this T[] array)
        {
            if (array.Length > 0)
            {
                return array[array.Length - 1];
            }

            throw Error.NoElements();
        }

        public static T Last<T>(this T[] array, Predicate<T> predicate)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (predicate(array[i]))
                {
                    return array[i];
                }
            }

            throw Error.NoElements();
        }

        public static T LastOrDefault<T>(this T[] array)
        {
            if (array.Length > 0)
            {
                return array[array.Length - 1];
            }

            return default(T);
        }

        public static T LastOrDefault<T>(this T[] array, Predicate<T> predicate)
        {
            for (int i = array.Length - 1; i >= 0; i--)
            {
                if (predicate(array[i]))
                {
                    return array[i];
                }
            }

            return default(T);
        }

        public static T ElementAt<T>(this T[] array, int index)
        {
            Contract.RequiresInRange(index, array.Length);

            return array[index];
        }

        public static T ElementAtOrDefault<T>(this T[] array, int index)
        {
            if (Contract.IsInRange(index, array.Length))
            {
                return array[index];
            }

            return default(T);
        }

        public static short Sum(this short[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            short sum = 0;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    sum += source[i];
                }
            }

            return sum;
        }

        public static short? Sum(this short?[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            short sum = 0;
            short? current;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    current = source[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        public static short Min(this short[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var min = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (min > source[i])
                {
                    min = source[i];
                }
            }

            return min;
        }

        public static short? Min(this short?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            short? min = null;
            short? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        public static short Max(this short[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var max = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (max < source[i])
                {
                    max = source[i];
                }
            }

            return max;
        }

        public static short? Max(this short?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            short? max = null;
            short? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        public static double Average(this short[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            return (double)Sum(source) / source.Length;
        }

        public static double? Average(this short?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.Length;
        }

        public static int Sum(this int[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            int sum = 0;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    sum += source[i];
                }
            }

            return sum;
        }

        public static int? Sum(this int?[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            int sum = 0;
            int? current;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    current = source[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        public static int Min(this int[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var min = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (min > source[i])
                {
                    min = source[i];
                }
            }

            return min;
        }

        public static int? Min(this int?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            int? min = null;
            int? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        public static int Max(this int[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var max = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (max < source[i])
                {
                    max = source[i];
                }
            }

            return max;
        }

        public static int? Max(this int?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            int? max = null;
            int? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        public static double Average(this int[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            return (double)Sum(source) / source.Length;
        }

        public static double? Average(this int?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.Length;
        }

        public static long Sum(this long[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            long sum = 0;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    sum += source[i];
                }
            }

            return sum;
        }

        public static long? Sum(this long?[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            long sum = 0;
            long? current;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    current = source[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        public static long Min(this long[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var min = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (min > source[i])
                {
                    min = source[i];
                }
            }

            return min;
        }

        public static long? Min(this long?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            long? min = null;
            long? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        public static long Max(this long[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var max = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (max < source[i])
                {
                    max = source[i];
                }
            }

            return max;
        }

        public static long? Max(this long?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            long? max = null;
            long? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        public static double Average(this long[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            return (double)Sum(source) / source.Length;
        }

        public static double? Average(this long?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.Length;
        }

        public static float Sum(this float[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            float sum = 0;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    sum += source[i];
                }
            }

            return sum;
        }

        public static float? Sum(this float?[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            float sum = 0;
            float? current;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    current = source[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        public static float Min(this float[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var min = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (min > source[i])
                {
                    min = source[i];
                }
            }

            return min;
        }

        public static float? Min(this float?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            float? min = null;
            float? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        public static float Max(this float[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var max = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (max < source[i])
                {
                    max = source[i];
                }
            }

            return max;
        }

        public static float? Max(this float?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            float? max = null;
            float? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        public static double Average(this float[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            return (double)Sum(source) / source.Length;
        }

        public static double? Average(this float?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.Length;
        }

        public static double Sum(this double[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            double sum = 0;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    sum += source[i];
                }
            }

            return sum;
        }

        public static double? Sum(this double?[] source)
        {
            Contract.RequiresNotDefault(source, "source");

            double sum = 0;
            double? current;
            checked
            {
                for (int i = 0; i < source.Length; i++)
                {
                    current = source[i];
                    if (current != null)
                    {
                        sum += current.GetValueOrDefault();
                    }
                }
            }

            return sum;
        }

        public static double Min(this double[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var min = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (min > source[i])
                {
                    min = source[i];
                }
            }

            return min;
        }

        public static double? Min(this double?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            double? min = null;
            double? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    min = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null
                    && min.Value > current.Value)
                {
                    min = current;
                }
            }

            return min;
        }

        public static double Max(this double[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var max = source[0];
            for (int i = 1; i < source.Length; i++)
            {
                if (max < source[i])
                {
                    max = source[i];
                }
            }

            return max;
        }

        public static double? Max(this double?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            double? max = null;
            double? current;
            int i = 0;
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null)
                {
                    max = current;
                }
            }
            for (; i < source.Length; i++)
            {
                current = source[i];
                if (current != null 
                    && max.Value < current.Value)
                {
                    max = current;
                }
            }

            return max;
        }

        public static double Average(this double[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            return (double)Sum(source) / source.Length;
        }

        public static double? Average(this double?[] source)
        {
            Contract.RequiresNotDefault(source, "source");
            Contract.RequiresNonEmptyCollection(source.Length);

            var sum = Sum(source);
            if(sum == null)
            {
                return null;
            }

            return sum.Value / source.Length;
        }
// ReSharper restore UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments
    }
}