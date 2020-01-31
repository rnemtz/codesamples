using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeExercises
{
    public class Wayfair
    {
        #region WAYFAIR

        public static List<int[]> GetPermutations(int[] array)
        {
            var results = new List<int[]>();
            Permutations(array, 0, results);
            return results;
        }

        public static List<string> GetPermutations(string s)
        {
            var results = new List<string>();
            Permutations(s.ToCharArray(), 0, results);
            return results;
        }

        private static void Permutations(int[] array, int index, ICollection<int[]> list)
        {
            if (index >= array.Length) list.Add(array.Clone() as int[]);
            else
                for (var i = index; i < array.Length; i++)
                {
                    Swap(ref array, i, index);
                    Permutations(array, index + 1, list);
                    Swap(ref array, i, index);
                }
        }

        private static void Permutations(char[] array, int index, ICollection<string> list)
        {
            if (index >= array.Length) list.Add(string.Join(string.Empty, array));
            else
            {
                for (var i = index; i < array.Length; i++)
                {
                    Swap(ref array, i, index);
                    Permutations(array, index + 1, list);
                    Swap(ref array, i, index);
                }
            }
        }

        public struct Interval
        {
            public int Buy;
            public int Sell;
        }

        /*
         * Stock Buy sell to maximize profit
         * 
         */
        public static void MaximizeProfit(int[] values, int n, out Interval[] results)
        {
            if (n <= 1)
            {
                results = null;
                return;
            }
            results = new Interval[n / 2 + 1];
            var count = 0;
            var i = 0;
            while (i < n - 1)
            {
                //Find local minima
                while (i < n - 1 && values[i + 1] <= values[i]) i++;
                if (i == n - 1) break;
                results[count].Buy = i++;

                //Find local maxima
                while (i < n && values[i] >= values[i - 1]) i++;
                results[count].Sell = i - 1;

                count++;
            }
        }

        public static int GetMaxProfixSum(int[] values)
        {
            if (values.Length <= 1) return 0;
            var results = new List<KeyValuePair<int, int>>();
            var index = 0;
            var length = values.Length;
            while (index < length - 1)
            {
                //find  next min
                while (index < length - 1 && values[index + 1] <= values[index]) index++;
                if (index == length - 1) break;
                var buy = index++;

                //find next max
                while (index <= length - 1 && values[index] >= values[index - 1]) index++;
                var sell = index - 1;
                results.Add(new KeyValuePair<int, int>(buy, sell));
            }

            var sum = 0;
            foreach (var inter in results) sum += values[inter.Value] - values[inter.Key];

            return sum;
        }


        private static bool ContainsSubstringBooyerMoore(string source, string sub)
        {
            //preprocess the string
            //build a table that contains the length to shift when a bad match occurs
            var dis = GetBadMatchTable(sub);
            //stage 2
            //string to find is searched from the last character to the first
            //bad match table is used to skip the chars when a mismatch occurs
            var startIndex = 0;
            while (startIndex <= source.Length - sub.Length)
            {
                var leftToMatch = sub.Length - 1;
                while (leftToMatch >= 0 && sub[leftToMatch] == source[startIndex + leftToMatch])
                    leftToMatch--;

                if (leftToMatch < 0)
                    return true;
                startIndex = dis[sub[startIndex + sub.Length - 1]];
            }
            return false;
        }

        private static Dictionary<int, int> GetBadMatchTable(string sub)
        {
            var defaultValue = sub.Length;
            var distances = new Dictionary<int, int>();
            for (var i = 0; i < defaultValue - 1; i++)
                distances[sub[i]] = sub.Length - i - 1;
            return distances;
        }

        private static bool ContainsSubstringNaive(string source, string sub)
        {
            for (var i = 0; i < source.Length; i++)
            {
                var count = 0;
                while (source[i + count] == sub[count])
                {
                    count++;
                    if (sub.Length == count) return true;
                }
            }
            return false;
        }

        /*
         * Remove duplicates from an array
         */

        /*
            Adding repeated numbers to a set it won't cause an exception it will discard them only
            and mantain the order when you return it as an array;
        */
        public static int[] RemoveDuplicates(int[] array)
        {
            var temp = new HashSet<int>();
            foreach (var n in array) temp.Add(n);
            return temp.ToArray();
        }
        /*
         * Given a two-dimensional array of strings, return all possible combination of words. <-cartesian product
         * Example:
         * ['grey','black']
         * ['fox','dog']
         * ['jumped','ran','growled']
         * 
         * Return:
         * grey fox jumped
         * grey fox ran
         * grey fox growled
         * black fox jumped
         * ...
         * black dog growled  
         * 
         */


        public static List<string> GetCartesianMatrix(string[][] array)
        {
            var result = new List<string>();
            // build a matrix of indexes
            //size arr.length * combinations
            var items = 1;
            foreach (var row in array) items *= row.Length;
            var matrix = new int[array.Length, items];
            var prevRep = 1;
            for (var r = 0; r < array.Length; r++)
            {
                prevRep *= array[r].Length;
                var repetitions = items / prevRep;
                var c = 0;
                var index = 0;
                while (c < items)
                {
                    for (var i = 0; i < repetitions; i++) matrix[r, c++] = index;
                    index++;
                    if (index >= array[r].Length) index = 0;
                }
            }

            //fill matrix
            for (var c = 0; c < matrix.GetLength(1); c++)
            {
                var str = new StringBuilder();
                for (var r = 0; r < matrix.GetLength(0); r++) str.Append($"{array[r][matrix[r, c]]} ");
                result.Add(str.ToString().TrimEnd());
            }

            return result;
        }

        public static List<string> GetCartesianProduct(string[][] array)
        {
            //Build a matrix of indexes. Combinations of all strings
            var itemsPerRow = 1;
            foreach (var row in array)
                itemsPerRow *= row.Length;

            var matrix = new int[array.Length, itemsPerRow];
            var previousRep = 1;
            for (var r = 0; r < array.Length; r++)
            {
                previousRep *= array[r].Length;
                var repetitions = itemsPerRow / previousRep;
                var index = 0;
                var c = 0;
                while (c < itemsPerRow)
                {
                    for (var i = 0; i < repetitions; i++) matrix[r, c++] = index;
                    index++;
                    if (index >= array[r].Length) index = 0;
                }
            }

            var result = new List<string>();
            //Fill the List with indexes in matrix
            for (var c = 0; c < matrix.GetLength(1); c++)
            {
                var str = new StringBuilder();
                for (var r = 0; r < matrix.GetLength(0); r++)
                    str.Append($"{array[r][matrix[r, c]]} ");

                result.Add(str.ToString().TrimEnd());
            }

            return result;
        }

        public static List<string> GetCombinations(string[][] array)
        {
            var itemsInArray = array.Aggregate(1, (current, line) => current * line.Length);
            var matrix = new int[array.Length, itemsInArray];
            var ant = 1;
            for (var c = 0; c < array.Length; c++)
            {
                ant *= array[c].Length;
                var rep = itemsInArray / ant;
                var index = 0;
                var r = 0;
                while (r < itemsInArray)
                {
                    for (var i = 0; i < rep; i++) matrix[c, r++] = index;
                    index++;
                    if (index >= array[c].Length) index = 0;
                }
            }
            var result = new List<string>();
            for (var c = 0; c < matrix.GetLength(1); c++)
            {
                var str = new StringBuilder();
                for (var r = 0; r < matrix.GetLength(0); r++) str.Append($"{array[r][matrix[r, c]]} ");
                result.Add(str.ToString().Trim());
            }
            return result;
        }


        /*
         * You are given a one dimensional array that may contain both positive and negative integers, 
         * find the sum of contiguous xay of numbers which has the largest sum.
         * For example, if the given array is {-2, -5, 6, -2, -3, 1, 5, -6}, then the 
         * maximum subarray sum is 7 (see highlighted elements).
         * 
         * KADANE's Algorithm O(N)
         * 
         */
        public static int MaximumSubArray(int[] a)
        {
            var maxSoFar = int.MinValue;
            var maxEndingHere = 0;

            foreach (var number in a)
            {
                maxEndingHere += number;
                if (maxSoFar < maxEndingHere) maxSoFar = maxEndingHere;
                if (maxEndingHere < 0) maxEndingHere = 0;
            }

            return maxSoFar;


            //var maxH = 0;
            //var maxS = int.MinValue;
            //foreach (var n in a)
            //{
            //    maxH += n;
            //    if (maxH < 0) maxH = 0;
            //    if (maxH > maxS) maxS = maxH;
            //}
            //return maxS;
        }

        /*
         * Given an array of integers, negative and positive, return the array with all negative numbers to the left and positive numbers
         * to the right, without changing the order of the positives
         */

        public static int[] SwapNegativesAndPositives(int[] numbers)
        {
            if (numbers.Length < 2) return numbers;
            var lIndex = 0;
            for (var i = 0; i < numbers.Length; i++) if (numbers[i] < 0) ShiftValues(ref numbers, lIndex++, i - 1);
            return numbers;
        }

        public static void ShiftValues(ref int[] array, int lIndex, int index)
        {
            for (var i = index; i >= lIndex; i--)
                Swap(ref array, i, i + 1);
        }

        /*
         * Get Tree Depth
         */
        public static int BinaryTreeDepth(BinarySearchTreeNode node)
        {
            if (node == null) return 0;
            return Math.Max(BinaryTreeDepth(node.Left), BinaryTreeDepth(node.Right)) + 1;
        }

        public static HashSet<int> FindNonDuplicateHs(int[] list)
        {
            var hd = new HashSet<int>();
            var hn = new HashSet<int>();
            foreach (var c in list) if (hn.Add(c) == false) hd.Add(c);
            hn.SymmetricExceptWith(hd.AsEnumerable());
            return hn;
        }

        public static int FindNonDuplicate(int[] list)
        {
            var dict = new Dictionary<int, int>();
            foreach (var n in list)
                if (dict.ContainsKey(n)) dict[n]++;
                else dict.Add(n, 1);
            return dict.SingleOrDefault(x => x.Value == 1).Key;
        }

        public static bool IsPalindrome(string str)
        {
            //Remove spaces
            //Reverse string
            //return str is equal to reversed string
            return true;
        }

        /*
         * Reverse a string
         */
        public static string ReverseString(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return str;
            var len = str.Length;
            var array = str.ToCharArray();
            for (var i = 0; i < len / 2; i++)
                Swap(ref array, i, len - 1 - i);
            return string.Join(string.Empty, array);
        }

        public static void Swap(ref char[] array, int source, int target)
        {
            var temp = array[source];
            array[source] = array[target];
            array[target] = temp;
        }

        public static void Swap(ref int[] array, int source, int target)
        {
            var temp = array[source];
            array[source] = array[target];
            array[target] = temp;
        }

        /*
         * Determine whether invalid {}, [], () exists in a string
         */

        public static bool IsCorrectNotation(string notation)
        {
            if (string.IsNullOrWhiteSpace(notation)) return false;
            var stk = new Stack<char>();
            var openings = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' } };
            var closing = new Dictionary<char, char> { { ')', '(' }, { ']', '[' }, { '}', '{' } };
            foreach (var c in notation)
                if (openings.ContainsKey(c)) stk.Push(c);
                else if (closing.ContainsKey(c)) if (openings[stk.Pop()] != c) return false;
            return stk.Count == 0;
        }

        /*
         * find a way to separate money into n parts. 
         * For example, if total money is 121, find a way to separate 121 into 4 closest parts and the solution 
         * would be an array with elements 30, 30, 30, 31  
         */

        public static int[] GetQuarts(int n)
        {
            if (n < 4) return new[] { 0 };
            var numbers = new int[4];
            var parts = n / 4;
            numbers[0] = parts;
            numbers[1] = parts;
            numbers[2] = parts;
            numbers[3] = parts + n % 4;
            return numbers;
        }

        /*
         * Is anagram of a given word
         */

        public static bool IsAnagram(string s1, string s2)
        {
            var chars = new Dictionary<char, int>();
            foreach (var c in s1.ToCharArray())
            {
                if (c == ' ') continue;
                if (chars.ContainsKey(c)) chars[c]++;
                else chars.Add(c, 1);
            }
            foreach (var c in s2.ToCharArray())
            {
                if (c == ' ') continue;
                if (!chars.ContainsKey(c)) return false;
                chars[c]--;
                if (chars[c] < 0) return false;
            }
            return true;
        }

        /*
         * Convert number to words
         * EX: 100 -> one hundred
         */
        public static string ConvertNumberToWord(int number)
        {
            if (Math.Abs(number) == 0) return "zero";

            //Handle negative numbers
            var result = string.Empty;
            if (number < 0)
            {
                result = "minus ";
                number *= -1;
            }

            var words = new Dictionary<int, string>[2];
            // units
            words[0] = new Dictionary<int, string>
            {
                {1, "one"},
                {2, "two"},
                {3, "three"},
                {4, "four"},
                {5, "five"},
                {6, "six"},
                {7, "seven"},
                {8, "eight"},
                {9, "nine"}
            };
            // tens
            words[1] = new Dictionary<int, string>
            {
                {1, "ten"},
                {2, "twenty"},
                {3, "thirty"},
                {4, "forty"},
                {5, "fifty"},
                {6, "sixty"},
                {7, "seventy"},
                {8, "eighty"},
                {9, "ninety"}
            };

            var length = number.ToString().Length;
            while (length > 1)
                switch (length)
                {
                    case 4: //thousands 
                        result += $"{words[0][int.Parse(number.ToString()[0].ToString())]} thousand ";
                        number %= 1000;
                        length = number.ToString().Length;
                        if (length == 1 && number > 0) result += "and ";
                        break;
                    case 3: //hundreds
                        result += $"{words[0][int.Parse(number.ToString()[0].ToString())]} hundred ";
                        number %= 100;
                        length = number.ToString().Length;
                        if (number > 0) result += "and ";
                        break;
                    case 2: //tens
                        result += $"{words[1][int.Parse(number.ToString()[0].ToString())]} ";
                        number %= 10;
                        length = number.ToString().Length;
                        break;
                    default:
                        return "Not supported";
                }
            if (number > 0) result += $"{words[0][int.Parse(number.ToString()[0].ToString())]}";
            return result.TrimEnd();
        }

        /*
         * * Fibonacci (40)
         * * Iterative Approach
         *  Completed in 00:00:00.0010031
         *  Recursive Approach
         *  Completed in 00:00:05.9519619
         *  Recursive Approach with Memoization
         *  Completed in 00:00:00.0839923
         */

        //Get Fibonnacci Number Iterative
        public static long FibonacciIterative(long number)
        {
            var numbers = new long[number + 1];
            var n = number - 1;
            numbers[0] = 0;
            numbers[1] = 1;
            for (var i = 2; i <= n + 1; i++) numbers[i] = numbers[i - 2] + numbers[i - 1];
            return numbers[number];
        }

        //Get Fibonacci Number Recursively with Dynamic programming

        public static long FibonacciRecursiveMemo(long number, long[] memo)
        {
            if (number <= 1) return number;
            if (!memo.Contains(number))
                memo[number] = FibonacciRecursiveMemo(number - 1, memo) + FibonacciRecursiveMemo(number - 2, memo);
            return memo[number];
        }

        //Get Fibonacci Number Recursively
        public static long FibonacciRecursive(long number)
        {
            if (number == 0 || number == 1) return number;
            return FibonacciRecursive(number - 1) + FibonacciRecursive(number - 2);
        }

        public static long ReturnSum(int n, int[] array)
        {
            if (n < 1) return 0;
            long sum = 0;
            for (var i = 0; i < n; i++) if (IsMultiple(i, array)) sum += i;
            return sum;
        }

        private static bool IsMultiple(int n, IEnumerable<int> array)
        {
            return array.Any(t => n % t == 0);
        }

        #endregion
    }
}
