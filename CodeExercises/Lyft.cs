using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercises
{
    public class Lyft
    {
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
            if (node.Val < min || node.Val > max) return false;
            return IsBst(node.Left, min, node.Val) && IsBst(node.Right, node.Val, max);
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
            if (left == null || right == null || left.Val != right.Val) return false;
            return IsMirror(left.Left, right.Left) && IsMirror(left.Right, right.Right);
        }


        #endregion
    }
}
