using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using CodeExercises.Internal;

namespace CodeExercises
{
    internal class Program
    {
        private static void Main()
        {
            var st = new Stack();
            st.Push(10);
            st.Push(5);

            var current = st.Pop();
            Console.WriteLine(current);
            Console.WriteLine(st.IsEmpty());

            current = st.Peek();
            Console.WriteLine(current);
            Console.WriteLine(st.IsEmpty());

            current = st.Pop();
            Console.WriteLine(current);
            Console.WriteLine(st.IsEmpty());

            Console.ReadKey();
        }


        #region FLEXPORT

        /*
         * mapping ={
         *  '1', ['a','b']
         *  '2', ['c','d']
         * }
            getWords('11', mapping) => ['ac', 'ad', 'bc', 'bd']
            getWords('1', mapping) => ['a', 'b']
            getWords('12', mapping) => ['ac', 'ad', 'bc', 'bd']
            getWords('122', mapping) => ['acc', 'adc', 'bcc', 'bdc', 'acd', 'add', 'bcd', 'bdd']
            getWords('', mapping) => ['']
        */

        //private static string[] GetWords(string number, Dictionary<string, string[]> mapping)
        //{
        //    var result = new List<string>();
        //    int[,] tst = new int[3,2];
        //    var map = new Dictionary<double, KeyValuePair<int, int>>();
        //    map.OrderBy(x=> x.Key).Take()

        //    return result.ToArray();
        //}

        #endregion

        #region CARVANA

        /*
         * Carvana Online Exam (HackerRank)
         */
        private static string Rev(string a)
        {
            //var str = string.Empty;
            //for (var i = a.Length - 1; i >= 0; i--)
            //{
            //    str += a[i].ToString();
            //}
            //return str;
            //1. Array
            //var str = new char[a.Length];
            //for (var i = 0; i < a.Length; i++)
            //{
            //    str[i] = a[a.Length - 1- i];
            //}
            //return string.Join(string.Empty, str);

            //var stk = new Stack<string>();
            //foreach (char t in a)
            //{
            //    stk.Push(t.ToString());
            //}
            //var result = string.Empty;
            //while (stk.Count > 0)
            //{
            //    result += stk.Pop();
            //}
            //return result;

            var index = a.Length - 1;
            var array = a.ToCharArray();
            for (var i = 0; i < a.Length; i++)
            {
                //swap in same string
                //implementing a temporary variable
                //we can use a different method say, 
                // private method just for swap, but this
                // is simply enough to demonstrate it.
                if (index <= i)
                    break;
                var temp = array[i];
                array[i] = array[index];
                array[index] = temp;
                index--;
            }

            return string.Join(string.Empty, array);
        }

        #endregion

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

        #region MICROSOFT

        /*
            // in: balanced binary search tree containing positive ints
            // out: find the value which is closest to max(node value)/2
        */

        public class TreeNodeT
        {
            public int val { get; set; }
            public TreeNodeT left { get; set; }
            public TreeNodeT right { get; set; }
        }

        public int GetValue(TreeNodeT node)
        {
            //base case
            if (node == null) return -1;
            //get max value/2 O(log n)
            var maxValue = GetMaxValue(node) / 2;
            //bs for result  (log n)
            var result = BinarySearch(node, maxValue, out var closest);
            if (result != null) return result.val;
            return Math.Min(Math.Abs(result.val - maxValue), Math.Abs(closest - maxValue));
        }

        private int GetMaxValue(TreeNodeT node)
        {
            var value = -1;
            while (node != null)
            {
                value = node.val;
                node = node.right;
            }
            return value;
        }

        public TreeNodeT BinarySearch(TreeNodeT node, int maxValue, out int closest)
        {
            closest = int.MaxValue;
            //try find value
            //when compares the value evaluate if is closest to maxValue
            var current = node;
            while (node != null)
            {
                //comparison
                if (node.val == maxValue) return node;
                if (Math.Abs(node.val - maxValue) < closest) closest = node.val;
                current = node;
                //left or right
                node = maxValue >= node.val ? node.right : node.left;
            }
            return current;
        }


        /*
            * Second smallest number in a Binary Search Tree
            */

        /*
            * Design a Max throughput per hour of N in cloud transactions
            */

        /*
        * Convert number to Roman numbers
        */

        /*
            * FuzzBuzz. In an array print Fizz if number is multiple of 3, 
            * Buzz if is multiple of 5
            * and FuzzBuzz if is multiple of both
            */

        /*
            * Max number of Permutations in an string
            */

        /*
        * in a Stream of numbers
        * Max Number
        * Min Number
        * Frequency
        * Average
        * Median
        */

        /*
            * Convert Integer to Binary
            */

        #endregion

        #region CODE WARS && OTHER

        /*
         * There are incoming numbers from a stream, print the largest 1000 numbers
         * 3,6,7,64,3,34,5,6,75,42,4
         * print 5 largest
         */

        public static class Numbers
        {
            private static readonly Mutex M;

            static Numbers()
            {
                List = new HashSet<int>();
                M = new Mutex();
                Min = int.MaxValue;
            }

            public static HashSet<int> List { get; }
            private static int Min { get; set; }

            public static void IncomingNumbers(int n)
            {
                M.WaitOne();
                if (List.Add(n))
                    if (Min > n)
                        Min = n;
                if (List.Count > 500)
                {
                    List.Remove(Min);
                    Min = List.Min();
                }
                M.ReleaseMutex();
            }
        }


        /*
        * Print a matrix in a spiral way
        */

