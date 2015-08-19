using System;
using System.Runtime.CompilerServices;
using Slinq.Abstract;
using Slinq.Models;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes",
        Justification = "It is not going to be compared")]
    public struct WhereIterator<T> : IStrongEnumerator<T>, IStrongEnumerable<T, WhereIterator<T>>
    {
        private readonly T[] _source;
        private readonly Predicate<T> _predicate;
        private readonly int _actualLength;
        private int _index;

        internal WhereIterator(ExtractedArray<T> extractedArray, Predicate<T> predicate)
        {
            _source = extractedArray.Array;
            _predicate = predicate;
            _actualLength = extractedArray.ActualLength;
            _index = -1;
        }

        public T Current
        {
            [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
            get { return _source[_index]; }
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            while (++_index < _actualLength)
            {
                if (_predicate.Invoke(_source[_index]))
                {
                    return true;
                }
            }

            return false;
        }

        public WhereIterator<T> GetEnumerator()
        {
            return this;
        }

        public WhereSelectIterator<T, TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return new WhereSelectIterator<T, TResult>(this, selector);
        }
    }
}