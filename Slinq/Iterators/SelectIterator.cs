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
        internal readonly TSource[] Source;
        internal readonly Func<TSource, TResult> Selector;
        internal readonly int ActualLength;
        private int _index;

        internal SelectIterator(TSource[] array, int actualLength, Func<TSource, TResult> selector)
        {
            Source = array;
            Selector = selector;
            ActualLength = actualLength;
            _index = -1;
        }

        public TResult Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return Selector(Source[_index]); }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return ++_index < ActualLength;
        }

        public SelectIterator<TSource, TResult> GetEnumerator()
        {
            return this;
        }

        public SelectWhereIterator<TSource, TResult> Where(Predicate<TResult> predicate)
        {
            return new SelectWhereIterator<TSource, TResult>(this, predicate);
        }
    }
}