using System;
using System.Collections.Generic;
using Slinq.Iterators;
using Slinq.Utils;

namespace Slinq.Extensions
{
    public static class ListExtensions
    {
        public static WhereIterator<T> Where<T>(this List<T> source, Predicate<T> predicate)
        {
            return new WhereIterator<T>(
                ArrayProvider<T>.GetWrappedArrayWithDynamicCilGeneration(source), 
                predicate,
                actualLength: source.Count); // the wrapped array is not used in 100%, i.e. list of 10 elements has 16 long array
        }
    }
}