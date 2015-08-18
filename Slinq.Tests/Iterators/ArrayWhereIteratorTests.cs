using System.Linq;
using NUnit.Framework;
using Slinq.Extensions;

namespace Slinq.Tests.Iterators
{
    [TestFixture]
    public class ArrayWhereIteratorTests
    {
        [Test]
        public void WhereReturnsItemsThatMatchPredicate()
        {
            var numbers = Enumerable.Range(1, 4).ToArray();

            var filtered = numbers.Where(number => number % 2 != 0);

            Assert.That(filtered.Any(number => number % 2 == 0), Is.False);
        }

        [Test]
        public void WhereTrailsArrayTakenFromListToElementsThatWereAddedToListOnly()
        {
            var numbers = Enumerable.Range(1, 2).ToList();

            var filtered = numbers.Where(number => number == default(int));

            // todo: add .ToArray() Extension, check length and content instead
            Assert.That(filtered.Any(), Is.False);
        }
    }
}