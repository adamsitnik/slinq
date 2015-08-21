using System;
using System.Collections.Generic;
using Slinq.Iterators;
using Slinq.Utils;

namespace Slinq.Models
{
    internal struct ExtractedArray<T>
    {
        private readonly T[] _array;

        /// <summary>
        /// the wrapped array is not used in 100%, i.e. list of 10 sequentially added elements has 16 long array
        /// </summary>
        private readonly int _actualLength;

        internal ExtractedArray(T[] array, int actualLength)
        {
            Contract.RequiresInInclusiveRange(actualLength, array.Length);

            _array = array;
            _actualLength = actualLength;
        }

// ReSharper disable MethodNamesNotMeaningful we just follow the existing convention
        internal WhereIterator<T> Where(Predicate<T> predicate)
        {
            return new WhereIterator<T>(_array, _actualLength, predicate);
        }

        internal SelectIterator<T, TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return new SelectIterator<T, TResult>(_array, _actualLength, selector);
        }

        internal bool Any()
        {
            return _actualLength > 0;
        }

        internal bool Any(Predicate<T> predicate)
        {
            for (int i = 0; i < _actualLength; i++)
            {
                if (predicate(_array[i]))
                {
                    return true;
                }
            }

            return false;
        }

        internal bool All(Predicate<T> predicate)
        {
            for (int i = 0; i < _actualLength; i++)
            {
                if (!predicate(_array[i]))
                {
                    return false;
                }
            }

            return true;
        }

        internal int Count()
        {
            return _actualLength;
        }

        internal int Count(Predicate<T> predicate)
        {
            var count = 0;
            for (int i = 0; i < _actualLength; i++)
            {
                if (predicate(_array[i]))
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
            for (int i = 0; i < _actualLength; i++)
            {
                if (equalityComparer.Equals(_array[i], item))
                {
                    return true;
                }
            }

            return false;
        }

        internal T Aggregate(Func<T, T, T> aggregator)
        {
            if (_actualLength == 0)
            {
                throw Error.NoElements();
            }

            var aggregate = _array[0];
            for (int i = 1; i < _actualLength; i++)
            {
                aggregate = aggregator(aggregate, _array[i]);
            }

            return aggregate;
        }

        internal TAccumulate Aggregate<TAccumulate>(
            
            TAccumulate seed,
            Func<TAccumulate, T, TAccumulate> aggregator)
        {
            var aggregate = seed;
            for (int i = 1; i < _actualLength; i++)
            {
                aggregate = aggregator(aggregate, _array[i]);
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
            if (_actualLength > 0)
            {
                return _array[0];
            }

            throw Error.NoElements();
        }

        internal T First(Predicate<T> predicate)
        {
            for (int i = 0; i < _actualLength; i++)
            {
                if (predicate(_array[i]))
                {
                    return _array[i];
                }
            }

            throw Error.NoElements();
        }

        internal T FirstOrDefault()
        {
            if (_actualLength > 0)
            {
                return _array[0];
            }

            return default(T);
        }

        internal T FirstOrDefault(Predicate<T> predicate)
        {
            for (int i = 0; i < _actualLength; i++)
            {
                if (predicate(_array[i]))
                {
                    return _array[i];
                }
            }

            return default(T);
        }

        internal T Last()
        {
            if (_actualLength > 0)
            {
                return _array[_actualLength - 1];
            }

            throw Error.NoElements();
        }

        internal T Last(Predicate<T> predicate)
        {
            for (int i = _actualLength - 1; i >= 0; i--)
            {
                if (predicate(_array[i]))
                {
                    return _array[i];
                }
            }

            throw Error.NoElements();
        }

        internal T LastOrDefault()
        {
            if (_actualLength > 0)
            {
                return _array[_actualLength - 1];
            }

            return default(T);
        }

        internal T LastOrDefault(Predicate<T> predicate)
        {
            for (int i = _actualLength - 1; i >= 0; i--)
            {
                if (predicate(_array[i]))
                {
                    return _array[i];
                }
            }

            return default(T);
        }

        internal T ElementAt(int index)
        {
            Contract.RequiresInRange(index, _actualLength);

            return _array[index];
        }

        internal T ElementAtOrDefault(int index)
        {
            if (Contract.IsInRange(index, _actualLength))
            {
                return _array[index];
            }

            return default(T);
        }
// ReSharper restore MethodNamesNotMeaningful
    }
}