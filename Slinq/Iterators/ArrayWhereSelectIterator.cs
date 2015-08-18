using System;
using System.Runtime.CompilerServices;
using Slinq.Abstract;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes",
        Justification = "It is not going to be compared")]
    public struct ArrayWhereSelectIterator<TSource, TResult>
        : IStrongEnumerator<TResult>, IStrongEnumerable<TResult, ArrayWhereSelectIterator<TSource, TResult>>
    {
        private readonly Func<TSource, TResult> _selector;
        private ArrayWhereIterator<TSource> _source;

        public ArrayWhereSelectIterator(ArrayWhereIterator<TSource> source, Func<TSource, TResult> selector)
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

        public ArrayWhereSelectIterator<TSource, TResult> GetEnumerator()
        {
            return this;
        }
    }
}