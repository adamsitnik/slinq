﻿using System;
using System.Linq;
using NUnit.Framework;
using Slinq.Extensions;

namespace Slinq.Tests.Extensions
{
    [TestFixture]
    public class SortingExtensionsTests
    {
        [Test]
        public void SingleSortingRulesShouldBeRespected()
        {
            var randomNumbers = GenerateRandomNumbers();
            var sortedByLinq = Enumerable.OrderBy(randomNumbers, number => number).ToArray();

            var result = SortingExtensions.OrderBy(randomNumbers, number => number).ToArray();

            CollectionAssert.AreEqual(sortedByLinq, result);
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