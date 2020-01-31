using System.Collections.Generic;
using System.Linq;

namespace CodeExercises
{
    public class LeetCode
    {
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
    }
}
