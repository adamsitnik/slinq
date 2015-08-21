using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Slinq.Abstract;
using Slinq.Utils;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes",
        Justification = "It is not going to be compared")]
    public struct RangeSelectIterator<T> : IStrongEnumerator<T>, IStrongEnumerable<T, RangeSelectIterator<T>>
    {
        private readonly Func<int, T> _selector;
        private RangeIterator _rangeIterator;

        public RangeSelectIterator(RangeIterator rangeIterator, Func<int, T> selector)
        {
            _rangeIterator = rangeIterator;
            _selector = selector;
        }

        public T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _selector(_rangeIterator.Current); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return _rangeIterator.MoveNext();
        }

        public RangeSelectIterator<T> GetEnumerator()
        {
            return this;
        }

        public T[] ToArray()
        {
            var result = new T[_rangeIterator.Count];
            for (int rangeIndex = _rangeIterator.Start, resultIndex = 0; rangeIndex < _rangeIterator.End; rangeIndex++, resultIndex++)
            {
                result[resultIndex] = _selector(rangeIndex);
            }

            return result;
        }

        public List<T> ToList()
        {
            return ListFactory<T>.Create(ToArray());
        }
    }
}