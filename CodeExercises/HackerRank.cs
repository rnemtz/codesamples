using System.Collections.Generic;
using System.Linq;

namespace CodeExercises
{
    public class HackerRank
    {
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
    }
}
