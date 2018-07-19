using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeExercises
{
    internal class Program
    {
        private static void Main()
        {
           Console.ReadLine();
        }

        #region cases
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
            private readonly System.Collections.Generic.Stack<InnerObject> MStack;

            public MaxStack()
            {
                MStack = new System.Collections.Generic.Stack<InnerObject>();
            }


            public void Push(int val)
            {
                //o(1)
                var current = new InnerObject() { Val = val };
                if (IsEmpty()) current.Max = val;
                else
                {
                    current.Max = Max().Value;
                    if (val > current.Max) current.Max = val;
                }
                MStack.Push(current);
            }

            public int? Pop()
            {
                //o(1)
                if (IsEmpty()) return null;
                var current = MStack.Pop();
                return current.Val;
            }

            public int? Peek()
            {
                //o(1)
                if (IsEmpty()) return null;
                var current = MStack.Peek();
                return current.Val;
            }

            public int? Max()
            {
                //o(1)
                if (IsEmpty()) return null;

                var current = MStack.Peek();
                return current.Max;
            }

            public bool IsEmpty()
            {
                return MStack.Count == 0;
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

        public static int СenturyFromYear(int year)
        {
            return Convert.ToInt32(year % 100 > 0 ? Math.Floor((decimal)(year / 100)) + 1 : Math.Floor((decimal)(year / 100)));
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
            {
                foreach (var letter in word.ToCharArray())
                {
                    if (!char.IsNumber(letter)) continue;
                    result[int.Parse(letter.ToString()) - 1] = word;
                    break;
                }
            }
            return string.Join(" ", result);
        }

        public static int GeneralizedGcd(int num, int[] arr)
        {
            var hcf = 2;
            while (true)
            {
                for (var i = 0; i < arr.Length; i++)
                {
                    if (arr[i] % hcf != 0)
                        return hcf - 1;
                }
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
                    {
                        if (currentStates[r + 1] == 0)
                        {
                            newStates[r] = 0;
                        }
                        else
                        {
                            newStates[r] = 1;
                        }
                    }
                    if (r > 0 && r < states.Length - 1)
                    {
                        if (currentStates[r - 1] == currentStates[r + 1])
                        {
                            newStates[r] = 0;
                        }
                        else
                        {
                            newStates[r] = 1;
                        }
                    }
                    if (r != states.Length - 1) continue;
                    if (currentStates[r - 1] == 0)
                    {
                        newStates[r] = 0;
                    }
                    else
                    {
                        newStates[r] = 1;
                    }
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
            var linesFromFile = System.IO.File.ReadAllLines(@"C:\Obsidian\DAP-Errors.txt");
            foreach (var line in linesFromFile)
            {
                try
                {
                    var url = new Uri(line.Split(' ')[0]);
                    var urlPath = url.Scheme + "//" + url.DnsSafeHost + url.AbsolutePath;
                    urlPath = Regex.Replace(urlPath, @"[({]?[a-zA-Z0-9]{8}[-]?([a-zA-Z0-9]{4}[-]?){3}[a-zA-Z0-9]{12}[})]?", string.Empty, RegexOptions.IgnoreCase);
                    urlPath = Regex.Replace(urlPath, @"(\d+)$", string.Empty, RegexOptions.IgnoreCase);
                    if (urlList.ContainsKey(urlPath))
                    {
                        urlList[urlPath]++;
                    }
                    else
                    {
                        urlList.Add(urlPath, 1);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            var lines = new List<string> { "URL, Error Count, Percentage" };
            var total = urlList.Sum(x => x.Value);
            lines.AddRange(urlList.OrderByDescending(x => x.Value).Select(error => $"{error.Key},{error.Value},{Convert.ToDecimal(error.Value) * 100 / total}"));
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

        public static List<string> RetrieveMostFrequentlyUsedWords(String literatureText,
            List<string> wordsToExclude)
        {
            //Validate Input List
            if (string.IsNullOrWhiteSpace(literatureText)) return new List<string>();

            //Convert to space any special character
            var inputLiteratureText = literatureText.Where(t => !char.IsLetter(t)).Aggregate(literatureText, (current, t) => current.Replace(t.ToString(), " "));

            //Exclude common word from List
            var result = inputLiteratureText.ToLower();
            if (wordsToExclude != null && wordsToExclude.Any())
                result = wordsToExclude.Select(x => x.ToLower())
                    .Aggregate(result, (current, word) => current.Replace(string.Format(" {0} ", word), " ")
                        .Replace(string.Format("{0} ", word), " ")
                        .Replace(string.Format(" {0}", word), " "));

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