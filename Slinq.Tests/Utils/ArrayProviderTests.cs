using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            var wrappedArray = ArrayProvider<int>.GetWrappedArray(numbers);

            for (int i = 0; i < 5; i++)
            {
                Assert.That(numbers[i], Is.EqualTo(wrappedArray[i]));
            }
        }

        [Test]
        public void ShouldReturnArrayThatReadOnlyCollectionCreatedFromArrayJustWraps()
        {
            EnsureThatArrayProviderReturnsWrappedArray(Enumerable.Range(1, 5).ToArray());
        }

        [Test]
        public void ShouldReturnArrayThatReadOnlyCollectionCreatedFromListJustWraps()
        {
            EnsureThatArrayProviderReturnsWrappedArray(Enumerable.Range(1, 5).ToList());
        }

        [Test]
        public void ShouldFailWithNotSupportedExceptionForReadonlyCollectionThatIsNotCreatedFromArrayEitherList()
        {
            IList<int> customCollection = new MyCustomCollection<int>();

            var readOnlyCollection = new ReadOnlyCollection<int>(customCollection);

            Assert.That(
                () => ArrayProvider<int>.GetWrappedArray(readOnlyCollection), 
                Throws.InstanceOf<NotSupportedException>());
        }

        private void EnsureThatArrayProviderReturnsWrappedArray(IList<int> numbers)
        {
            var readOnlyCollection = new ReadOnlyCollection<int>(numbers);

            var wrappedArray = ArrayProvider<int>.GetWrappedArray(readOnlyCollection);

            for (int i = 0; i < 5; i++)
            {
                Assert.That(numbers[i], Is.EqualTo(wrappedArray[i]));
            }
        }

        private class MyCustomCollection<T> : IList<T>
        {
            public int Count { get; private set; }

            public bool IsReadOnly { get; private set; }

            public T this[int index]
            {
                get { throw new System.NotImplementedException(); }
                set { throw new System.NotImplementedException(); }
            }

            public IEnumerator<T> GetEnumerator()
            {
                throw new System.NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public void Add(T item)
            {
                throw new System.NotImplementedException();
            }

            public void Clear()
            {
                throw new System.NotImplementedException();
            }

            public bool Contains(T item)
            {
                throw new System.NotImplementedException();
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                throw new System.NotImplementedException();
            }

            public bool Remove(T item)
            {
                throw new System.NotImplementedException();
            }

            public int IndexOf(T item)
            {
                throw new System.NotImplementedException();
            }

            public void Insert(int index, T item)
            {
                throw new System.NotImplementedException();
            }

            public void RemoveAt(int index)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}