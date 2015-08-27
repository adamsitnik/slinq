using System;
using System.Collections.Generic;
using Slinq.Utils;

namespace Slinq.Extensions
{
    public static class SortingExtensions
    {
        public static SortingIterator<TSource> OrderBy<TSource, TKey>(this TSource[] source, Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            return new SortingIterator<TSource>(source).OrderBy(keySelector);
        }

        public static SortingIterator<TSource> OrderByDescending<TSource, TKey>(this TSource[] source, Func<TSource, TKey> keySelector)
            where TKey : IComparable<TKey>
        {
            return new SortingIterator<TSource>(source).OrderByDescending(keySelector);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public struct SortingIterator<TSource>
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
                Array.Sort(_source, new Comparer<TSource>(_rules.ToArray()));

                return _source;
            }

            public SortingIterator<TSource> ThenBy<TKey>(Func<TSource, TKey> keySelector)
                where TKey : IComparable<TKey>
            {
                AddAscendingRule(keySelector);

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
        }

        private class Comparer<TSource> : IComparer<TSource>
        {
            private const int AreEqual = 0;

            private readonly Func<TSource, TSource, int>[] _rules;

            internal Comparer(Func<TSource, TSource, int>[] rules)
            {
                Contract.RequiresNonEmptyCollection(rules.Length);

                _rules = rules;
            }

            public int Compare(TSource x, TSource y)
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