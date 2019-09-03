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
        public void TopKFrequent(int[] nums, int k, int[] expectedResult)
        {
            var sut = new Solution();

            var result = sut.TopKFrequent(nums, k);

            Assert.That(result, Is.EquivalentTo(expectedResult));
        }
    }
}