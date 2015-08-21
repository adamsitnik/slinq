using System;
using System.Runtime.CompilerServices;
using Slinq.Abstract;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes",
        Justification = "It is not going to be compared")]
    public struct SelectWhereIterator<TSource, TResult> : IStrongEnumerator<TResult>, IStrongEnumerable<TResult, SelectWhereIterator<TSource, TResult>>
    {
        // we have so many fields here because "manual inling" is just performing better than pure calls to SelectIterator's MoveNext/Current
        private readonly TSource[] _source;
        private readonly Func<TSource, TResult> _selector;
        private readonly int _actualLength;
        private readonly Predicate<TResult> _predicate;
        private TResult _current;
        private int _index;

        public SelectWhereIterator(SelectIterator<TSource, TResult> selectIterator, Predicate<TResult> predicate)
        {
            _source = selectIterator.Source;
            _selector = selectIterator.Selector;
            _actualLength = selectIterator.ActualLength;
            _predicate = predicate;
            _current = default(TResult);
            _index = -1;
        }

        public TResult Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _current; }
        }

        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            while (++_index < _actualLength)
            {
                var next = _selector(_source[_index]);
                if (_predicate(next))
                {
                    _current = next; // the selector might be quite expensive operation so we need to store it's result
                    return true;
                }
            }

            return false;
        }

        public SelectWhereIterator<TSource, TResult> GetEnumerator()
        {
            return this;
        }
    }
}