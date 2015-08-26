﻿//------------------------------------------------------------------------------
// <auto-generated>look at the RangeIteratorGenerator.tt</auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Slinq.Abstract;
using Slinq.Utils;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Design")]
    public struct RangeEnumerator : IStrongEnumerator<int>
    {
        private readonly int _end;
        private int _currentItemIndex;

        internal RangeEnumerator(int start, int end)
        {
            _end = end;
            _currentItemIndex = start - 1;
        }

        public int Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _currentItemIndex; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return ++_currentItemIndex < _end;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes", Justification = "Design")]
    public struct RangeIterator : IStrongEnumerable<int, RangeEnumerator>, IFixedCount
    {
        private readonly int _start;
        private readonly int _end;

        public RangeIterator(int start, int count)
        {
            _start = start;
            _end = start + count;
        }

        public int FixedCount
        {
            get { return _end - _start; }
        }

        public RangeEnumerator GetEnumerator()
        {
            return new RangeEnumerator(_start, _end);
        }

        public RangeSelectIterator<T> Select<T>(Func<int, T> selector)
        {
            return new RangeSelectIterator<T>(_start, _end, selector);
        }

        public void ForEach(Action<int> command)
        {
            for (int i = _start; i < _end; i++)
            {
                command(i);
            }
        }

        // it is not about performance but usability
        public void ForEach(Action command)
        {
            for (int i = _start; i < _end; i++)
            {
                command();
            }
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

        public bool Contains(int item)
        {
            return Contains(item, EqualityComparer<int>.Default);
        }

        public bool Contains(int item, IEqualityComparer<int> equalityComparer)
        {
            int index = _start;
            while (index < _end)
            {
                if (equalityComparer.Equals(index, item))
                {
                    return true;
                }
                ++index;
            }

            return false;
        }

        public int Count()
        {
            return FixedCount;
        }

        public int First()
        {
            int index = _start;
            while (index < _end)
            {
                return index;
            }

            throw Error.NoElements();
        }

        public int FirstOrDefault()
        {
            int index = _start;
            while (index < _end)
            {
                return index;
            }

            return default(int);
        }

        public int Last()
        {
            int index = _end - 1;
            while (index >= _start)
            {
                return index;
            }

            throw Error.NoElements();
        }

        public int LastOrDefault()
        {
            int index = _end - 1;
            while (index >= _start)
            {
                return index;
            }

            return default(int);
        }
    }
}