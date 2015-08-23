﻿using System;
using System.Collections.Generic;
using Slinq.Iterators;
using Slinq.Utils;

namespace Slinq.Models
{
    internal struct ExtractedArray<T>
    {
        internal readonly T[] Array;

        /// <summary>
        /// the wrapped array is not used in 100%, i.e. list of 10 sequentially added elements has 16 long array
        /// </summary>
        internal readonly int ActualLength;

        internal ExtractedArray(T[] array, int actualLength)
        {
            Contract.RequiresInInclusiveRange(actualLength, array.Length);

            Array = array;
            ActualLength = actualLength;
        }

// ReSharper disable MethodNamesNotMeaningful we just follow the existing convention
        internal WhereIterator<T> Where(Predicate<T> predicate)
        {
            return new WhereIterator<T>(Array, ActualLength, predicate);
        }

        internal SelectIterator<T, TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return new SelectIterator<T, TResult>(Array, ActualLength, selector);
        }

        internal bool Any()
        {
            return ActualLength > 0;
        }

        internal bool Any(Predicate<T> predicate)
        {
            for (int i = 0; i < ActualLength; i++)
            {
                if (predicate(Array[i]))
                {
                    return true;
                }
            }

            return false;
        }

        internal bool All(Predicate<T> predicate)
        {
            for (int i = 0; i < ActualLength; i++)
            {
                if (!predicate(Array[i]))
                {
                    return false;
                }
            }

            return true;
        }

        internal int Count()
        {
            return ActualLength;
        }

        internal int Count(Predicate<T> predicate)
        {
            var count = 0;
            for (int i = 0; i < ActualLength; i++)
            {
                if (predicate(Array[i]))
                {
                    checked
                    {
                        ++count;
                    }
                }
            }

            return count;
        }

        internal bool Contains(T item)
        {
            // can not use Array.Contains because it could give false-positive 
            // if i.e. this was array taken from List and somebody would query for 0 (default(int))
            // and at the end there would be some values that match but they are not in the List for real
            return Contains(item, EqualityComparer<T>.Default);
        }

        internal bool Contains(T item, IEqualityComparer<T> equalityComparer)
        {
            for (int i = 0; i < ActualLength; i++)
            {
                if (equalityComparer.Equals(Array[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        internal T Aggregate(Func<T, T, T> aggregator)
        {
            if (ActualLength == 0)
            {
                throw Error.NoElements();
            }

            var aggregate = Array[0];
            for (int i = 1; i < ActualLength; i++)
            {
                aggregate = aggregator(aggregate, Array[i]);
            }

            return aggregate;
        }

        internal TAccumulate Aggregate<TAccumulate>(
            
            TAccumulate seed,
            Func<TAccumulate, T, TAccumulate> aggregator)
        {
            var aggregate = seed;
            for (int i = 1; i < ActualLength; i++)
            {
                aggregate = aggregator(aggregate, Array[i]);
            }

            return aggregate;
        }

        internal TResult Aggregate<TAccumulate, TResult>(
            
            TAccumulate seed,
            Func<TAccumulate, T, TAccumulate> aggregator,
            Func<TAccumulate, TResult> resultSelector)
        {
            return resultSelector(Aggregate(seed, aggregator));
        }

        internal T First()
        {
            if (ActualLength > 0)
            {
                return Array[0];
            }

            throw Error.NoElements();
        }

        internal T First(Predicate<T> predicate)
        {
            for (int i = 0; i < ActualLength; i++)
            {
                if (predicate(Array[i]))
                {
                    return Array[i];
                }
            }

            throw Error.NoElements();
        }

        internal T FirstOrDefault()
        {
            if (ActualLength > 0)
            {
                return Array[0];
            }

            return default(T);
        }

        internal T FirstOrDefault(Predicate<T> predicate)
        {
            for (int i = 0; i < ActualLength; i++)
            {
                if (predicate(Array[i]))
                {
                    return Array[i];
                }
            }

            return default(T);
        }

        internal T Last()
        {
            if (ActualLength > 0)
            {
                return Array[ActualLength - 1];
            }

            throw Error.NoElements();
        }

        internal T Last(Predicate<T> predicate)
        {
            for (int i = ActualLength - 1; i >= 0; i--)
            {
                if (predicate(Array[i]))
                {
                    return Array[i];
                }
            }

            throw Error.NoElements();
        }

        internal T LastOrDefault()
        {
            if (ActualLength > 0)
            {
                return Array[ActualLength - 1];
            }

            return default(T);
        }

        internal T LastOrDefault(Predicate<T> predicate)
        {
            for (int i = ActualLength - 1; i >= 0; i--)
            {
                if (predicate(Array[i]))
                {
                    return Array[i];
                }
            }

            return default(T);
        }

        internal T ElementAt(int index)
        {
            Contract.RequiresInRange(index, ActualLength);

            return Array[index];
        }

        internal T ElementAtOrDefault(int index)
        {
            if (Contract.IsInRange(index, ActualLength))
            {
                return Array[index];
            }

            return default(T);
        }
// ReSharper restore MethodNamesNotMeaningful
    }
}