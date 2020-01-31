using System;

namespace CodeExercises
{
    public class Microsoft
    {
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
    }
}
