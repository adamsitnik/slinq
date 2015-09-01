using NUnit.Framework;
using Slinq.Utils;

namespace Slinq.Tests.Utils
{
    [TestFixture]
    public class DedicatedSortersFactoryTests
    {
        [Test]
        public void CanCreateADumbSorterWithoutCrash()
        {
            var arrray = new[] { 1, 2, 3 };

            var sorter = DedicatedSortersFactory.CreateDedicatedSorter<int>();

            Assert.IsNotNull(sorter);

            Assert.DoesNotThrow(() => sorter.Sort(arrray));
        }
    }
}