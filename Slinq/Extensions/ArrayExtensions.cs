using System;
using Slinq.Iterators;

namespace Slinq.Extensions
{
    public static class ArrayExtensions
    {
        public static WhereIterator<T> Where<T>(this T[] source, Predicate<T> predicate)
        {
            return new WhereIterator<T>(source, predicate, source.Length);
        }
    }
}