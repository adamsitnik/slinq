using System;
using Slinq.Abstract;

namespace Slinq.Extensions
{
    public static class StrongEnumeratorExtensions
    {
        public static bool Any<T, TEnumerator>(this IStrongEnumerable<T, TEnumerator> source)
            where TEnumerator : struct, IStrongEnumerator<T>
        {
            return source.GetEnumerator().MoveNext();
        }

        public static bool Any<T, TEnumerator>(this IStrongEnumerable<T, TEnumerator> source, Predicate<T> predicate)
            where TEnumerator : struct, IStrongEnumerator<T>
        {
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (predicate.Invoke(enumerator.Current))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// possible further optimizations: move from the end to the start
        /// </summary>
        public static T Last<T, TEnumerator>(this IStrongEnumerable<T, TEnumerator> source)
            where TEnumerator : struct, IStrongEnumerator<T>
        {
            var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            while (enumerator.MoveNext())
            {
            }

            return enumerator.Current;
        }

        public static int Sum<TEnumerator>(this IStrongEnumerable<int, TEnumerator> source)
            where TEnumerator : struct, IStrongEnumerator<int>
        {
            int sum = 0;
            foreach (var number in source)
            {
                sum += number;
            }

            return sum;
        }
    }
}