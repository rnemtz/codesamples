using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoreExcercises
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            
            Console.ReadKey();
        }
        #region BinarySearch
        /*
         *
            var test = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9};
            var result = BinarySearch(test, 2);
         *
         */

        public static bool BinarySearch(int[] array, int a)
        {
            var min = 0;
            var max = array.Length - 1;

            while (min <= max)
            {
                var mid = min + (max - min) / 2;

                if (a == array[mid])
                {
                    return true;
                }

                if (a < array[mid])
                {
                    max = mid - 1;
                }
                else
                {
                    min = mid + 1;
                }
            }

            return false;
        }

        #endregion



        #region Lowest Common Ancestor BST and BT
        /**/
        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null) return null;

            while (root != null)
            {
                if (p.Val < root.Val && q.Val < root.Val) root = root.Left;
                else if (p.Val > root.Val && q.Val > root.Val) root = root.Right;
                else return root;
            }
            return null;
        }

        public static int LowestCa(TreeNode node, TreeNode l, TreeNode m)
        {
            if (node == null)
            {
                return 0;
            }

            while (node != null)
            {
                if (l.Val < node.Val && m.Val < node.Val)
                {
                    node = node.Left;
                }
                else if (l.Val > node.Val && m.Val > node.Val)
                {
                    node = node.Right;
                }
                else
                {
                    return node.Val;
                }
            }

            return 0;
        }

        private static TreeNode result;

        public static bool TraverseTree(TreeNode currentNode, TreeNode p, TreeNode q)
        {
            // If reached the end of a branch, return false.
            if (currentNode == null)
            {
                return false;
            }

            // Left Recursion. If left recursion returns true, set left = 1 else 0
            var left = TraverseTree(currentNode.Left, p, q) ? 1 : 0;

            // Right Recursion
            var right = TraverseTree(currentNode.Right, p, q) ? 1 : 0;

            // If the current node is one of p or q
            var mid = (currentNode == p || currentNode == q) ? 1 : 0;

            // If any two of the flags left, right or mid become True
            if (mid + left + right >= 2)
            {
                result = currentNode;
            }

            // Return true if any one of the three bool values is True.
            return (mid + left + right > 0);
        }
        #endregion

        #region Bottom View Binary Tree
        /*  
            var node = new TreeNode(20)
            {
                Left = new TreeNode(8)
                {
                    Left = new TreeNode(5),
                    Right = new TreeNode(3)
                    {
                        Left = new TreeNode(10),
                        Right = new TreeNode(14)
                    }
                },
                Right = new TreeNode(22)
                {
                    Left = new TreeNode(4),
                    Right = new TreeNode(25)
                }
            };
            foreach (var c in BottomView(node))
            {
                Console.Write($"{c} ");
            }
         */

        private static List<int> BottomView(TreeNode node)
        {
            var list = new List<int>();
            GetList(node, list);
            return list;
        }

        private static void GetList(TreeNode node, List<int> list)
        {
            if (node == null)
            {
                return;
            }

            GetList(node.Left, list);

            if (node.Left == null && node.Right == null)
            {
                list.Add(node.Val);
                return;
            } 

            GetList(node.Right, list);
        }

        #endregion

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
                return 0;
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
