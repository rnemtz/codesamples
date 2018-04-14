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
}