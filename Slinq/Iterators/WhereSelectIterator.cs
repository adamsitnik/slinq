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
        private readonly Func<TSource, TResult> _selector;
        private WhereIterator<TSource> _source;

        public WhereSelectIterator(WhereIterator<TSource> source, Func<TSource, TResult> selector)
        {
            _source = source;
            _selector = selector;
        }

        public TResult Current
        {
            [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
            get { return _selector.Invoke(_source.Current); }
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return _source.MoveNext();
        }

        public WhereSelectIterator<TSource, TResult> GetEnumerator()
        {
            return this;
        }
    }
}