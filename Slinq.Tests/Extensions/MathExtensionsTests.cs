using System;
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
            var array = Enumerable.Range(1, 20).ToArray();
            var expected = Enumerable.Sum(array);

            var sum = ArrayExtensions.Sum(array);

            Assert.AreEqual(expected, sum);
        }

        [Test]
        public void SumListShouldReturnSumOfAllElements()
        {
            var list = Enumerable.Range(1, 20).ToList();
            var expected = Enumerable.Sum(list);

            var sum = ListExtensions.Sum(list);

            Assert.AreEqual(expected, sum);
        }

        [Test]
        public void SumReadOnlyCollectionShouldReturnSumOfAllElements()
        {
            var readOnlyCollection = new ReadOnlyCollection<int>(Enumerable.Range(1, 20).ToArray());
            var expected = Enumerable.Sum(readOnlyCollection);

            var sum = ReadOnlyCollectionExtensions.Sum(readOnlyCollection);

            Assert.AreEqual(expected, sum);
        }
    }
}