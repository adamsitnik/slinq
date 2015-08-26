﻿//------------------------------------------------------------------------------
// <auto-generated>look at the RangeSelectIteratorGenerator.tt</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Slinq.Abstract;
using Slinq.Utils;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Design")]
    public struct RangeSelectEnumerator<T> : IStrongEnumerator<T>
    {
        private readonly Func<int, T> _selector;
        private readonly int _end;
        private int _currentItemIndex;

        internal RangeSelectEnumerator(int start, int end, Func<int, T> selector)
        {
            _selector = selector;
            _end = end;
            _currentItemIndex = start - 1;
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _selector(_currentItemIndex); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return ++_currentItemIndex < _end;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Design")]
    public struct RangeSelectIterator<T> : IStrongEnumerable<T, RangeSelectEnumerator<T>>, IFixedCount
    {
        private readonly int _start;
        private readonly int _end;
        private readonly Func<int, T> _selector;

        public RangeSelectIterator(int start, int end, Func<int, T> selector)
        {
            _start = start;
            _end = end;
            _selector = selector;
        }

        public int FixedCount
        {
            get { return _end - _start; }
        }

        public RangeSelectEnumerator<T> GetEnumerator()
        {
            return new RangeSelectEnumerator<T>(_start, _end, _selector);
        }

        public T[] ToArray()
        {
            var result = new T[FixedCount];
            for (int rangeIndex = _start, resultIndex = 0; rangeIndex < _end; rangeIndex++, resultIndex++)
            {
                result[resultIndex] = _selector(rangeIndex);
            }

            return result;
        }

        public List<T> ToList()
        {
            return ListFactory<T>.Create(ToArray());
        }

        public bool Any()
        {
            int index = _start;
            while (index < _end)
            {
                return true;
            }

            return false;
        }

        public int Count()
        {
            return FixedCount;
        }

        public T First()
        {
            int index = _start;
            while (index < _end)
            {
                return _selector(index);
            }

            throw Error.NoElements();
        }

        public T FirstOrDefault()
        {
            int index = _start;
            while (index < _end)
            {
                return _selector(index);
            }

            return default(T);
        }

        public T Last()
        {
            int index = _end - 1;
            while (index >= _start)
            {
                return _selector(index);
            }

            throw Error.NoElements();
        }

        public T LastOrDefault()
        {
            int index = _end - 1;
            while (index >= _start)
            {
                return _selector(index);
            }

            return default(T);
        }
    }
}