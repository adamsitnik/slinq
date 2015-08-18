using NUnit.Framework;
using Slinq.Extensions;

namespace Slinq.Tests
{
    [TestFixture]
    public class ArrayWhereIteratorTests
    {
        [Test]
        public void WhereReturnsItemsThatMatchPredicate()
        {
            var numbers = new[] { 1, 2, 3, 4 };

            var filtered = numbers.Where(number => number % 2 != 0);

            Assert.That(filtered.Any(number => number % 2 == 0), Is.False);
        }
    }
}