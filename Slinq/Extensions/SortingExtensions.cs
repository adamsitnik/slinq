using System;
using System.Collections.Generic;
using Slinq.Iterators;

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

        public static TSource[] Sort<TSource>(this TSource[] source, IComparer<TSource> comparer)
        {
            Array.Sort(source, comparer);

            return source;
        }
    }
}