        private static void SpiralMatrix(int[,] matrix)
        {
            var lu = 0;
            var ru = matrix.GetLength(1) - 1;
            var rb = matrix.GetLength(0) - 1;
            var lb = 0;
            var direction = Direction.Right;
            while (ru >= lb && rb >= lu)
                switch (direction)
                {
                    case Direction.Right:
                        for (var i = lb; i <= ru; i++) Console.Write($"{matrix[lb, i]} ");
                        lu++;
                        direction = Direction.Down;
                        break;
                    case Direction.Down:
                        for (var i = lu; i <= rb; i++) Console.Write($"{matrix[i, ru]} ");
                        ru--;
                        direction = Direction.Left;
                        break;
                    case Direction.Left:
                        for (var i = ru; i >= lb; i--) Console.Write($"{matrix[rb, i]} ");
                        rb--;
                        direction = Direction.Up;

                        break;
                    case Direction.Up:
                        for (var i = rb; i >= lu; i--) Console.Write($"{matrix[i, lb]} ");
                        lb++;
                        direction = Direction.Right;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
        }

        private enum Direction
        {
            Right,
            Down,
            Left,
            Up
        }

        /*
         * Given an array and a sum, determine if any of the items add up to sum. 
         * Do a linear space solution and constant time solution.
         */
        public static bool ArraySum(int[] list, int sum)
        {
            var items = new Dictionary<int, int>();
            for (var c = 0; c < list.Length; c++)
                if (items.ContainsKey(list[c]))
                {
                    var rest = sum - list[c];
                    if (items.ContainsKey(rest)) return true;
                }
                else
                {
                    var rest = sum - list[c];
                    if (items.ContainsKey(rest)) return true;
                    items.Add(list[c], 1);
                }
            return false;
        }

        /*
         * Check if an item exists in sortedList
         * (Binary Search)
         */
        public bool ExistInSortedList(int[] list, int a)
        {
            var low = 0;
            var high = list.Length - 1;
            while (low <= high)
            {
                var mid = (high - low) / 2 + low;
                if (list[mid] == a) return true;
                if (a < list[mid]) high = mid - 1;
                else low = mid + 1;
            }
            return false;
        }

        /*
         * Print numbers between 2 parameters
         */
        public static void PrintNumbersInBetween(int a, int b)
        {
            for (var c = a + 1; c < b; c++) Console.WriteLine($"number:{c}");
        }

        /*
         * Traverse In-Order for a Binary Tree
         */
        public static void TraverseInOrder(BinarySearchTreeNode node)
        {
            if (node == null) return;
            TraverseInOrder(node.Left);
            Console.WriteLine(node.Value);
            TraverseInOrder(node.Right);
        }

        /*
         * Find distance between two given keys of a Binary Tree 
         */
        public static int GetDistance(BinarySearchTreeNode a, BinarySearchTreeNode b, BinarySearchTreeNode node)
        {
            if (node == null) return 0;
            var lca = GetLowestCommonAncestor(a, b, node);
            var da = GetLevelOfNode(lca, a, 0);
            var db = GetLevelOfNode(lca, a, 0);
            return da + db;
        }

        /*
         * Get Level of Node
         */
        public static int GetLevelOfNode(BinarySearchTreeNode root, BinarySearchTreeNode node, int level)
        {
            if (root == null) return -1;
            if (root.Value == node.Value) return level;
            var left = GetLevelOfNode(root.Left, node, level + 1);
            return left != -1 ? left : GetLevelOfNode(root.Right, node, level + 1);
        }

        /*
         * Lowest Common Ancestor in a Tree
         */
        public static BinarySearchTreeNode GetLowestCommonAncestor(BinarySearchTreeNode a, BinarySearchTreeNode b,
            BinarySearchTreeNode root)
        {
            if (root == null) return null;
            if (a == root || b == root) return root;
            var left = GetLowestCommonAncestor(a, b, root.Left);
            var right = GetLowestCommonAncestor(a, b, root.Right);
            if (left != null && right != null) return root;
            if (left == null && right == null) return null;
            return left ?? right;
        }

        /*
         * Binary Search Tree. Find longest path within it.
         * Find a path between any two leaf nodes where path
         * is the longest.
         */
        public static int GetLongestPath(BinarySearchTreeNode node)
        {
            if (node == null) return 0;
            var ltree = TreeHeight(node.Left);
            var rtree = TreeHeight(node.Right);
            return ltree + rtree + 1;
        }

        public static int TreeHeight(BinarySearchTreeNode node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(TreeHeight(node.Left), TreeHeight(node.Right));
        }

        /*  
            Common element in 3 arrays, O (log N) 
            var a = new int[] { 1,2,3,4,5};
            var b = new int[] { 4,5, 6,7,8,9};
            var c = new int[] { 5, 12,14,15};
        */
        public static bool CommonElement(int[] a, int[] b, int[] c)
        {
            foreach (var num in a)
            {
                if (!BinarySearch(b, num)) continue;
                if (BinarySearch(c, num)) return true;
            }
            return false;
        }

        private static bool BinarySearch(int[] a, int n)
        {
            var low = 0;
            var high = a.Length - 1;
            while (low <= high)
            {
                var mid = (high - low) / 2 + low;
                if (a[mid] == n) return true;
                if (n < a[mid]) high = mid - 1;
                else low = mid + 1;
            }
            return false;
        }

        public static int СenturyFromYear(int year)
        {
            return Convert.ToInt32(year % 100 > 0
                // ReSharper disable once PossibleLossOfFraction
                ? Math.Floor((decimal)(year / 100)) + 1
                // ReSharper disable once PossibleLossOfFraction
                : Math.Floor((decimal)(year / 100)));
        }

        public static int DuplicateCount(string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;
            str = str.ToLower();
            var strArray = str.ToCharArray();
            return strArray.GroupBy(x => x).Count(y => y.Count() > 1);
        }

        public static string Order(string words)
        {
            if (string.IsNullOrWhiteSpace(words)) return string.Empty;
            var wordArray = words.Split(' ');
            var result = new string[wordArray.Length];
            foreach (var word in wordArray)
                foreach (var letter in word.ToCharArray())
                {
                    if (!char.IsNumber(letter)) continue;
                    result[int.Parse(letter.ToString()) - 1] = word;
                    break;
                }
            return string.Join(" ", result);
        }

        public static int GeneralizedGcd(int num, int[] arr)
        {
            var hcf = 2;
            while (true)
            {
                if (arr.Any(t => t % hcf != 0))
                    return hcf - 1;
                hcf++;
            }
        }

        public static int[] CellCompete(int[] states, int days)
        {
            if (days <= 0) return states;
            var newStates = states.Clone() as int[];
            var currentStates = states.Clone() as int[];
            for (var i = 1; i <= days; i++)
            {
                newStates = currentStates.Clone() as int[];
                for (var r = 0; r < states.Length; r++)
                {
                    if (r == 0)
                        if (currentStates[r + 1] == 0)
                            newStates[r] = 0;
                        else
                            newStates[r] = 1;
                    if (r > 0 && r < states.Length - 1)
                        if (currentStates[r - 1] == currentStates[r + 1])
                            newStates[r] = 0;
                        else
                            newStates[r] = 1;
                    if (r != states.Length - 1) continue;
                    if (currentStates[r - 1] == 0)
                        newStates[r] = 0;
                    else
                        newStates[r] = 1;
                }
                currentStates = newStates.Clone() as int[];
            }
            return newStates;
        }

        public static void WriteErrorstoList(Dictionary<string, int> errors)
        {
        }

        public static void PrintErrorsFromDAP()
        {
            var urlList = new Dictionary<string, int>();
            var linesFromFile = File.ReadAllLines(@"C:\Obsidian\DAP-Errors.txt");
            foreach (var line in linesFromFile)
                try
                {
                    var url = new Uri(line.Split(' ')[0]);
                    var urlPath = url.Scheme + "//" + url.DnsSafeHost + url.AbsolutePath;
                    urlPath = Regex.Replace(urlPath,
                        @"[({]?[a-zA-Z0-9]{8}[-]?([a-zA-Z0-9]{4}[-]?){3}[a-zA-Z0-9]{12}[})]?", string.Empty,
                        RegexOptions.IgnoreCase);
                    urlPath = Regex.Replace(urlPath, @"(\d+)$", string.Empty, RegexOptions.IgnoreCase);
                    if (urlList.ContainsKey(urlPath))
                        urlList[urlPath]++;
                    else
                        urlList.Add(urlPath, 1);
                }
                catch (Exception)
                {
                }
            var lines = new List<string> { "URL, Error Count, Percentage" };
            var total = urlList.Sum(x => x.Value);
            lines.AddRange(urlList.OrderByDescending(x => x.Value)
                .Select(error => $"{error.Key},{error.Value},{Convert.ToDecimal(error.Value) * 100 / total}"));
            lines.Add($"Total,{total},100");
            File.WriteAllLines(@"C:\\Obsidian\DAP-Error-Count.csv", lines);
        }

        public static int Divide(int dividend, int divisor)
        {
            var isNegativeDivisor = divisor < 0;
            var isNegativeDividend = dividend < 0;
            if (isNegativeDivisor) divisor = divisor * -1;
            if (isNegativeDividend) dividend = dividend * -1;
            var result = dividend;
            var count = 0;
            while (result >= divisor)
            {
                result = result - divisor;
                count++;
                if (result > divisor) continue;
                if (isNegativeDivisor && isNegativeDividend) return count;
                return isNegativeDivisor || isNegativeDividend ? count * -1 : count;
            }
            return 0;
        }

        public static Tuple<int, int> FindTwoSum(IList<int> list, int sum)
        {
            foreach (var i in list)
            {
                var r = sum - i;
                if (list.Contains(r)) return Tuple.Create(list.IndexOf(i), list.IndexOf(r));
            }
            return null;
        }

        public static List<string> RetrieveMostFrequentlyUsedWords(string literatureText,
            List<string> wordsToExclude)
        {
            //Validate Input List
            if (string.IsNullOrWhiteSpace(literatureText)) return new List<string>();

            //Convert to space any special character
            var inputLiteratureText = literatureText.Where(t => !char.IsLetter(t))
                .Aggregate(literatureText, (current, t) => current.Replace(t.ToString(), " "));

            //Exclude common word from List
            var result = inputLiteratureText.ToLower();
            if (wordsToExclude != null && wordsToExclude.Any())
                result = wordsToExclude.Select(x => x.ToLower())
                    .Aggregate(result, (current, word) => current.Replace($" {word} ", " ")
                        .Replace($"{word} ", " ")
                        .Replace($" {word}", " "));
            var words = result.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var wordsGroup = words.ToLookup(x => x);
            var maxFrequency = wordsGroup.Max(x => x.Count());
            var resultWords = wordsGroup.Where(x => x.Count() == maxFrequency).ToList();
            return resultWords.Select(x => x.Key).ToList();
        }

        public static int[] SortArray(int[] array)
        {
            var oIndex = 0;
            var ordered = array.Where(x => x % 2 > 0).OrderBy(x => x).ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] % 2 == 0) continue;
                array[i] = ordered[oIndex++];
                oIndex++;
            }
            return array;
        }

        public static int GetUnique(IEnumerable<int> numbers)
        {
            return numbers.GroupBy(x => x)
                .Select(n => new { number = n.Key, value = n.Count() })
                .OrderBy(n => n.value)
                .First().number;
        }

        public static string TitleCase(string title, string minorWords = "")
        {
            if (string.IsNullOrEmpty(title)) return string.Empty;
            var response = title.ToLower().Split(' ');
            for (var i = 0; i < response.Length; i++)
            {
                if (!string.IsNullOrEmpty(minorWords) &&
                    minorWords.ToLower().Split(' ').Contains(response[i])) continue;
                response[i] = response[i][0].ToString().ToUpper() + response[i].Substring(1, response[i].Length - 1);
            }
            var result = string.Join(" ", response);
            return result[0].ToString().ToUpper() + result.Substring(1, result.Length - 1);
        }

        public static int SquareDigits(int n)
        {
            var numbers = n.ToString().ToArray();
            var result = numbers.Aggregate(string.Empty,
                (current, digit) => current + (int.Parse(digit.ToString()) * int.Parse(digit.ToString())).ToString());
            return int.Parse(result);
        }

        public static bool Xo(string input)
        {
            var x = input.Count(w => w.ToString().ToLower() == "x");
            var o = input.Count(w => w.ToString().ToLower() == "o");
            return x == o;
        }

        public static int FindShort(string s)
        {
            return s.Split(' ').OrderBy(x => x.Length).First().Length;
        }

        public static string ToJadenCase(string phrase)
        {
            var words = phrase.Split(' ');
            for (var i = 0; i < words.Length; i++)
                words[i] = words[i][0].ToString().ToUpper() + words[i].Substring(1, words[i].Length - 1);
            return string.Join(" ", words);
        }

        public static string SpinWords(string sentence)
        {
            var words = sentence.Split(' ');
            var response = new List<string>();
            foreach (var word in words)
                if (word.Length > 4)
                {
                    var wArray = word.ToArray();
                    Array.Reverse(wArray);
                    response.Add(string.Join(string.Empty, wArray));
                }
                else
                {
                    response.Add(word);
                }
            return string.Join(" ", response);
        }

        public static int DigitalRoot(long n)
        {
            return DigitalRootRecursive(n);
        }

        private static int DigitalRootRecursive(long n)
        {
            if (n < 10) return int.Parse(n.ToString());
            var digits = n.ToString().ToArray().Select(x => int.Parse(x.ToString()));
            return DigitalRootRecursive(digits.Sum());
        }

        public static int DigitalRootLoop(long n)
        {
            var number = n;
            while (true)
            {
                if (number < 10) return int.Parse(number.ToString());
                var digits = n.ToString().ToArray().Select(x => int.Parse(x.ToString()));
                number = digits.Sum();
            }
        }

        public static int Find(int[] integers)
        {
            var isEven = integers.Count(x => x % 2 == 0) > 1;
            return isEven ? integers.SingleOrDefault(x => x % 2 > 0) : integers.SingleOrDefault(x => x % 2 == 0);
        }

        public static int FindEvenIndex(int[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                var left = GetSum(i, arr, true);
                var right = GetSum(i, arr);
                if (left == right) return i;
            }
            return -1;
        }

        public static int GetSum(int index, int[] arr, bool isLeft = false)
        {
            var sum = 0;
            if (isLeft) for (var i = 0; i < index; i++) sum += arr[i];
            else for (var i = index + 1; i < arr.Length; i++) sum += arr[i];
            return sum;
        }

        public static string HighAndLow(string numbers)
        {
            var digits = numbers.Split(' ');
            var max = digits.Max(x => int.Parse(x.ToString()));
            var min = digits.Min(x => int.Parse(x.ToString()));
            return $"{max} {min}";
        }

        public static int Persistence(long n)
        {
            return PersistenceLoop(n, 0);
        }

        public static int PersistenceLoop(long n, int counter)
        {
            while (true)
            {
                var digits = n.ToString().ToCharArray();
                if (digits.Length == 1) return counter;
                var number = digits.Aggregate(1, (current, digit) => current * int.Parse(digit.ToString()));
                counter += 1;
                n = number;
            }
        }

        public static int Persistence(long n, int counter)
        {
            var digits = n.ToString().ToCharArray();
            if (digits.Length == 1) return counter;
            var number = digits.Aggregate(1, (current, digit) => current * int.Parse(digit.ToString()));
            counter += 1;
            return Persistence(number, counter);
        }

        public static int FindOddCount(int[] seq)
        {
            foreach (var number in seq)
            {
                var response = seq.Count(x => x == number);
                if (response % 2 == 1) return number;
            }
            return -1;
        }

        public static string GetMiddle(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;
            if (s.Length == 1) return s;
            return s.Length % 2 > 0
                ? s[s.Length / 2].ToString()
                : s[s.Length / 2 - 1] + s[s.Length / 2].ToString();
        }

        public static string Decode(string morseCode)
        {
            if (string.IsNullOrWhiteSpace(morseCode)) return string.Empty;
            if (morseCode.Count(x => x != 46 || x != 45) > 0) return string.Empty;
            var morse = new Dictionary<string, char>
            {
                {".-", 'A'},
                {"-...", 'B'},
                {"-.-.", 'C'},
                {"-..", 'D'},
                {".", 'E'},
                {"..-.", 'F'},
                {"--.", 'G'},
                {"....", 'H'},
                {"..", 'I'},
                {".---", 'J'},
                {"-.-", 'K'},
                {".-..", 'L'},
                {"--", 'M'},
                {"-.", 'N'},
                {"---", 'O'},
                {".--.", 'P'},
                {"--.-", 'Q'},
                {".-.", 'R'},
                {"...", 'S'},
                {"-", 'T'},
                {"..-", 'U'},
                {"...-", 'V'},
                {".--", 'W'},
                {"-..-", 'X'},
                {"-.--", 'Y'},
                {"--..", 'Z'},
                {".----", '1'},
                {"..---", '2'},
                {"...--", '3'},
                {"....-", '4'},
                {".....", '5'},
                {"-....", '6'},
                {"--...", '7'},
                {"---..", '8'},
                {"----.", '9'},
                {"-----", '0'},
                {".-.-.-", '.'},
                {"--..--", ','}
            };
            return string.Join(" ",
                morseCode.Split(new[] { "   " }, StringSplitOptions.None).Select(word => string.Join(string.Empty,
                      word.Split(' ').Select(character => morse[character].ToString()))).ToList());
        }

        public static string Accum(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return s;
            var response = string.Empty;
            for (var i = 0; i < s.Length; i++)
            {
                if (string.IsNullOrEmpty(s[i].ToString())) continue;
                response += s[i].ToString().ToUpper();
                for (var r = 0; r < i; r++)
                    response += s[i].ToString().ToLower();
                if (i < s.Length - 1) response += "-";
            }
            return response;
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            return GetList(GetNumber(l1) + GetNumber(l2));
        }

        public ListNode GetList(int number)
        {
            var reverseNumber = number.ToString().Reverse().ToList();
            var node = new ListNode(reverseNumber[0]);
            for (var i = 1; i < reverseNumber.Count; i++)
            {
                node.Next = new ListNode(reverseNumber[i]);
                node = node.Next;
            }
            return node;
        }

        public int GetNumber(ListNode l)
        {
            var number = new List<string>();
            while (l != null)
            {
                number.Add(l.Val.ToString());
                l = l.Next;
            }
            return int.Parse(number.ToArray().Reverse().ToString());
        }

        public static int CamelCase(string s)
        {
            var test = s.Count(x => x > 64 && x < 91) + 1;
            return test;
        }

        public static long FindNextSquare(long num)
        {
            //check num first
            if (!(Math.Sqrt(num) % 1 == 0)) return -1;
            while (true)
                if (Math.Sqrt(num++) % 1 == 0) return num;
        }

        public static int[] CountPositivesSumNegatives(int[] input)
        {
            if (input == null || input.Length == 0) return new int[] { };
            return new[] { input.Where(x => x >= 0).Count(), input.Where(x => x < 0).Sum() };
        }

        public static int SumDiagonals(int[,] matrix)
        {
            if (matrix is null) return 0;
            if (matrix.GetLength(0) != matrix.GetLength(1)) return 0;
            if (matrix.Length <= 2) return 0;
            var ltr = 0;
            var rtl = 0;
            var rigthIndex = matrix.GetLength(0) - 1;
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                ltr += matrix[i, i];
                rtl += matrix[i, rigthIndex--];
            }
            return Math.Abs(ltr - rtl);
        }

        public static string Reverse(string str)
        {
            return string.Join(" ", str.Split(' ').Select(word => new string(word.Reverse().ToArray())).ToList());
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

        // ReSharper disable once UnusedMember.Local
        private static string ReplaceString(string input, string search, string replace)
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

        private static string Replace(int startIndex, int endIndex, string input, string replace)
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

        public static int CountDiv(int a, int b, int k)
        {
            var counter = 0;
            for (var i = a; i <= b; i++)
                if (i % k == 0) counter++;
            return counter;
        }

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

        public static int MaxDepth(TreeNode node)
        {
            if (node == null) return 0;
            var leftDepth = MaxDepth(node.Left);
            var rightDepth = MaxDepth(node.Right);
            return leftDepth > rightDepth ? leftDepth + 1 : rightDepth + 1;
        }

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

        public static int SingleNumber(int[] nums)
        {
            var num = nums.GroupBy(x => x).SingleOrDefault(y => y.Count() == 1).Key;
            return num;
        }

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

        /*
         * Implement an iterator over a binary search tree (BST). Your iterator will be initialized with the root node of a BST.
         * Calling next() will return the next smallest number in the BST. 
         * Note: next() and hasNext() should run in average O(1) time and uses O(h) memory, where h is the height of the tree.
         * 
         * Your BSTIterator will be called like this:
         * BSTIterator i = new BSTIterator(root);
         * while (i.HasNext()) v[f()] = i.Next();
         */
        public class BSTIterator
        {
            private readonly Queue<TreeNode> q;

            public BSTIterator(TreeNode root)
            {
                q = new Queue<TreeNode>();
                Traverse(root);
            }

            private void Traverse(TreeNode node)
            {
                if (node == null) return;
                Traverse(node.left);
                q.Enqueue(node);
                Traverse(node.right);
            }

            /** @return whether we have a next smallest number */
            public bool HasNext()
            {
                return q.Count > 0;
            }

            /** @return the next smallest number */
            public int Next()
            {
                return q.Dequeue().val;
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

        /*
        * LRU Cache
        * Implementing with Dictionary and Queue
        */
        /*  
         *  var cache = new LeastRecentUsedItems<int, string>(3);
            cache.Add(1, "uno");
            cache.Add(2, "dos");
            cache.Add(3, "tres");
            cache.Add(4, "cuatro");
            cache.Add(5, "cinco");
            var list = cache.PrintValues();
            foreach (var c in list) Console.WriteLine(c);
            cache.Add(6, "seis");
            cache.Add(7, "siete");
            Console.WriteLine();
            list = cache.PrintValues();
            foreach (var c in list) Console.WriteLine(c);
            cache.Add(8, "ocho");
            cache.Add(9, "nueve");
            Console.WriteLine();
            list = cache.PrintValues();
            foreach (var c in list) Console.WriteLine(c);
        */
        public class LeastRecentUsedItems<TKey, TValue>
        {
            private readonly Dictionary<TKey, Item> _items;
            private readonly int _maxSize;
            private readonly Queue<Item> _queue;

            public LeastRecentUsedItems(int maxSize = 50)
            {
                if (maxSize <= 0) throw new ArgumentException();
                _queue = new Queue<Item>();
                _items = new Dictionary<TKey, Item>();
                _maxSize = maxSize;
            }

            public void Add(TKey key, TValue value)
            {
                if (_items.ContainsKey(key))
                {
                    _items.Remove(key);
                }
                else if (_items.Count == _maxSize)
                {
                    var item = _queue.Dequeue();
                    _items.Remove(item.Key);
                }
                _items.Add(key, new Item(key, value));
                _queue.Enqueue(new Item(key, value));
            }

            public bool TryGetValue(TKey key, out TValue value)
            {
                value = default(TValue);
                if (!_items.TryGetValue(key, out var item)) return false;
                value = item.Value;
                return true;
            }

            public List<TValue> PrintValues()
            {
                return _items.Select(item => item.Value.Value).ToList();
            }

            private class Item
            {
                public Item(TKey key, TValue value)
                {
                    Key = key;
                    Value = value;
                }

                public TKey Key { get; }
                public TValue Value { get; }
            }
        }

        /*
         * LRU Cache
         * Implementing with Dictionary and Double Linked List
         */
        public class LeastRecentUsedCache
        {
            private readonly Dictionary<int, Node> _itemsDictionary;
            private readonly int _maxCacheSize;
            private Node _head;
            private Node _tail;

            public LeastRecentUsedCache(int maxCacheSize = 50)
            {
                if (maxCacheSize < 1) throw new ArgumentException();
                _maxCacheSize = maxCacheSize;
                _itemsDictionary = new Dictionary<int, Node>();
                _head = null;
            }

            public void Set(int key, string value)
            {
                if (!_itemsDictionary.TryGetValue(key, out var item))
                {
                    item = new Node { Key = key, Value = value };
                    if (_itemsDictionary.Count == _maxCacheSize)
                    {
                        _itemsDictionary.Remove(_tail.Key);
                        _tail = _tail.Previous;
                        if (_tail != null) _tail.Next = null;
                    }
                    _itemsDictionary.Add(key, item);
                }
                item.Value = value;
                MoveToHead(item);
                if (_tail == null) _tail = _head;
            }

            public bool TryGetValue(int key, out string value)
            {
                value = default(string);
                if (!_itemsDictionary.TryGetValue(key, out var item)) return false;
                MoveToHead(item);
                value = item.Value;
                return true;
            }

            private void MoveToHead(Node item)
            {
                if (item == _head || item == null) return;
                var next = item.Next;
                var previous = item.Previous;
                if (next != null) next.Previous = item.Previous;
                if (previous != null) previous.Next = item.Next;
                item.Previous = null;
                item.Next = _head;
                if (_head != null) _head.Previous = item;
                _head = item;
                if (_tail == item) _tail = previous;
            }

            private class Node
            {
                public Node Next { get; set; }
                public Node Previous { get; set; }
                public int Key { get; set; }
                public string Value { get; set; }
            }
        }

        /*
         * Traversal in Level order for N-Ary tree
         */
        /*
            var tree = new NTree();
            Console.Write("Add Node (y/n): ");
            var ck = Console.ReadKey();
            while (ck.Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                Console.Write("Parent: ");
                var parent = Console.ReadLine();
                Console.Write("Node: ");
                var value = Console.ReadLine();

                tree.Add(parent, value);
                Console.WriteLine($"Node added to {parent}");
                Console.WriteLine();
                Console.Write("Add Node (y/n): ");
                ck = Console.ReadKey();
            }
            Console.WriteLine();
            tree.LevelTraversal();
        */
        public class NTree
        {
            public NNode Root { get; set; }
            public int Count { get; set; }

            public void Add(string parent, string key)
            {
                Count++;
                if (Root == null)
                {
                    Root = new NNode(key, null) { Level = 1 };
                }
                else
                {
                    var node = Find(parent);
                    if (node == null) Root.Children.Add(new NNode(key, Root) { Level = Root.Level + 1 });
                    else Add(node, key);
                }
            }

            private void Add(NNode parent, string value)
            {
                parent?.Children.Add(new NNode(value, parent) { Level = parent.Level + 1 });
            }

            public NNode Find(string key)
            {
                return string.IsNullOrWhiteSpace(key) ? null : Find(Root, key);
            }

            /* BFS Approach */
            private NNode Find(NNode node, string key)
            {
                if (node == null) return null;
                var q = new Queue<NNode>();
                q.Enqueue(node);
                while (q.Any())
                {
                    var current = q.Dequeue();
                    if (current.Value == key) return current;
                    foreach (var currentChild in current.Children) q.Enqueue(currentChild);
                }
                return null;
            }

            /* BFS Approach */
            public void LevelTraversal()
            {
                if (Root == null) return;
                var q = new Queue<NNode>();
                q.Enqueue(Root);
                while (q.Any())
                {
                    var current = q.Dequeue();
                    Console.WriteLine($"Level: {current.Level}: Name: {current.Value}");
                    foreach (var c in current.Children) q.Enqueue(c);
                }
            }
        }

        public class NNode
        {
            public NNode(string value, NNode parent)
            {
                Value = value;
                Children = new List<NNode>();
                Parent = parent;
            }

            public string Value { get; set; }
            public NNode Parent { get; set; }
            public List<NNode> Children { get; set; }
            public int Level { get; set; }
        }

        public class TreeNode
        {
            private static bool _isPerfect = true;
            public TreeNode Left;
            public TreeNode Right;
            public int Value;

            public static bool IsPerfect(TreeNode root)
            {
                var depth = GetTreeDepth(root);
                Traverse(root, 1, depth);
                return _isPerfect;
            }

            public static void Traverse(TreeNode node, int nodeDepth, int treeDepth)
            {
                if (node == null || !_isPerfect) return;
                if (IsLeaf(node))
                {
                    _isPerfect = nodeDepth == treeDepth;
                }
                else
                {
                    if (node.Left != null && node.Right != null)
                    {
                        Traverse(node.Left, nodeDepth + 1, treeDepth);
                        Traverse(node.Right, nodeDepth + 1, treeDepth);
                    }
                    else
                    {
                        _isPerfect = false;
                    }
                }
            }

            public static int GetTreeDepth(TreeNode root)
            {
                var depth = 0;
                var current = root;
                while (current != null)
                {
                    depth++;
                    current = current.Left;
                }
                return depth;
            }

            public static bool IsLeaf(TreeNode node)
            {
                return node.Left == null && node.Right == null;
            }

            public static TreeNode Leaf()
            {
                return new TreeNode();
            }

            public static TreeNode Join(TreeNode left, TreeNode right)
            {
                return new TreeNode().WithChildren(left, right);
            }

            public TreeNode WithLeft(TreeNode left)
            {
                Left = left;
                return this;
            }

            public TreeNode WithRight(TreeNode right)
            {
                Right = right;
                return this;
            }

            public TreeNode WithChildren(TreeNode left, TreeNode right)
            {
                Left = left;
                Right = right;
                return this;
            }

            public TreeNode WithLeftLeaf()
            {
                return WithLeft(Leaf());
            }

            public TreeNode WithRightLeaf()
            {
                return WithRight(Leaf());
            }

            public TreeNode WithLeaves()
            {
                return WithChildren(Leaf(), Leaf());
            }
        }

        public class PrivateNode
        {
            public PrivateNode Left;
            public PrivateNode Right;
            public int Value;

            public PrivateNode(int value, PrivateNode left = null, PrivateNode right = null)
            {
                Value = value;
                Left = left;
                Right = right;
            }
        }

        #endregion

        #region HACKERRANK

        private static int[] RotateLeft(int[] a, int d)
        {
            var tempArray = new int[a.Length];
            for (var i = a.Length - 1; i >= 0; i--)
            {
                var index = i - d;
                if (index < 0) index = index + a.Length;
                tempArray[index] = a[i];
            }
            a = tempArray;

            var stack = new Stack<string>();
            stack.Push("wer");
            stack.Pop();
            return a;
        }


        //Timeouts
        private static int[] rotLeft(int[] a, int d)
        {
            while (d > 0)
            {
                var tempArray = new int[a.Length];
                for (var i = a.Length - 1; i >= 0; i--)
                {
                    var index = i - 1;
                    if (index == -1) index = a.Length - 1;
                    tempArray[index] = a[i];
                }
                a = tempArray;
                d--;
            }
            return a;
        }


        private static int MiniSwaps(int[] arr)
        {
            if (!arr.Any()) return 0;
            var swaps = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                if (arr[i] == i + 1) continue;
                while (arr[i] != i + 1)
                {
                    Swap(ref arr, i, arr[i] - 1);
                    swaps++;
                }
            }
            return swaps;
        }


        //Timeout
        private static int MinimumSwaps(int[] arr)
        {
            if (IsSorted(arr, 0)) return 0;
            var swaps = 0;
            var currentIndex = 0;
            for (var i = currentIndex; i < arr.Length; i++)
            {
                if (IsSorted(arr, currentIndex)) return swaps;
                var r = FindMin(arr, currentIndex);
                if (r > 0 && r != currentIndex)
                {
                    Swap(ref arr, r, currentIndex);
                    swaps++;
                }
                currentIndex++;
            }
            return swaps;
        }

        public static int FindMin(int[] array, int currentIndex)
        {
            var min = currentIndex;
            for (var i = currentIndex; i < array.Length; i++)
                if (array[i] < array[min]) min = i;
            return min;
        }

        private static void Swap(ref int[] array, int source, int target)
        {
            var temp = array[source];
            array[source] = array[target];
            array[target] = temp;
        }

        private static bool IsSorted(IReadOnlyList<int> array, int fromIndex)
        {
            if (!array.Any()) return true;
            var previous = array[fromIndex];
            for (var i = fromIndex + 1; i < array.Count; i++)
            {
                if (array[i] < previous) return false;
                previous = array[i];
            }
            return true;
        }


        //O(N) Solutionon
        private static long ArrayMan(int n, int[][] queries)
        {
            var numbers = new long[n + 1];
            foreach (var operation in queries)
            {
                var a = operation[0];
                var b = operation[0];
                long k = operation[0];

                numbers[a] += k;
                if (b + 1 <= n) numbers[b + 1] -= k;
            }
            long temp = 0;
            long maxValue = 0;
            for (var i = 1; i <= n; i++)
            {
                temp += numbers[i];
                if (temp > maxValue) maxValue = temp;
            }
            return maxValue;
        }

        //Times out with long operations
        //O(N*M)
        private static long ArrayManipulation(int n, int[][] queries)
        {
            if (queries == null || !queries.Any()) return 0;
            if (n <= 0) return 0;
            var result = new int[n];
            long maxNumber = 0;
            foreach (var operation in queries) //O(N)
            {
                if (operation == null || !operation.Any()) continue;
                var low = operation[0] - 1;
                var high = operation[1];
                var factor = operation[2];
                for (var r = low; r < high; r++) //O(M)
                {
                    result[r] += factor;
                    if (result[r] > maxNumber) maxNumber = result[r];
                }
            }
            return maxNumber;
        }

        private static int[] GradingStudents(int[] grades)
        {
            if (grades == null || !grades.Any()) return new int[0];
            var result = new int[grades.Length];
            for (var i = 0; i < grades.Length; i++)
                if (grades[i] < 38) result[i] = grades[i];
                else result[i] = grades[i] % 5 >= 3 ? grades[i] + (5 - grades[i] % 5) : grades[i];
            return result;
        }


        private static int BirthdayCakeCandles(int[] ar)
        {
            //Base Case
            if (ar == null || !ar.Any()) return 0;
            var maxNumber = 0;
            foreach (var candle in ar)
            {
                if (candle <= maxNumber) continue;
                maxNumber = candle;
            }
            var maxNumberValues = new Dictionary<int, int> { { maxNumber, 0 } };
            foreach (var candle in ar)
            {
                if (candle != maxNumber) continue;
                maxNumberValues[maxNumber]++;
            }
            return maxNumberValues[maxNumber];
        }


        private static long AVeryBigSum(long[] ar)
        {
            if (!ar.Any()) return 0;
            long result = 0;
            for (var i = 0; i < ar.Length; i++)
                result += ar[i];
            return result;
        }

        #endregion

        #region LEETCODE

        /*
         * You are given coins of different denominations and a total amount of money amount. 
         * Write a function to compute the fewest number of coins that you need to make up that amount. 
         * If that amount of money cannot be made up by any combination of the coins, return -1.

            Example 1:

            Input: coins = [1, 2, 5], amount = 11
            Output: 3 
            Explanation: 11 = 5 + 5 + 1
            Example 2:

            Input: coins = [2], amount = 3
            Output: -1
            Note:
            You may assume that you have an infinite number of each kind of coin.
        */

        public static int CoinChange(int[] coins, int amount)
        {
            return amount < 1 ? 0 : CoinChange(coins, amount, new int[amount]);
        }

        private static int CoinChange(IEnumerable<int> coins, int rem, IList<int> count)
        {
            if (rem < 0) return -1;
            if (rem == 0) return 0;
            if (count[rem - 1] != 0) return count[rem - 1];
            var min = int.MaxValue;
            var coinsArray = coins as int[] ?? coins.ToArray();
            foreach (var coin in coinsArray)
            {
                var res = CoinChange(coinsArray, rem - coin, count);
                if (res >= 0 && res < min)
                    min = 1 + res;
            }
            count[rem - 1] = min == int.MaxValue ? -1 : min;
            return count[rem - 1];
        }

        #endregion

        #region AMAZON

        public static List<List<int>> OptimalUtilization(int deviceCapacity,
            List<List<int>> foregroundAppList,
            List<List<int>> backgroundAppList)
        {

            var dict = new Dictionary<List<int>, double>();
            foreach (var fa in foregroundAppList)
            { //O(f*b) <--
                foreach (var ba in backgroundAppList)
                {
                    if (fa[1] + ba[1] > deviceCapacity) continue;
                    dict.Add(new List<int> { fa[0], ba[0] }, fa[1] + ba[1]);
                }
            }

            var result = new List<List<int>>();
            if (dict.Count == 0) return result;

            double lastValue = -1;
            foreach (var item in dict.OrderByDescending(x => x.Value))
            {
                if (lastValue > 0 && item.Value < lastValue) break;
                result.Add(item.Key);
                lastValue = item.Value;
            }

            return result;
        }


        public static List<List<int>> NearestXsteakHouses(int totalSteakhouses,
            int[,] allLocations,
            int numSteakhouses)
        {

            if (allLocations == null
                || allLocations.Length == 0
                || numSteakhouses > totalSteakhouses)
                return new List<List<int>>();

            var map = new Dictionary<KeyValuePair<int, int>, double>();
            for (var i = 0; i < totalSteakhouses; i++)
            { //O(N)
                var locationX = allLocations[i, 0];
                var locationY = allLocations[i, 1];
                var distance = Math.Sqrt((locationX * locationX)
                                         + (locationY * locationY));
                var kv = new KeyValuePair<int, int>(locationX, locationY);
                if (map.ContainsKey(kv)) continue;
                map.Add(kv, distance);
            }

            var closestSteakHouses = map.OrderBy(x => x.Value).Take(numSteakhouses); //O(N log N) <- Bottleneck
            return closestSteakHouses.Select(x => new List<int> { x.Key.Key, x.Key.Value }).ToList();
        }


        /*
         * Amazon AWS
         */

        /* Need to do math with really big integers. Say `LargeInt`.
         *  For now, we need to add two really large numbers.
         *  Say 193,707,721 + 76,187,287 = 269,895,008
         *  I want you to build this in a way that other people can use it. e.g. a library.
         */

        public static string Sum(string a, string b)
        {
            var result = string.Empty;
            var aLength = a.Length - 1;
            var bLength = b.Length - 1;
            var carry = 0;
            var maxIndex = Math.Max(aLength, bLength);

            for (var i = 0; i <= maxIndex; i++)
            {
                var left = aLength >= 0 ? int.Parse(a[aLength].ToString()) : 0;
                var right = bLength >= 0 ? int.Parse(b[bLength].ToString()) : 0;
                var sum = left + right + carry;

                carry = sum >= 10 ? 1 : 0;

                result += sum.ToString()[sum.ToString().Length - 1].ToString();

                bLength--;
                aLength--;
            }
            return string.Join(string.Empty, result.Reverse());
        }

        /*
        * Amazon 
        * 
        * Given an array of numbers in sorted order
        * count the pairs of numbers whose sum is less than X
        */

        public static int GetCountOfPairs(int[] numbers, int n)
        {
            var count = 0;
            var firstIndex = 0;

            var lastIndex = numbers.Length - 1;
            while (firstIndex != lastIndex)
                if (numbers[firstIndex] + numbers[lastIndex] < n)
                {
                    count += lastIndex - firstIndex;
                    firstIndex++;
                }
                else
                {
                    lastIndex--;
                }
            return count;
        }

        /*
         * AMAZON Luxembourg Question
         */

        /*
         var literatureText ="Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur? Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat avoids a pain that produces no resultant pleasure? quo voluptas nulla pariatur? Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, Lorem ipsum dolor sit amet.., comes from a line in section 1.10.32. There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc. It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).";
        var wordsToExclude = new[] {"and", "he", "the", "to", "is"};
        var startDate = DateTime.Now;
        var res = GetFrequentWords(literatureText, wordsToExclude);
        var resultTimeStamp = DateTime.Now - startDate;
        Console.WriteLine($"(Total Time in O(N**2): {resultTimeStamp}");
        Console.WriteLine();
        startDate = DateTime.Now;
        res = GetFrequentWordsFaster(literatureText, wordsToExclude);
        resultTimeStamp = DateTime.Now - startDate;
        Console.WriteLine($"(Total Time in O(N): {resultTimeStamp}");
        Console.ReadKey();
        */

        /*
         * Optimized Solution
         */

        public static string[] GetFrequentWordsFaster(string literatureText, string[] wordsToExclude)
        {
            if (string.IsNullOrWhiteSpace(literatureText)) return new string[0];
            literatureText = literatureText.ToLower(); //O(1)
            var result = new List<string>();

            //Create a Dictionary with the allowed alphabetic characters
            var allowedCharacters = new Dictionary<char, int>
            {
                {
                    'a', 0
                },
                {'b', 0},
                {'c', 0},
                {'d', 0},
                {'e', 0},
                {'f', 0},
                {'g', 0},
                {'h', 0},
                {'i', 0},
                {'j', 0},
                {'k', 0},
                {'l', 0},
                {'m', 0},
                {'n', 0},
                {'o', 0},
                {'p', 0},
                {'q', 0},
                {'r', 0},
                {'s', 0},
                {'t', 0},
                {'u', 0},
                {'v', 0},
                {'x', 0},
                {'w', 0},
                {'y', 0},
                {'z', 0},
                {'A', 0},
                {'B', 0},
                {'C', 0},
                {'D', 0},
                {'E', 0},
                {'F', 0},
                {'G', 0},
                {'H', 0},
                {'I', 0},
                {'J', 0},
                {'K', 0},
                {'L', 0},
                {'M', 0},
                {'N', 0},
                {'O', 0},
                {'P', 0},
                {'Q', 0},
                {'R', 0},
                {'S', 0},
                {'T', 0},
                {'U', 0},
                {'V', 0},
                {'X', 0},
                {'W', 0},
                {'Y', 0},
                {'Z', 0}
            };
            var lText = literatureText.ToArray(); //O(N)
            for (var c = 0; c < lText.Length; c++) //O(N)
                if (!allowedCharacters.ContainsKey(lText[c])) lText[c] = ' '; //O(1) 
            var splitArray = string.Join(string.Empty, lText) //O(N**2) it seems <-- Bottleneck
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var fqwords = new Dictionary<string, int>();
            var maxFrequency = 1;
            var wordsToExcludeDict = new Dictionary<string, int>();
            foreach (var word in wordsToExclude) //O(N)   
                wordsToExcludeDict.Add(word, 0);
            foreach (var word in splitArray) //O(N)
            {
                if (wordsToExcludeDict.ContainsKey(word)) continue; //O(1) 
                if (fqwords.ContainsKey(word)) //O(1)
                {
                    fqwords[word] = fqwords[word] + 1;
                    if (fqwords[word] > maxFrequency) maxFrequency = fqwords[word];
                }
                else
                {
                    fqwords.Add(word, 1);
                }
            }
            foreach (var word in fqwords) //O(N)
                if (word.Value == maxFrequency) result.Add(word.Key);
            return result.ToArray(); //O(N)
        }

        /*
         * Complexity O(N**2) due the Array.Contains inside a loop
         */
        public static string[] GetFrequentWords(string literatureText, string[] wordsToExclude)
        {
            var result = new List<string>();

            //check nulls,
            if (string.IsNullOrWhiteSpace(literatureText)) return new string[0];

            //Create a Dictionary with the allowed alphabetic characters
            var allowedCharacters = new[]
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
                'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
            var lText = literatureText.ToArray(); //O(N)
            for (var c = 0; c < lText.Length; c++) //O(N)
                if (!allowedCharacters.Contains(lText[c])) lText[c] = ' '; //O(1) since is a fixed array
            //Remove words to Exclude
            var splitArray = string.Join(string.Empty, lText) //O(N**2) ??
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var fqwords = new Dictionary<string, int>();
            var maxFrequency = 1;
            foreach (var word in splitArray) //O(N)
            {
                if (wordsToExclude.Contains(word)) continue; //O(N) <-- O(N**2) 
                /*
                 * Solution to this bottle neck, 
                 * 1. is to remove the words using Booyer-Moore algorithm
                 * 2. implement it using a Trie/Prefix Tree
                 * 3. Insert words to a dictionary to get a ContainsKey => O(1)
                 */
                var w = word.ToLower();
                if (fqwords.ContainsKey(w)) //O(1)
                {
                    fqwords[w] = fqwords[w] + 1;
                    if (fqwords[w] > maxFrequency) maxFrequency = fqwords[w];
                }
                else
                {
                    fqwords.Add(w, 1);
                }
            }
            foreach (var word in fqwords) //O(N)
                if (word.Value == maxFrequency) result.Add(word.Key);

            //iterate through dictionary and get maxfrequency
            //get keys and insert into result array.
            return result.ToArray(); //O(N)
        }

        #endregion

        #region LYFT

        /*
         * Lyft
         */
        /*  TEST CASE
         *  var stMax = new MaxStack();

            stMax.Push(45);
            stMax.Push(-10);

            var max = stMax.Max();
            Console.WriteLine(max);

            stMax.Push(68);
            max = stMax.Max();
            Console.WriteLine(max);

            stMax.Pop();
            max = stMax.Max();
            Console.WriteLine(max);
         */
        public class MaxStack
        {
            private readonly Stack<InnerObject> _mStack;

            public MaxStack()
            {
                _mStack = new Stack<InnerObject>();
            }

            public void Push(int val)
            {
                //o(1)
                var current = new InnerObject { Val = val };
                if (IsEmpty())
                {
                    current.Max = val;
                }
                else
                {
                    var max = Max();
                    if (max != null) current.Max = max.Value;
                    if (val > current.Max) current.Max = val;
                }
                _mStack.Push(current);
            }

            public int? Pop()
            {
                //o(1)
                if (IsEmpty()) return null;
                var current = _mStack.Pop();
                return current.Val;
            }

            public int? Peek()
            {
                //o(1)
                if (IsEmpty()) return null;
                var current = _mStack.Peek();
                return current.Val;
            }

            public int? Max()
            {
                //o(1)
                if (IsEmpty()) return null;
                var current = _mStack.Peek();
                return current.Max;
            }

            public bool IsEmpty()
            {
                return _mStack.Count == 0;
            }
        }

        public class InnerObject
        {
            public int Val { get; set; }
            public int Max { get; set; }
        }


        /*
    * Clone a list with random pointer
    */

        private class LNode
        {
            public LNode Next { get; set; }
            public LNode Random { get; set; }
            public int Value { get; set; }
        }

        private static LNode CloneLinkedList(LNode node)
        {
            var h = new LNode();
            while (node != null)
            {
                h.Next = new LNode
                {
                    Value = node.Value,
                    Next = node.Next
                };
                node = node.Next;
            }
            node = h.Next;
            while (h.Next != null)
            {
                node.Next = h.Next.Next;
                h.Next.Random = node.Random;
                h.Next = h.Next.Next;
            }
            return h.Next;
        }

        /*
         * Check if is BST or not
         */

        private static bool IsBst(TreeNode node, long min = long.MinValue, long max = long.MaxValue)
        {
            if (node == null) return true;
            if (node.Value < min || node.Value > max) return false;
            return IsBst(node.Left, min, node.Value) && IsBst(node.Right, node.Value, max);
        }

        /*
         * Cartesian Product using Backtracking
         *  
            var m = new string[4][];
            m[0] = new[] {"grey", "black"};
            m[1] = new[] {"fox", "dog"};
            m[2] = new[] {"pumped", "ran", "growled"};
            m[3] = new[] { "oil", "speed", "air"};
         */

        private static List<string> GetC(string[][] m)
        {
            var result = new List<string>();
            var product = m.Aggregate(1, (current, n) => current * n.Length);
            Combine(m, 0, 0, string.Empty, result, ref product);
            return result;
        }

        private static void Combine(IReadOnlyList<string[]> m, int r, int c, string current, ICollection<string> result, ref int prod)
        {
            if (result.Count == prod) return;
            if (current.Split(' ').Length == m.Count)
                result.Add(current);
            else
            {
                for (var i = r; i < m.Count; i++)
                {
                    for (var j = c; j < m[r].Length; j++)
                    {
                        current += $" {m[r][j]}";
                        Combine(m, r + 1, c, current.Trim(), result, ref prod);
                        current = RemoveLastString(current);
                    }
                    current = RemoveLastString(current);
                }
            }
        }

        private static string RemoveLastString(string str)
        {
            var w = str.Split(' ');
            str = string.Empty;
            for (var k = 0; k < w.Length - 1; k++) str += $" {w[k]}";
            return str.Trim();
        }

        private static long GetWays(long n, long[] c)
        {
            var map = new long[n + 1];
            for (var i = 0; i < map.Length; i++) map[i] = 0;
            map[0] = 1;

            foreach (var t in c)
                for (var j = t; j <= n; j++)
                    map[j] += map[j - t];

            return map[n];
        }

        /*
         * Determine if a tree is a mirror
         */

        public static bool IsMirror(TreeNode node)
        {
            return node == null || IsMirror(node.Left, node.Right);
        }

        public static bool IsMirror(TreeNode left, TreeNode right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null || left.Value != right.Value) return false;
            return IsMirror(left.Left, right.Left) && IsMirror(left.Right, right.Right);
        }


        #endregion
    }

    public class TreeNode
    {
        public TreeNode Left;
        public TreeNode Right;
        public int Val;

        public TreeNode(int x)
        {
            Val = x;
        }
    }

    public class ListNode
    {
        public ListNode Next;
        public int Val;

        public ListNode(int x)
        {
            Val = x;
        }
    }
}
