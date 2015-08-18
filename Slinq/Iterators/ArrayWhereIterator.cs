using System;
using Slinq.Abstract;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes",
        Justification = "It is not going to be compared")]
    public struct ArrayWhereIterator<T> : IStrongEnumerator<T>, IStrongEnumerable<T, ArrayWhereIterator<T>>
    {
        private readonly T[] _source;
        private readonly Predicate<T> _predicate;
        private readonly int _actualLength;
        private int _index;

        internal ArrayWhereIterator(T[] source, Predicate<T> predicate, int actualLength)
            : this()
        {
            _source = source;
            _predicate = predicate;
            _actualLength = actualLength;
            _index = -1;
        }

        public T Current
        {
            get { return _source[_index]; }
        }

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

        public ArrayWhereIterator<T> GetEnumerator()
        {
            return this;
        }

        public ArrayWhereSelectIterator<T, TResult> Select<TResult>(Func<T, TResult> selector)
        {
            return new ArrayWhereSelectIterator<T, TResult>(this, selector);
        }
    }
}