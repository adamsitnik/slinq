using System.Linq;
using NUnit.Framework;
using Slinq.Utils;

namespace Slinq.Tests.Utils
{
    [TestFixture]
    public class ArrayProviderTests
    {
        [Test]
        public void ShouldReturnArrayThatListJustWraps()
        {
            var numbers = Enumerable.Range(1, 5).ToList();

            var wrappedArray = ArrayProvider<int>.GetWrappedArrayWithDynamicCilGeneration(numbers);

            for (int i = 0; i < 5; i++)
            {
                Assert.That(numbers[i], Is.EqualTo(wrappedArray[i]));
            }
        }
    }
}