using System;
using System.Collections.Generic;
using System.Linq;
using CodeExercises.DataStructures;

namespace CodeExercises
{
    internal class Program
    {
        private static void Main(string[] args)
        {

         
        }

        //Frog Jump
        public static bool CanCross(int[] stones)
        {
            if (stones[1] == 1) return true;
            return stones[1] <= 1 && JumpFrog(stones, 1, 1);
        }

        private static bool JumpFrog(IReadOnlyList<int> stones, int index, int k)
        {
            var canCross = false;
            if (index == stones.Count - 1) return true;
            for (var r = index + 1; r < stones.Count; r++)
            {
                if (stones[r] <= stones[index] + k + 1 && stones[r] >= stones[index] + k - 1)
                {
                    canCross = canCross || JumpFrog(stones, r, stones[r] - stones[index]);
                }
            }
            return canCross;
        }

        //Triangle
        public static int Triangle(int[] a)
        {

            //Non optimal Solution
            for (var p = 0; p < a.Length; p++)
            {
                for (var q = p; q < a.Length; q++)
                {
                    for (var r = q; r < a.Length; r++)
                    {
                        if (a[p] + a[q] > a[r] && a[q] + a[r] > a[p] && a[r] + a[p] > a[q])
                            return 1;
                    }
                }
            }

            return 0;
        }

        //Count Div
        public static int CountDiv(int a, int b, int k)
        {
            var counter = 0;
            for (var i = a; i <= b; i++)
            {
                if (i % k == 0) counter++;
            }
            return counter;
        }

        //Binary Gap
        public static int BinaryGap(int number)
        {
            var bin = Convert.ToString(number, 2);
            if (!bin.Contains("0")) return 0;
            var gap = 0;
            var maxGap = 0;
            var oneFlag = false;
            for (var i = 0; i < bin.Length; i++)
            {
                if (bin[i] == '1')
                {
                    oneFlag = true;
                    gap=0;
                }
                if (!oneFlag) continue;
                if (bin[i] != '0' || i >= bin.Length) continue;

                gap++;
                if (gap > maxGap) maxGap = gap;
            }

            return maxGap;
        }

        //Detect Capital
        public static bool DetectCapitalUse(string word)
        {
            var capitals = word.Count(c => c >= 65 && c <= 90);
            if (capitals == word.Length) return true;

            var lower = word.Count(c=> c >= 97 && c <= 122);
            if (lower == word.Length) return true;

            if (capitals != 1) return false;
            var first = word.First(c => c >= 65 && c <= 90);
            return word[0] == first;
        }

        //Add Digits
        public static int AddDigits(int num)
        {
            var result = num.ToString();
            for (var i = 5; i > 0; i--)
            {
                if (result.Length == 1) return int.Parse(result);
                result = result.ToArray().Sum(x => int.Parse(x.ToString())).ToString();
            }
            return int.Parse(result);
        }

        //Max Depth in Binary Tree
        public static int MaxDepth(TreeNode node)
        {
            if (node == null) return 0;
            var leftDepth = MaxDepth(node.left);
            var rightDepth = MaxDepth(node.right);

            return leftDepth > rightDepth ? leftDepth + 1 : rightDepth + 1;
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int x)
            {
                val = x;
            }
        }

        //Max Consecutive Ones
        public static int FindMaxConsecutiveOnes(int[] nums)
        {
            //More efficient by more than 80%
            var max = 0;
            var current = 0;
            foreach (var i in nums)
            {
                if (i == 1)
                {
                    current++;
                    if (current > max) max = current;
                }
                else current = 0;
            }
            return max;

            //Less efficient solution but quicker to implement.
            //var ones = string.Join(string.Empty, nums).Split('0');
            //var max = ones.OrderByDescending(x => x).First();
            //return max.Length;
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