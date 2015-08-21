using System;
using System.Runtime.CompilerServices;
using Slinq.Abstract;
using Slinq.Utils;

namespace Slinq.Iterators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes",
        Justification = "It is not going to be compared")]
    public struct RangeIterator : IStrongEnumerator<int>, IStrongEnumerable<int, RangeIterator>
    {
        internal readonly int Start;
        internal readonly int End;
        private int _currentIndex;

        public RangeIterator(int start, int count)
        {
            Contract.Requires(count > 0);

            Start = start;
            End = start + count;
            _currentIndex = Start - 1;
        }

        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return End - Start; }
        }

        public int Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return _currentIndex; }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            return ++_currentIndex < End;
        }

        public RangeIterator GetEnumerator()
        {
            return this;
        }

        public RangeSelectIterator<T> Select<T>(Func<int, T> selector)
        {
            return new RangeSelectIterator<T>(this, selector);
        }

        public void ForEach(Action<int> command)
        {
            for (int i = Start; i < End; i++)
            {
                command(i);
            }
        }

        // it is not about performance but usability
        public void ForEach(Action command)
        {
            for (int i = Start; i < End; i++)
            {
                command();
            }
        }
    }
}