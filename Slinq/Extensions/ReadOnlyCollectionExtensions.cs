using System;
using System.Collections.ObjectModel;
using Slinq.Iterators;
using Slinq.Utils;

namespace Slinq.Extensions
{
    public static class ReadOnlyCollectionExtensions
    {
        /// <summary>
        /// supports only for Array/List based 
        /// </summary>
        /// <param name="source">that wraps Array/List</param>
        public static WhereIterator<T> Where<T>(this ReadOnlyCollection<T> source, Predicate<T> predicate)
        {
            return new WhereIterator<T>(
                ArrayProvider<T>.GetWrappedArray(source),
                predicate,
                actualLength: source.Count);
        }
    }
}