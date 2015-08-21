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
        internal readonly T[] Source;
        internal readonly Predicate<T> Predicate;
        internal readonly int ActualLength;
        private int _index;

        internal WhereIterator(T[] array, int actualLength, Predicate<T> predicate)
        {
            Source = array;
            Predicate = predicate;
            ActualLength = actualLength;
            _index = -1;
        }

        public T Current
        {
            [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
            get { return Source[_index]; }
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            while (++_index < ActualLength)
            {
                if (Predicate(Source[_index]))
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