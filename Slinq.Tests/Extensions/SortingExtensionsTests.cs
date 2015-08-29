﻿using System;
using System.Linq;
using NUnit.Framework;
using Slinq.Extensions;
using Slinq.Utils;

namespace Slinq.Tests.Extensions
{
    [TestFixture]
    public class SortingExtensionsTests
    {
        [Test]
        public void WhenNoRulesAreSpecifiedTheDefaultComparerShouldBeUsed()
        {
            var randomNumbers = GenerateRandomNumbers();
            var sortedByArraySort = randomNumbers.ToArray();
            Array.Sort(sortedByArraySort);

            var sortedByOptimizedArraySort = randomNumbers.ToArray();
            ArraySorter.IntrospectiveSort(sortedByOptimizedArraySort, 0, randomNumbers.Length - 1);

            CollectionAssert.AreEqual(sortedByArraySort, sortedByOptimizedArraySort);
        }

        [Test]
        public void SingleSortingRulesShouldBeRespected()
        {
            var randomNumbers = GenerateRandomNumbers();
            var sortedByLinq = Enumerable.OrderBy(randomNumbers, number => number).ToArray();

            var result = SortingExtensions.OrderBy(randomNumbers, number => number).ToArray();

            CollectionAssert.AreEqual(sortedByLinq, randomNumbers);
        }

        [Test]
        public void AllSortingRulesShouldBeRespected()
        {
            var randomDates = GenerateRandomDates();
            var sortedByLinq = Enumerable.OrderBy(randomDates, date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();

            var result = SortingExtensions.OrderBy(randomDates, date => date.Year).ThenBy(date => date.Month).ThenBy(date => date.Day).ToArray();

            CollectionAssert.AreEqual(sortedByLinq, result);
        }

        private int[] GenerateRandomNumbers()
        {
            var random = new Random();
            
            return Enumerable.Range(1, 1000).Select(_ => random.Next()).ToArray();
        }

        private DateTime[] GenerateRandomDates()
        {
            var random = new Random();

            return Enumerable.Range(1, 1000)
                .Select(_ => new DateTime(random.Next(2000, 2015), random.Next(1,  12), random.Next(1, 28)))
                .ToArray();
        }
    }
}