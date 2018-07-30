using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CodeExercises.Sorting;

namespace CodeExercises
{
    internal class Program
    {
        private static void Main()
        {
            Console.ReadKey();
        }

        #region CODE WARS && OTHER

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
                ? Math.Floor((decimal) (year / 100)) + 1
                // ReSharper disable once PossibleLossOfFraction
                : Math.Floor((decimal) (year / 100)));
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
            var lines = new List<string> {"URL, Error Count, Percentage"};
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
                    .Aggregate(result, (current, word) => current.Replace(string.Format(" {0} ", word), " ")
                        .Replace(string.Format("{0} ", word), " ")
                        .Replace(string.Format(" {0}", word), " "));
            var words = result.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
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
                .Select(n => new {number = n.Key, value = n.Count()})
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
                if (maxCacheSize < 0) throw new ArgumentException();
                _maxCacheSize = maxCacheSize;
                _itemsDictionary = new Dictionary<int, Node>();
                _head = null;
            }

            public void Set(int key, string value)
            {
                if (!_itemsDictionary.TryGetValue(key, out var item))
                {
                    item = new Node {Key = key, Value = value};
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
                    Root = new NNode(key, null) {Level = 1};
                }
                else
                {
                    var node = Find(parent);
                    if (node == null) Root.Children.Add(new NNode(key, Root) {Level = Root.Level + 1});
                    else Add(node, key);
                }
            }

            private void Add(NNode parent, string value)
            {
                parent?.Children.Add(new NNode(value, parent) {Level = parent.Level + 1});
            }

            public NNode Find(string key)
            {
                return string.IsNullOrWhiteSpace(key) ? null : Find(Root, key);
            }

/* BDF Approach */
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

/* BDF Approach */
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
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
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
                .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
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
                var current = new InnerObject {Val = val};
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
 * Lyft
 */

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