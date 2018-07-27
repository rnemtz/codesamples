using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeExercises.Trees
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private int _count;
        public TreeNode<T> Current { get; set; }

        private TreeNode<T> Head { get; set; }

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
            if (Head == null) Head = new TreeNode<T>(value);
            //Case 2: Tree is not empty, find the right location to insert
            else
                AddTo(Head, value);
            _count++;
        }

        private void AddTo(TreeNode<T> node, T value)
        {
            //Case 1 Value is less than current node value
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.LeftNode == null) node.LeftNode = new TreeNode<T>(value);
                else AddTo(node.LeftNode, value);
            }
            //Case 2 Value is equal or greater than the current value
            else
            {
                if (node.RightNode == null) node.RightNode = new TreeNode<T>(value);
                else AddTo(node.RightNode, value);
            }
        }

        public bool Contains(T value)
        {
            TreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        private TreeNode<T> FindWithParent(T value, out TreeNode<T> parent)
        {
            var current = Head;
            parent = null;

            //while don't have a match
            while (current != null)
            {
                var result = current.CompareTo(value);
                if (result > 0)
                {
                    //if the value is less than current, go left;
                    parent = current;
                    current = current.LeftNode;
                }
                else if (result < 0)
                {
                    //if the value is more than current, go right.
                    parent = current;
                    current = current.RightNode;
                }
                else
                {
                    //match!
                    break;
                }
            }
            return current;
        }

        public bool Remove(T value)
        {
            //Find the node
            //Leaf (terminal node
            //Non Leaf node
            var current = FindWithParent(value, out var parent);
            if (current == null) return false;

            _count--;
            //Case 1 Removing a node has no right child,
            //Find Node to remove
            //Has no right child
            //Promote left child
            if (current.RightNode == null)
            {
                if (parent == null)
                {
                    Head = current.LeftNode;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);
                    if (result > 0) parent.LeftNode = current.LeftNode;
                    else if (result < 0) parent.RightNode = current.LeftNode;
                }
            }
            //Case 2 Remove right child has no left child.
            //Find node to remove
            //Node right has no left
            //Promote right child
            else if (current.RightNode.LeftNode == null)
            {
                current.RightNode.LeftNode = current.LeftNode;
                if (parent == null)
                {
                    Head = current.RightNode;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);
                    if (result > 0) parent.LeftNode = current.RightNode;
                    else if (result < 0) parent.RightNode = current.RightNode;
                }
            }
            //Case 3 Remove right child has left child
            //Find node to remove
            //Node right has left
            //Find right's left most child
            //Promote that node to deleted node.
            else
            {
                var leftmost = current.RightNode.LeftNode;
                var leftmostParent = current.RightNode;

                while (leftmostParent.LeftNode != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.LeftNode;
                }

                leftmostParent.LeftNode = leftmost.RightNode;
                leftmost.LeftNode = current.LeftNode;
                leftmost.RightNode = current.RightNode;

                if (parent == null)
                {
                    Head = leftmost;
                }
                else
                {
                    var result = parent.CompareTo(current.Value);
                    if (result > 0) parent.LeftNode = leftmost;
                    else if (result < 0) parent.RightNode = leftmost;
                }
            }
            return true;
        }

        public void Traversal(Action<T> action)
        {
            VisitPreOrder(action, Head);
            VisitInOrder(action, Head);
            VisitPostOrder(action, Head);
        }

        public IEnumerator<T> InOrderTraversal()
        {
            if (Head != null)
            {
                var stack = new Stack<TreeNode<T>>();
                var current = Head;
                var goLeft = true;

                stack.Push(current);
                while (stack.Count > 0)
                {
                    if (goLeft)
                    {
                        while (current.LeftNode != null)
                        {
                            stack.Push(current);
                            current = current.LeftNode;
                        }
                    }
                    //in order is LEFT-YIELD-RIGHT
                    yield return current.Value;
                    if (current.RightNode != null)
                    {
                        current = current.RightNode;
                        goLeft = true;
                    }
                    else
                    {
                        current = stack.Pop();
                        goLeft = false;
                    }

                }
            }
        }

        public void VisitPreOrder(Action<T> action, TreeNode<T> node)
        {
            if (node == null) return;
            action(node.Value);
            VisitPreOrder(action, node.LeftNode);
            VisitPreOrder(action, node.RightNode);

            //4
            //2            //6
            //1     //3    //5      //7 

            //Traversal: 4 2 1 3 6 5 7
        }

        public void VisitInOrder(Action<T> action, TreeNode<T> node)
        {
            if (node == null) return;
            VisitInOrder(action, node.LeftNode);
            action(node.Value);
            VisitInOrder(action, node.RightNode);

            //4
            //2            //6
            //1     //3    //5      //7 

            //Traversal: 1 2 3 4 5 6 7
        }

        public void VisitPostOrder(Action<T> action, TreeNode<T> node)
        {
            if (node == null) return;
            VisitPostOrder(action, node.LeftNode);
            VisitPostOrder(action, node.RightNode);
            action(node.Value);

            //4
            //2            //6
            //1      //3    //5      //7 

            //Traversal: 1 3 2 5 7 6 4
        }

        public TreeNode<T> Find(TreeNode<T> node, int value)
        {
            if (node == null) return null;
            if (value.CompareTo(node.Value) == 0) return Current;
            if (value.CompareTo(node.Value) < 0) return Find(node.LeftNode, value);

            return Find(Current.RightNode, value);
        }
    }

    public class TreeNode<T> : IComparable<T> where T : IComparable<T>
    {
        public TreeNode(T value)
        {
            Value = value;
        }

        public TreeNode<T> LeftNode { get; set; }
        public TreeNode<T> RightNode { get; set; }
        public T Value { get; set; }

        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }
}