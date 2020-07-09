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
        public IList<int> TopKFrequent(int[] nums, int k) =>
            TopKFrequent(CalculateFrequency(nums), k);

        public IEnumerable<(int value, int frequency)> CalculateFrequency(IEnumerable<int> nums)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                map.TryGetValue(num, out int frequency);
                map[num] = ++frequency;
            }
            return map.Select(kvp => (value: kvp.Key, frequency: kvp.Value));
        }

        private IList<int> TopKFrequent(IEnumerable<(int value, int frequency)> items, int k)
        {
            List<(int value, int frequency)> minHeap = new List<(int value, int frequency)>();

            foreach (var item in items)
            {
                AddToMinHeap(item);
            }
            return minHeap.Select(item => item.value).ToList();

            int? GetParentIndex(int index) =>
                (index == 0) ? (int?)null : (index - 1) / 2;

            void AddToMinHeap((int value, int frequency) item)
            {
                if (minHeap.Count == k)
                {
                    if (item.frequency > minHeap[0].frequency)
                    {
                        AddToStartAndBubbleDown(item);
                    }
                }
                else
                {
                    AddToEndAndBubbleUp(item);
                }
            }

            void AddToStartAndBubbleDown((int value, int frequency) item)
            {
                var index = 0;
                minHeap[index] = item;

                var childIndexA = (index * 2) + 1;
                var childIndexB = (index * 2) + 2;

                var childItemA = childIndexA < minHeap.Count ? minHeap[childIndexA] : default;
                var childItemB = childIndexB < minHeap.Count ? minHeap[childIndexB] : default;

                while (childItemA != default && childItemA.frequency < item.frequency ||
                    childItemB != default && childItemB.frequency < item.frequency)
                {
                    bool swapWithA = childItemB == default || childItemA.frequency < childItemB.frequency;
                    int indexToSwap = swapWithA ? childIndexA : childIndexB;
                    var itemToSwap = swapWithA ? childItemA : childItemB;

                    minHeap[index] = itemToSwap;
                    minHeap[indexToSwap] = item;

                    index = indexToSwap;

                    childIndexA = (index * 2) + 1;
                    childIndexB = (index * 2) + 2;
                    childItemA = childIndexA < minHeap.Count ? minHeap[childIndexA] : default;
                    childItemB = childIndexB < minHeap.Count ? minHeap[childIndexB] : default;
                }
            }

            void AddToEndAndBubbleUp((int value, int frequency) item)
            {
                var index = minHeap.Count;
                minHeap.Add(item);
                var parentIndex = GetParentIndex(index);
                while (parentIndex.HasValue)
                {
                    var parentItem = minHeap[parentIndex.Value];
                    if (parentItem.frequency <= item.frequency)
                    {
                        break;
                    }
                    else
                    {
                        minHeap[parentIndex.Value] = item;
                        minHeap[index] = parentItem;
                        index = parentIndex.Value;
                        parentIndex = GetParentIndex(index);
                    }
                }
            }
        }
    }
}
