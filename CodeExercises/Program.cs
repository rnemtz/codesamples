using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeExercises
{
    internal class Program
    {
        private static void Main()
        {
            Console.ReadLine();
        }

        public static int SquareDigits(int n)
        {
            var numbers = n.ToString().ToArray();
            var result = numbers.Aggregate(string.Empty, (current, digit) => current + (int.Parse(digit.ToString()) * int.Parse(digit.ToString())).ToString());
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
            var isEven = integers.Where(x => x % 2 == 0).Count() > 1;
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
                morseCode.Split(new[] {"   "}, StringSplitOptions.None).Select(word => string.Join(string.Empty,
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
                node.next = new ListNode(reverseNumber[i]);
                node = node.next;
            }
            return node;
        }

        public int GetNumber(ListNode l)
        {
            var number = new List<string>();
            while (l != null)
            {
                number.Add(l.val.ToString());
                l = l.next;
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
            return new[] {input.Where(x => x >= 0).Count(), input.Where(x => x < 0).Sum()};
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
            var leftDepth = MaxDepth(node.left);
            var rightDepth = MaxDepth(node.right);

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

        public class ListNode
        {
            public ListNode next;
            public int val;

            public ListNode(int x)
            {
                val = x;
            }
        }
    }
}