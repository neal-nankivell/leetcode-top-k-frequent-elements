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
            Dictionary<int, Item> map = new Dictionary<int, Item>();
            foreach (int num in nums)
            {
                if (map.TryGetValue(num, out Item item))
                {
                    item.Frequency++;
                }
                else
                {
                    map[num] = new Item(num);
                }
            }

            return TopKFrequent(map.Values, k);
        }

        private IList<int> TopKFrequent(IEnumerable<Item> items, int k)
        {
            List<Item> minHeap = new List<Item>();

            void AddToMinHeap(Item item)
            {
                if (minHeap.Count == k)
                {
                    if (item.Frequency > minHeap[0].Frequency)
                    {
                        AddToStartAndBubbleDown(item);
                    }
                }
                else
                {
                    AddToEndAndBubbleUp(item);
                }
            }

            void AddToStartAndBubbleDown(Item item)
            {
                var index = 0;
                minHeap[index] = item;

                var childIndexA = (index * 2) + 1;
                var childIndexB = (index * 2) + 2;

                var childItemA = childIndexA < minHeap.Count ? minHeap[childIndexA] : null;
                var childItemB = childIndexB < minHeap.Count ? minHeap[childIndexB] : null;

                while (childItemA != null && childItemA.Frequency < item.Frequency ||
                    childItemB != null && childItemB.Frequency < item.Frequency)
                {
                    bool swapWithA = childItemB == null || childItemA.Frequency < childItemB.Frequency;
                    int indexToSwap = swapWithA ? childIndexA : childIndexB;
                    Item itemToSwap = swapWithA ? childItemA : childItemB;

                    minHeap[index] = itemToSwap;
                    minHeap[indexToSwap] = item;

                    index = indexToSwap;

                    childIndexA = (index * 2) + 1;
                    childIndexB = (index * 2) + 2;
                    childItemA = childIndexA < minHeap.Count ? minHeap[childIndexA] : null;
                    childItemB = childIndexB < minHeap.Count ? minHeap[childIndexB] : null;
                }
            }

            void AddToEndAndBubbleUp(Item item)
            {
                var index = minHeap.Count;
                minHeap.Add(item);
                var parentIndex = GetParentIndex(index);
                while (parentIndex.HasValue)
                {
                    var parentItem = minHeap[parentIndex.Value];
                    if (parentItem.Frequency <= item.Frequency)
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

            int? GetParentIndex(int index) =>
                (index == 0) ? (int?)null : (index - 1) / 2;

            foreach (Item item in items)
            {
                AddToMinHeap(item);
            }
            return minHeap.Select(item => item.Value).ToList();

        }

        private class Item
        {
            public Item(int value)
            {
                Value = value;
                Frequency = 1;
            }
            public int Value { get; }
            public int Frequency { get; set; }
        }
    }
}
