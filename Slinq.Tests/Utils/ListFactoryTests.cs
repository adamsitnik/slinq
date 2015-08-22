using NUnit.Framework;
using Slinq.Extensions;
using Slinq.Utils;

namespace Slinq.Tests.Utils
{
    [TestFixture]
    public class ListFactoryTests
    {
        [Test]
        public void CreateFromArrayShouldNotAllocateAnyMemory()
        {
            var array = StrongEnumerable.Range(0, 10).Select(_ => _).ToArray();

            var list = ListFactory<int>.Create(array);
            var extractedArray = ArrayProvider<int>.GetWrappedArray(list);

            Assert.AreSame(array, extractedArray);
            CollectionAssert.AreEqual(array, list);
        }
    }
}