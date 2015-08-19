using System;
using System.Collections.Generic;

namespace Slinq.Models
{
    internal struct ExtractedArray<T>
    {
        internal readonly T[] Array;

        internal readonly int ActualLength;

        public ExtractedArray(T[] array, int actualLength)
        {
            Array = array;
            ActualLength = actualLength;
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
            if (!Any())
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
    }
}