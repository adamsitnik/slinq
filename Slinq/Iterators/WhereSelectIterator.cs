﻿//------------------------------------------------------------------------------
// <auto-generated>look at the WhereSelectIteratorGenerator.tt</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Slinq.Abstract;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Design")]
    public struct WhereSelectEnumerator<TSource, TResult> : IStrongEnumerator<TResult>
    {
        private readonly TSource[] _source;
        private readonly Predicate<TSource> _predicate;
        private readonly int _actualLength;
        private readonly Func<TSource, TResult> _selector;
        private int _currentItemIndex;

        internal WhereSelectEnumerator(TSource[] source, Predicate<TSource> predicate, int actualLength, Func<TSource, TResult> selector)
        {
            _source = source;
            _predicate = predicate;
            _actualLength = actualLength;
            _selector = selector;
            _currentItemIndex = -1;
        }

        public TResult Current
        {
            [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
            get { return _selector(_source[_currentItemIndex]); }
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
    public struct WhereSelectIterator<TSource, TResult> : IStrongEnumerable<TResult, WhereSelectEnumerator<TSource, TResult>>
    {
        private readonly TSource[] _source;
        private readonly int _actualLength;
        private readonly Predicate<TSource> _predicate;
        private readonly Func<TSource, TResult> _selector;

        internal WhereSelectIterator(TSource[] source, int actualLength, Predicate<TSource> predicate, Func<TSource, TResult> selector)
        {
            _source = source;
            _predicate = predicate;
            _actualLength = actualLength;
            _selector = selector;
        }

        public WhereSelectEnumerator<TSource, TResult> GetEnumerator()
        {
            return new WhereSelectEnumerator<TSource, TResult>(_source, _predicate, _actualLength, _selector);
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

        public bool Contains(TResult item)
        {
            return Contains(item, EqualityComparer<TResult>.Default);
        }

        public bool Contains(TResult item, IEqualityComparer<TResult> equalityComparer)
        {
            int index = 0;
            while (index < _actualLength)
            {
                if(_predicate(_source[index]))
                {
                    if (equalityComparer.Equals(_selector(_source[index]), item))
                    {
                        return true;
                    }
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

        public TResult First()
        {
            int index = 0;
            while (index < _actualLength)
            {
                if(_predicate(_source[index]))
                {
                    return _selector(_source[index]);
                }
                ++index;
            }

            throw Error.NoElements();
        }

        public TResult FirstOrDefault()
        {
            int index = 0;
            while (index < _actualLength)
            {
                if(_predicate(_source[index]))
                {
                    return _selector(_source[index]);
                }
                ++index;
            }

            return default(TResult);
        }

        public TResult Last()
        {
            int index = _actualLength - 1;
            while (index >= 0)
            {
                if(_predicate(_source[index]))
                {
                    return _selector(_source[index]);
                }
                --index;
            }

            throw Error.NoElements();
        }

        public TResult LastOrDefault()
        {
            int index = _actualLength - 1;
            while (index >= 0)
            {
                if(_predicate(_source[index]))
                {
                    return _selector(_source[index]);
                }
                --index;
            }

            return default(TResult);
        }

        public TResult Single()
        {
            TResult single = default(TResult);
            bool hasFound = false;
            int index = 0;
            while (index < _actualLength)
            {
                if(_predicate(_source[index]))
                {
                    if(hasFound)
                    {
                        throw Error.MoreThanOneElement();
                    }
                    single = _selector(_source[index]);
                    hasFound = true;
                }
                ++index;
            }

            if(hasFound)
            {
                return single;
            }
            throw Error.NoElements();
        }

        public TResult SingleOrDefault()
        {
            TResult single = default(TResult);
            bool hasFound = false;
            int index = 0;
            while (index < _actualLength)
            {
                if(_predicate(_source[index]))
                {
                    if(hasFound)
                    {
                        throw Error.MoreThanOneElement();
                    }
                    single = _selector(_source[index]);
                    hasFound = true;
                }
                ++index;
            }

            return single;
        }
    }
}