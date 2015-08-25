using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Slinq.Extensions;

namespace Slinq.Tests.Extensions
{
    [TestFixture]
    public class MathExtensionsTests
    {
        [Test]
        public void SumArrayShouldReturnSumOfAllElements()
        {
            CompareResults(GenerateRandomNumbers().ToArray(), Enumerable.Sum, ArrayExtensions.Sum);
        }

        [Test]
        public void SumListShouldReturnSumOfAllElements()
        {
            CompareResults(GenerateRandomNumbers().ToList(), Enumerable.Sum, ListExtensions.Sum);
        }

        [Test]
        public void SumReadOnlyCollectionShouldReturnSumOfAllElements()
        {
            CompareResults(new ReadOnlyCollection<int>(GenerateRandomNumbers().ToArray()), Enumerable.Sum, ReadOnlyCollectionExtensions.Sum);
        }

        [Test]
        public void MinArrayShouldReturnTheLowestValue()
        {
            CompareResults(GenerateRandomNumbers().ToArray(), Enumerable.Min, ArrayExtensions.Min);
        }

        [Test]
        public void MinListShouldReturnTheLowestValue()
        {
            CompareResults(GenerateRandomNumbers().ToList(), Enumerable.Min, ListExtensions.Min);
        }

        [Test]
        public void MinReadOnlyCollectionShouldReturnTheLowestValue()
        {
            CompareResults(new ReadOnlyCollection<int>(GenerateRandomNumbers().ToList()), Enumerable.Min, ReadOnlyCollectionExtensions.Min);
        }

        [Test]
        public void MaxArrayShouldReturnTheBiggestValue()
        {
            CompareResults(GenerateRandomNumbers().ToArray(), Enumerable.Max, ArrayExtensions.Max);
        }

        [Test]
        public void MaxListShouldReturnTheBiggestValue()
        {
            CompareResults(GenerateRandomNumbers().ToList(), Enumerable.Max, ListExtensions.Max);
        }

        [Test]
        public void MaxReadOnlyCollectionShouldReturnTheBiggestValue()
        {
            CompareResults(new ReadOnlyCollection<int>(GenerateRandomNumbers().ToList()), Enumerable.Max, ReadOnlyCollectionExtensions.Max);
        }

        private static void CompareResults<TSource, TResult>(
            TSource source,
            Func<TSource, TResult> expected,
            Func<TSource, TResult> sut)
        {
            Assert.AreEqual(expected(source), sut(source));
        }

        private static IEnumerable<int> GenerateRandomNumbers()
        {
            var random = new Random();
            int elementsCount = 110;

            return Enumerable.Range(1, elementsCount).Select(i => random.Next(int.MaxValue / elementsCount));
        }
    }
}