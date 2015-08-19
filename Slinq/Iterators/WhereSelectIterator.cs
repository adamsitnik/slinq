using System;
using System.Runtime.CompilerServices;
using Slinq.Abstract;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes",
        Justification = "It is not going to be compared")]
    public struct WhereSelectIterator<TSource, TResult>
        : IStrongEnumerator<TResult>, IStrongEnumerable<TResult, WhereSelectIterator<TSource, TResult>>
    {
        private readonly TSource[] _source;
        private readonly Predicate<TSource> _predicate;
        private readonly int _actualLength;
        private readonly Func<TSource, TResult> _selector;
        private int _index;

        public WhereSelectIterator(WhereIterator<TSource> source, Func<TSource, TResult> selector)
        {
            _source = source.Source;
            _predicate = source.Predicate;
            _actualLength = source.ActualLength;
            _selector = selector;
            _index = -1;
        }

        public TResult Current
        {
            [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
            get { return _selector(_source[_index]); }
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            while (++_index < _actualLength)
            {
                if (_predicate(_source[_index]))
                {
                    return true;
                }
            }

            return false;
        }

        public WhereSelectIterator<TSource, TResult> GetEnumerator()
        {
            return this;
        }
    }
}