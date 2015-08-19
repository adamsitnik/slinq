using System;
using System.Runtime.CompilerServices;
using Slinq.Abstract;
using Slinq.Models;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes",
        Justification = "It is not going to be compared")]
    public struct SelectIterator<TSource, TResult> : IStrongEnumerator<TResult>, IStrongEnumerable<TResult, SelectIterator<TSource, TResult>>
    {
        private readonly TSource[] _source;
        private readonly Func<TSource, TResult> _selector;
        private readonly int _actualLength;
        private int _index;

        internal SelectIterator(ExtractedArray<TSource> extractedArray, Func<TSource, TResult> selector)
        {
            _source = extractedArray.Array;
            _selector = selector;
            _actualLength = extractedArray.ActualLength;
            _index = -1;
        }

        public TResult Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _selector(_source[_index]); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return ++_index < _actualLength;
        }

        public SelectIterator<TSource, TResult> GetEnumerator()
        {
            return this;
        }
    }
}