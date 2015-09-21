using System;
using System.Linq;
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
            var sorter = DedicatedSortersFactory.CreateDedicatedSorter<DateTime>();

            Assert.IsNotNull(sorter);
        }

        [Test]
        public void CanSortDates()
        {
            var sourceArray = new[] { DateTime.MaxValue, DateTime.MinValue, DateTime.Now };
            var expected = sourceArray.ToArray();
            Array.Sort(expected);
            var sorter = DedicatedSortersFactory.CreateDedicatedSorter<DateTime>();

            sorter.Sort(sourceArray);

            CollectionAssert.AreEqual(expected, sourceArray);
        }

        [Test]
        public void CanSortIntegers()
        {
            var sourceArray = new[] { 10, 20, 1, -4, 5, 6, 11, 3443 };
            var expected = sourceArray.ToArray();
            Array.Sort(expected);
            var sorter = DedicatedSortersFactory.CreateDedicatedSorter<int>();

            sorter.Sort(sourceArray);

            CollectionAssert.AreEqual(expected, sourceArray);
        }

        [Test]
        public void CanSortDoubles()
        {
            var sourceArray = new[] { 10.1, 20.2, 1.343, -4.121, 5.23, 6.5, 11.77, 3443.23 };
            var expected = sourceArray.ToArray();
            Array.Sort(expected);
            var sorter = DedicatedSortersFactory.CreateDedicatedSorter<double>();

            sorter.Sort(sourceArray);

            CollectionAssert.AreEqual(expected, sourceArray);
        }

        [Test]
        public void CanSortFloats()
        {
            var sourceArray = new[] { 10.1f, 20.2f, 1.343f, -4.121f, 5.23f, 6.5f, 11.77f, 3443.23f };
            var expected = sourceArray.ToArray();
            Array.Sort(expected);
            var sorter = DedicatedSortersFactory.CreateDedicatedSorter<float>();

            sorter.Sort(sourceArray);

            CollectionAssert.AreEqual(expected, sourceArray);
        }
    }
}