using System.Collections.Generic;
using Answer;
using NUnit.Framework;

namespace Tests
{
    public class SolutionTests
    {
        [TestCase(
            new[] { 1, 1, 1, 2, 2, 3 },
            2,
            new[] { 1, 2 }
        )]
        [TestCase(
            new[] { 1 },
            1,
            new[] { 1 }
        )]
        [TestCase(
            new[] { 5, 2, 5, 3, 5, 3, 1, 1, 3 },
            2,
            new[] { 3, 5 }
        )]
        [TestCase(
            new[] { 4, 1, -1, 2, -1, 2, 3 },
            2,
            new[] { -1, 2 }
        )]
        public void TopKFrequent(int[] nums, int k, int[] expectedResult)
        {
            var sut = new Solution();

            var result = sut.TopKFrequent(nums, k);

            Assert.That(result, Is.EquivalentTo(expectedResult));
        }
    }
}