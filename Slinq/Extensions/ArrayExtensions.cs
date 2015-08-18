using System;
using Slinq.Iterators;

namespace Slinq.Extensions
{
    public static class ArrayExtensions
    {
        public static ArrayWhereIterator<T> Where<T>(this T[] source, Predicate<T> predicate)
        {
            return new ArrayWhereIterator<T>(source, predicate, source.Length);
        }
    }
}