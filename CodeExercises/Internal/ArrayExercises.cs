
using System.Collections.Generic;
using System.Linq;

namespace CodeExercises.Internal
{
    public class ArrayExercises
    {
        public ArrayExercises()
        {
            // Single-dimensional array. 
            var array1 = new int[5];

            // Set array element values.
            var array2 = new[] {1, 3, 5, 7, 9};

            // Alternative.
            int[] array3 = {1, 2, 3, 4, 5, 6};

            // Two dimensional array.
            var multiDimensionalArray1 = new int[2, 3];

            // Set array element values.
            int[,] multiDimensionalArray2 = {{1, 2, 3}, {4, 5, 6}};

            // Jagged array.
            var jaggedArray = new int[6][];

            // Set the values of the first array in the jagged array structure.
            jaggedArray[0] = new[] {1, 2, 3, 4};
        }


        /*
         * Remove Duplicates from SORTED ARRAY
         *
         * Given a sorted array nums, remove the duplicates in-place such that each element appear only once and
         * return the new length.
         *
         * Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1)
         * extra memory.
         *
         *
         */

        public int RemoveDuplicates(int[] numbers)
        {
            if (numbers.Length == 0)
            {
                return 0;
            }

            var i = 0;
            for (var j = 1; j < numbers.Length; j++)
            {
                if (numbers[j] == numbers[i])
                {
                    continue;
                }

                i++;
                numbers[i] = numbers[j];
            }

            return i + 1;
        }

        public int[] RemoveDuplicateNumbers(int[] numbers)
        {
            if (numbers == null || !numbers.Any())
            {
                return null;
            }

            var slow = 0;
            for (var fast = 1; fast < numbers.Length; fast++)
            {
                if (numbers[slow] == numbers[fast])
                {
                    continue;
                }

                slow++;
                numbers[slow] = numbers[fast];
            }

            return numbers;
        }

        /*
         * Two Sum
         * Given an array of integers, return indices of the two numbers such that they add up to a specific target.
         * You may assume that each input would have exactly one solution, and you may not use the same element twice.
         */

        public int[] TwoSum(int[] numbers, int target)
        {
            var sums = new Dictionary<int, int>();
            for (var i = 0; i < numbers.Length; i++)
            {
                if (sums.ContainsKey(numbers[i]))
                {
                    return new[] { sums[numbers[i]], i };
                }

                if (!sums.ContainsKey(target - numbers[i]))
                {
                    sums.Add(target - numbers[i], i);
                }
            }
            return null;
        }
    }
}
