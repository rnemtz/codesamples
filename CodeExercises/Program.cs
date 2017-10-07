using System;

namespace CodeExercises
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var one = new[] { 4, 1, 2, 3 };
            var two = new[] { 1, 3, 4, 2, 6, 7, 9, 8 };
            var result = NextGreaterElement(one, two);

           // Console.ReadLine();
        }


        //Next Greater Element
        public static int[] NextGreaterElement(int[] findNums, int[] nums)
        {
            var result = new int[findNums.Length];
            for (var i = 0; i < findNums.Length; i++)
            {
                result[i] = -1;
                for (var j = Array.IndexOf(nums,findNums[i]); j < nums.Length; j++)
                {
                    if (nums[j] <= findNums[i]) continue;
                    result[i] = nums[j];
                    break;
                }
            }
            return result;
        }
    }
}