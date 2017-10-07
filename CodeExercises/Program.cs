using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeExercises
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var one = new[] { 1, 1, 0, 1, 1, 1 };
            var two = new[] { 1, 3, 4, 2, 6, 7, 9, 8 };
            var result = SingleNumber(one);

            // Console.ReadLine();
        }

        //Single Number
        public static int SingleNumber(int[] nums)
        {
            var num = nums.GroupBy(x => x).SingleOrDefault(y => y.Count() == 1).Key;
            return num;
        }
        //Fizz Buz
        public static IList<string> FizzBuzz(int n)
        {
            var list = new List<string>();
            for (var c = 1; c <= n; c++)
            {
                var word = c.ToString();
                
                switch (c % 3)
                {
                    case 0 when c % 5 == 0:
                        word = "FizzBuzz";
                        break;
                    case 0:
                        word = "Fizz";
                        break;
                    default:
                        if (c % 5 == 0) word = "Buzz";
                        break;
                }
                list.Add(word);
            }
            return list;
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