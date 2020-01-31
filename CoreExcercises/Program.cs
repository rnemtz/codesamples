using System;

namespace CoreExcercises
{
    internal class Program
    {
        private static void Main(string[] args)
        {
           


            Console.ReadKey();
        }

        #region Max Path sum in a tree
        /*
         *
            var node = new TreeNode(-10)
            {
                Left = new TreeNode(9),
                Right = new TreeNode(20)
                {
                    Left = new TreeNode(15),
                    Right = new TreeNode(7)
                }
            };
            var maxSum = MaxSum(node); 
            answer max = 42
         *
         *
         */
        private static int max = int.MinValue;

        private static int MaximumSum(TreeNode node)
        {
            if (node == null)
            {
                return 0 ;
            }

            var left = MaximumSum(node.Left);
            var right = MaximumSum(node.Right);
            
            max = Math.Max(max, left + right + node.Val);

            return Math.Max(node.Val + Math.Max(left, right), 0);
        }

        private static int MaxSum(TreeNode node)
        {
            if (node == null) return 0;
            var left = MaxSum(node.Left);
            var right = MaxSum(node.Right);
            max = Math.Max(max, left + right + node.Val);

            return Math.Max(node.Val + Math.Max(left, right), 0);
        }
        #endregion

        #region TreeDepth
        /*
            1. Find Minimum Depth of a Binary Tree
            var node = new TreeNode(15)
            {
                Left = new TreeNode(5)
                {
                    Left = new TreeNode(3),
                    Right = new TreeNode(7)
                },
                Right = new TreeNode(20)
                {
                    Left = new TreeNode(17),
                    Right = new TreeNode(23)
                    {
                        Left = new TreeNode(22)
                    }
                }
            };

            var min = FindMinimumDepth(node);
            var max = FindMaximumDepth(node);
            Console.WriteLine($"Minimum: {min}, Maximum: {max}");
        */


        private static int FindMinimumDepth(TreeNode node)
        {
            return node == null ? -1 : Math.Min(FindMinimumDepth(node.Left), FindMinimumDepth(node.Right)) + 1;
        }

        private static int FindMaximumDepth(TreeNode node)
        {
            return node == null ? -1 : Math.Max(FindMaximumDepth(node.Left), FindMaximumDepth(node.Right)) + 1;
        }

        #endregion
    }
}
