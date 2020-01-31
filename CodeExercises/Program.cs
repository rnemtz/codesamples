using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using CodeExercises.Internal;

namespace CodeExercises
{
    internal class Program
    {
        private static void Main()
        {

            Console.ReadKey();
        }

        
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
