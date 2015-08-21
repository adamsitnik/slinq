using System.Linq;
using NUnit.Framework;
using Slinq.Extensions;

namespace Slinq.Tests.Iterators
{
    [TestFixture]
    public class RangeSelectIteratorTests
    {
        [Test]
        public void ToArrayShouldReturnTheSameResultAsMsLinqToArray()
        {
            var expectedResult = Enumerable.Range(100, 200).Select(number => number - 50).ToArray();

            var result = StrongEnumerable.Range(100, 200).Select(number => number - 50).ToArray();

            CollectionAssert.AreEqual(expectedResult, result);
        }
    }
}