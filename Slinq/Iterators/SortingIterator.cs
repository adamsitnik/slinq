using System;
using System.Collections.Generic;
using Slinq.Utils;

namespace Slinq.Iterators
{
    public class SortingIterator<TSource>
    {
        private readonly TSource[] _source;
        private readonly List<Func<TSource, TSource, int>> _rules; 

        public SortingIterator(TSource[] source)
        {
            _source = source;
            _rules = new List<Func<TSource, TSource, int>>();
        }

        public TSource[] ToArray()
        {
            Array.Sort(_source, new MultipleLambdaRulesComparer<TSource>(_rules.ToArray()));

            return _source;
        }

        public SortingIterator<TSource> ThenBy<TKey>(Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            AddAscendingRule(keySelector);

            return this;
        }

        public SortingIterator<TSource> ThenBy(Func<TSource, int> keySelector)
        {
            _rules.Add((left, right) => keySelector(left).CompareTo(keySelector(right)));

            return this;
        }

        public SortingIterator<TSource> ThenByDescending<TKey>(Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            AddDescendingRule(keySelector);

            return this;
        }

        internal SortingIterator<TSource> OrderBy<TKey>(Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            AddAscendingRule(keySelector);

            return this;
        }

        internal SortingIterator<TSource> OrderByDescending<TKey>(Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            AddDescendingRule(keySelector);

            return this;
        }

        private void AddAscendingRule<TKey>(Func<TSource, TKey> keySelector) 
            where TKey : IComparable<TKey>
        {
            _rules.Add((left, right) => keySelector(left).CompareTo(keySelector(right)));
        }

        private void AddDescendingRule<TKey>(Func<TSource, TKey> keySelector) 
            where TKey : IComparable<TKey>
        {
            _rules.Add((left, right) => keySelector(left).CompareTo(keySelector(right)) * -1);
        }

        private class MultipleLambdaRulesComparer<T> : IComparer<T>
        {
            private const int AreEqual = 0;

            private readonly Func<T, T, int>[] _rules;

            internal MultipleLambdaRulesComparer(Func<T, T, int>[] rules)
            {
                Contract.RequiresNonEmptyCollection(rules.Length);

                _rules = rules;
            }

            public int Compare(T x, T y)
            {
                for (int i = 0; i < _rules.Length; i++)
                {
                    var comparisonResult = _rules[i](x, y);
                    if (comparisonResult != AreEqual)
                    {
                        return comparisonResult;
                    }
                }

                return AreEqual;
            }
        }
    }
}