using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercises.Internal
{
    public class BsTree
    {
        public BstNode Root { get; set; }

        public BsTree()
        {
            var n1 = new BstNode(10);
            var n2 = new BstNode(5);
            var n5 = new BstNode(15);

            n1.LeftNode = n2;
            n1.RightNode = n5;

            var n3 = new BstNode(3);
            var n4 = new BstNode(7);

            n2.LeftNode = n3;
            n2.RightNode = n4;
           
            var n6 = new BstNode(13);
            var n7 = new BstNode(17);

            n5.RightNode = n7;
            n5.LeftNode = n6;

            Root = n1;
        }

        public void Add(int val)
        {
           
        }

        public bool IsEmpty()
        {
            return Root == null;
        }


        public void InOrder()
        {
            PrintInOrder(Root);
        }

        private void PrintInOrder(BstNode root)
        {
            if (root == null)
            {
                return;
            }

            PrintInOrder(root.LeftNode);
            Console.WriteLine(root.Value);
            PrintInOrder(root.RightNode);
        }

        public void PreOrder()
        {
           PrintPreOrder(Root);
        }

        private void PrintPreOrder(BstNode root)
        {
            if (root == null)
            {
                return;
            }

            Console.WriteLine(root.Value);
            PrintPreOrder(root.LeftNode);
            PrintPreOrder(root.RightNode);
        }

        public void PostOrder()
        {
            PrintPostOrder(Root);
        }

        private void PrintPostOrder(BstNode root)
        {
            if (root == null)
            {
                return;
            }

            PrintPostOrder(root.LeftNode);
            PrintPostOrder(root.RightNode);
            Console.WriteLine(root.Value);
        }

    }

    public class BstNode
    {
        public int Value { get; set; }
        public BstNode LeftNode { get; set; }
        public BstNode RightNode { get; set; }

        public BstNode(int value)
        {
            Value = value;
        }
    }
}
