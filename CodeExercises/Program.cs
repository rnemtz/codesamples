using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeExercises
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.ReadLine();
        }

        public static double MeanSquareError(int[] firstArray, int[] secondArray)
        {
            return firstArray.Select((t, i) => Math.Pow(Math.Abs(t - secondArray[i]), 2)).ToList().Average();
        }

        public static int[] MoveZeroes(int[] arr)
        {
            var result = arr.Where(number => number != 0).ToList();
            for (var i = result.Count; i < arr.Length; i++) result.Add(0);
            return result.ToArray();
        }

        public static int PositiveSum(int[] arr)
        {
            if (arr == null || arr.Length == 0) return 0;
            return arr.Where(t => t > 0).Sum();
        }

        public static int PositiveSumLinq(int[] arr)
        {
            return !arr.Any() ? 0 : arr.Where(x => x > 0).Sum();
        }

        public static long NextBiggerNumber(long n)
        {
            if (n < 10) return -1;
            var digits = n.ToString().ToCharArray();
            var digit = digits[0];
            for (var i = 1; i < digits.Length; i++)
            {
                if (digit != digits[i]) break;
                if (i + 1 == digits.Length) return -1;
            }

            var index = digits.Length - 1;
            var nextNumber = n;
            while (index > 0)
            {
                var swappedDigits = n.ToString().ToCharArray();
                for (var i = index - 1; i >= 0; i--)
                {
                    swappedDigits[index] = digits[i];
                    swappedDigits[i] = digits[index];
                    var swappedNumber = long.Parse(new string(swappedDigits));
                    if (swappedNumber <= n) continue;
                    if (swappedNumber < nextNumber)
                        nextNumber = swappedNumber;
                    if (nextNumber == n) nextNumber = swappedNumber;
                }
                index--;
            }
            if (index == 0 && nextNumber == n) return -1;
            return nextNumber;
        }

        public static int JosSurvivor(int n, int k)
        {
            var survivors = new int[n];
            for (var i = 1; i <= n; i++)
                survivors[i - 1] = i;
            var index = k;
            while (survivors.Sum(x => x) > 0)
                if (index <= n - 1)
                    index += k;
                else
                    index = index - n;
            return survivors.First();
        }

        public static string Highest(string s)
        {
            return s.Split(' ').OrderBy(x => x.Select(y => y - 96).Sum()).Last();
        }

        public static string High(string s)
        {
            var abc = new Dictionary<char, int>
            {
                {'a', 1},
                {'b', 2},
                {'c', 3},
                {'d', 4},
                {'e', 5},
                {'f', 6},
                {'g', 7},
                {'h', 8},
                {'i', 9},
                {'j', 10},
                {'k', 11},
                {'l', 12},
                {'m', 13},
                {'n', 14},
                {'o', 15},
                {'p', 16},
                {'q', 17},
                {'r', 18},
                {'s', 19},
                {'t', 20},
                {'u', 21},
                {'v', 22},
                {'w', 23},
                {'x', 24},
                {'y', 25},
                {'z', 26}
            };
            var words = s.Split(' ');
            var maxWordValue = 0;
            var maxWord = string.Empty;

            foreach (var word in words)
            {
                var currentWordValue = 0;
                currentWordValue += word.ToCharArray().Sum(c => abc[c]);
                if (currentWordValue <= maxWordValue) continue;
                maxWord = word;
                maxWordValue = currentWordValue;
            }
            return maxWord;
        }


        private string ReplaceString(string input, string search, string replace)
        {
            if (input == null) return string.Empty;
            if (search == null || replace == null) return input;

            if (search.Length > input.Length) return input;

            var output = string.Empty;
            var startIndex = 0;
            var endIndex = 0;
            var index = 0;
            var isFound = false;
            var isFirstLetter = true;
            var currentIndex = 0;

            for (var i = 0; i < input.Length; i++)
            {
                currentIndex = i;
                if (input[i] == search[index])
                {
                    if (isFirstLetter) startIndex = i;
                    isFirstLetter = false;
                    if (index == search.Length - 1)
                    {
                        isFound = true;
                        endIndex = i;
                        break;
                    }
                    index++;
                }
                else
                {
                    startIndex = 0;
                    endIndex = 0;
                    isFound = false;
                    isFirstLetter = true;
                }
            }
            if (!isFound) return input;
            output = Replace(startIndex, endIndex, input, replace);
            var isEndOfInput = false;
            index = 0;
            while (!isEndOfInput)
            {
                for (var i = currentIndex + 1; i < input.Length; i++)
                {
                    if (input[i] == search[index])
                    {
                        if (isFirstLetter) startIndex = i;
                        isFirstLetter = false;
                        if (index == search.Length - 1)
                        {
                            isFound = true;
                            endIndex = i;
                            currentIndex = i;
                            break;
                        }
                        index++;
                    }
                    else
                    {
                        index = 0;
                        isFound = false;
                        isFirstLetter = true;
                    }
                    if (i == input.Length - 1) isEndOfInput = true;
                }
                if (isFound) output = Replace(startIndex, endIndex, output, replace);
            }
            return output;
        }

        private string Replace(int startIndex, int endIndex, string input, string replace)
        {
            var output = string.Empty;
            for (var i = 0; i < startIndex; i++)
                output += input[i];
            output += replace;
            for (var i = endIndex + 1; i < input.Length; i++)
                output += input[i];
            return output;
        }


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
                if (stones[r] <= stones[index] + k + 1 && stones[r] >= stones[index] + k - 1)
                    canCross = canCross || JumpFrog(stones, r, stones[r] - stones[index]);
            return canCross;
        }

        //Triangle
        public static int Triangle(int[] a)
        {
            //Non optimal Solution
            for (var p = 0; p < a.Length; p++)
            for (var q = p; q < a.Length; q++)
            for (var r = q; r < a.Length; r++)
                if (a[p] + a[q] > a[r] && a[q] + a[r] > a[p] && a[r] + a[p] > a[q])
                    return 1;

            return 0;
        }

        //Count Div
        public static int CountDiv(int a, int b, int k)
        {
            var counter = 0;
            for (var i = a; i <= b; i++)
                if (i % k == 0) counter++;
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
                    gap = 0;
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

            var lower = word.Count(c => c >= 97 && c <= 122);
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

        //Max Consecutive Ones
        public static int FindMaxConsecutiveOnes(int[] nums)
        {
            //More efficient by more than 80%
            var max = 0;
            var current = 0;
            foreach (var i in nums)
                if (i == 1)
                {
                    current++;
                    if (current > max) max = current;
                }
                else
                {
                    current = 0;
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
                for (var j = Array.IndexOf(nums, findNums[i]); j < nums.Length; j++)
                {
                    if (nums[j] <= findNums[i]) continue;
                    result[i] = nums[j];
                    break;
                }
            }
            return result;
        }

        public class TreeNode
        {
            public TreeNode left;
            public TreeNode right;
            public int val;

            public TreeNode(int x)
            {
                val = x;
            }
        }
    }
}