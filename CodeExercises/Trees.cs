using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeExercises
{
    public class NBinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private int _count;
        public NTreeNode<T> Current { get; set; }

        private NTreeNode<T> Head { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T value)
        {
            //Case 1: Tree is empty
            if (Head == null) Head = new NTreeNode<T>(value);
            //Case 2: Tree is not empty, find the right location to insert
            else
                AddTo(Head, value);

            _count++;
        }

        private void AddTo(NTreeNode<T> node, T value)
        {
            //Case 1 Value is less than current node value
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.LeftNode == null) node.LeftNode = new NTreeNode<T>(value);
                else AddTo(node.LeftNode, value);
            }
            //Case 2 Value is equal or greater than the current value
            else
            {
                if (node.RightNode == null) node.RightNode = new NTreeNode<T>(value);
                else AddTo(node.RightNode, value);
            }
        }

        public bool Remove(T value)
        {
            return true;
        }

        public NTreeNode<T> Find(T value)
        {
            return new NTreeNode<T>(value);
        }
    }

    public class NTreeNode<T> : IComparable<T> where T : IComparable<T>
    {
        public NTreeNode(T value)
        {
            Value = value;
        }

        public NTreeNode<T> LeftNode { get; set; }
        public NTreeNode<T> RightNode { get; set; }
        public T Value { get; set; }

        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }


    public class BinarySearchTree
    {
        public BinarySearchTreeNode Root { get; set; }

        public int Count { get; set; }

        public void Add(int value)
        {
            //Case 1, Root is null
            if (Root == null)
            {
                Root = new BinarySearchTreeNode {Value = value};
                Count++;
                return;
            }

            Add(Root, value);
        }

        private void Add(BinarySearchTreeNode node, int value)
        {
            //Case 2, Value is less than node value
            if (value < node.Value)
            {
                if (node.Left == null)
                {
                    node.Left = new BinarySearchTreeNode {Value = value};
                    Count++;
                    return;
                }
                Add(node.Left, value);
                return;
            }

            //Case 3, Value is equal or more than node value
            if (node.Right == null)
            {
                node.Right = new BinarySearchTreeNode {Value = value};
                Count++;
                return;
            }
            Add(node.Right, value);
        }

        public void Remove(int value)
        {
            //Find the node
            var current = FindWithParent(value, out BinarySearchTreeNode parent);
            if (current == null) return;
            Count--;

            //3 scenarios
            //Case 1 Removed node that has no right child.
            if (current.Right == null)
            {
                if (parent == null) Root = current.Left;
                else
                {
                    if (parent.Value > current.Value) parent.Left = current.Left;
                    else if (parent.Value < current.Value) parent.Right = current.Left;
                }
            }
            //Case 2 Removed right child has no left child.
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null) Root = current.Right;
                else
                {
                    if (parent.Value > current.Value) parent.Left = current.Right;
                    else if (parent.Value < current.Value) parent.Right = current.Right;
                }
            }
            //Case 3 Removed right child has left child.
            else
            {
                var leftmost = current.Right.Left;
                var leftmostParent = current.Right;

                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }
                leftmostParent.Left = leftmost.Right;
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                if (parent == null) Root = leftmost;
                else
                {
                    if (parent.Value > current.Value) parent.Left = leftmost;
                    else if (parent.Value < current.Value) parent.Right = leftmost;
                }
            }
        }

        private BinarySearchTreeNode FindWithParent(int value, out BinarySearchTreeNode parent)
        {
            var current = Root;
            parent = null;

            while (current != null)
            {
                if (value < current.Value)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (value > current.Value)
                {
                    parent = current;
                    current = current.Right;
                }
                break;
            }
            return current;
        }

        public BinarySearchTreeNode Find(int value)
        {
            return Find(Root, value);
        }

        private BinarySearchTreeNode Find(BinarySearchTreeNode node, int value)
        {
            if (node == null) return null;
            return node.Value == value ? node : Find(value < node.Value ? node.Left : node.Right, value);
        }

        public void Enumerate()
        {
            Console.WriteLine("Pre-Order");
            Traversal(Root, TraverseType.PreOrder);

            Console.WriteLine("In-Order");
            Traversal(Root, TraverseType.InOrder);

            Console.WriteLine("Post-Order");
            Traversal(Root, TraverseType.PostOrder);
        }

        public void TraversalFrom(int value)
        {
            var node = Find(value);
            if (node == null)
            {
                Console.WriteLine("The value doesn't exist in the Binary Search Tree");
                return;
            }

            Console.WriteLine("Pre-Order");
            Traversal(node, TraverseType.PreOrder);

            Console.WriteLine("In-Order");
            Console.WriteLine(string.Empty);
            Traversal(node, TraverseType.InOrder);

            Console.WriteLine("Post-Order");
            Console.WriteLine(string.Empty);
            Traversal(node, TraverseType.PostOrder);
        }

        private void Traversal(BinarySearchTreeNode node, TraverseType type)
        {
            if (node == null) return;
            switch (type)
            {
                case TraverseType.PreOrder:
                    Console.WriteLine($"Node: {node.Value}");
                    Traversal(node.Left, type);
                    Traversal(node.Right, type);
                    break;
                case TraverseType.InOrder:
                    Traversal(node.Left, type);
                    Console.WriteLine($"Node: {node.Value}");
                    Traversal(node.Right, type);
                    break;
                case TraverseType.PostOrder:
                    Traversal(node.Left, type);
                    Traversal(node.Right, type);
                    Console.WriteLine($"Node: {node.Value}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void Clear()
        {
            Count = 0;
            Root = null;
        }

    }

    public enum TraverseType
    {
        PreOrder = 1,
        InOrder = 2,
        PostOrder = 3
    }

    public class BinarySearchTreeNode
    {
        public int Value { get; set; }
        public BinarySearchTreeNode Left { get; set; }
        public BinarySearchTreeNode Right { get; set; }
    }
}