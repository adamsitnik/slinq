using System;
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
// ReSharper restore UnusedMember.Global, ClassTooBig, MethodNamesNotMeaningful, TooManyArguments
    }
}