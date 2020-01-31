using System;

namespace CoreExcercises
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            Console.ReadKey();
        }

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
