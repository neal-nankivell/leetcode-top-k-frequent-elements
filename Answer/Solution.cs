using System;
using System.Collections.Generic;
using System.Linq;

namespace Answer
{
    public class Solution
    {
        /*
        Given a non-empty array of integers,
        return the k most frequent elements.

        Example 1:

        Input: nums = [1,1,1,2,2,3], k = 2
        Output: [1,2]
        Example 2:

        Input: nums = [1], k = 1
        Output: [1]
        Note:

        You may assume k is always valid,
        1 ≤ k ≤ number of unique elements.
        Your algorithm's time complexity must be better
        than O(n log n), where n is the array's size.
         */
        public IList<int> TopKFrequent(int[] nums, int k)
        {
            var frequencyMap = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                if (frequencyMap.ContainsKey(num))
                {
                    frequencyMap[num]++;
                }
                else
                {
                    frequencyMap[num] = 1;
                }
            }

            return frequencyMap
                .OrderByDescending(kvp => kvp.Value)
                .Take(k)
                .Select(kvp => kvp.Key)
                .ToList();
        }
    }
}
