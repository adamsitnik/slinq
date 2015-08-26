﻿//------------------------------------------------------------------------------
// <auto-generated>look at the WhereIteratorGenerator.tt</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;
using Slinq.Abstract;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Design")]
    public struct WhereEnumerator<T> : IStrongEnumerator<T>
    {
        private readonly T[] _source;
        private readonly int _actualLength;
        private readonly Predicate<T> _predicate;
        private int _currentItemIndex;

        internal WhereEnumerator(T[] array, int actualLength, Predicate<T> predicate)
        {
            _source = array;
            _predicate = predicate;
            _actualLength = actualLength;
            _currentItemIndex = -1;
        }

        public T Current
        {
            [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
            get { return _source[_currentItemIndex]; }
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            while (++_currentItemIndex < _actualLength)
            {
                if (_predicate(_source[_currentItemIndex]))
                {
                    return true;
                }
            }

            return false;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Design")]
    public struct WhereIterator<T> : IStrongEnumerable<T, WhereEnumerator<T>>
    {
        private readonly T[] _source;
        private readonly int _actualLength;
        private readonly Predicate<T> _predicate;

        internal WhereIterator(T[] source, int actualLength, Predicate<T> predicate)
        {
            _source = source;
            _predicate = predicate;
            _actualLength = actualLength;
        }

        public WhereEnumerator<T> GetEnumerator()
        {
            return new WhereEnumerator<T>(_source, _actualLength, _predicate);
        }

        public WhereSelectIterator<T, TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return new WhereSelectIterator<T, TResult>(_source, _actualLength, _predicate, selector);
        }

        public bool Any()
        {
            int index = 0;
            while (index < _actualLength)
            {
                if(_predicate(_source[index]))
                {
                    return true;
                }
                ++index;
            }

            return false;
        }

        public int Count()
        {
            int index = 0;
            int count = 0;
            while (index < _actualLength)
            {
                if(_predicate(_source[index]))
                {
                    ++count;
                }
                ++index;
            }

            return count;
        }

        public T First()
        {
            int index = 0;
            while (index < _actualLength)
            {
                if(_predicate(_source[index]))
                {
                    return _source[index];
                }
                ++index;
            }

            throw Error.NoElements();
        }

        public T FirstOrDefault()
        {
            int index = 0;
            while (index < _actualLength)
            {
                if(_predicate(_source[index]))
                {
                    return _source[index];
                }
                ++index;
            }

            return default(T);
        }

        public T Last()
        {
            int index = _actualLength - 1;
            while (index >= 0)
            {
                if(_predicate(_source[index]))
                {
                    return _source[index];
                }
                --index;
            }

            throw Error.NoElements();
        }

        public T LastOrDefault()
        {
            int index = _actualLength - 1;
            while (index >= 0)
            {
                if(_predicate(_source[index]))
                {
                    return _source[index];
                }
                --index;
            }

            return default(T);
        }
    }
}