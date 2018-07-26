using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeExercises.DataStructures
{
    public class LinkedList
    {
        public int Count { get; set; }
        public bool IsReadOnly => false;
        private Node Head { get; set; }
        private Node Tail { get; set; }

        public void AddFirst(Node node)
        {
            var temp = Head;
            Head = node;
            Head.Next = temp;
            Count++;
            if (Count == 1)
                Tail = Head;
        }

        public Node HeadNodeList()
        {
            return Head;
        }

        public void AddLast(Node node)
        {
            if (Count == 0)
                Head = node;
            else
                Tail.Next = node;
            Tail = node;
            Count++;
        }

        public void RemoveLast()
        {
            if (Count == 0) return;
            if (Count == 1)
            {
                Head = null;
                Tail = null;
            }
            else
            {
                var current = Head;
                while (current.Next != Tail)
                    current = current.Next;
                current.Next = null;
                Tail = current;
            }
            Count--;
        }

        public void RemoveFirst()
        {
            if (Count == 0) return;
            Head = Head.Next;
            Count--;
            if (Count == 0)
                Tail = null;
        }

        public ICollection<int> EnumerateValues()
        {
            var list = new List<int>();
            var node = Head;
            while (node != null)
            {
                list.Add(node.Value);
                node = node.Next;
            }
            return list;
        }

        private static void PrintList(Node node)
        {
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }
    }
}

namespace CodeExercises.LinkedLists
{
    public class LinkedList : IEnumerable
    {
        public Node Head { get; set; }
        public Node Tail { get; set; }
        public int Count { get; set; }
        public bool IsEmpty { get; set; }
        private SortedLinkedList Sorted { get; set; }


        public LinkedList()
        {
            Head = null;
            Tail = null;
            Count = 0;
            IsEmpty = true;
            Sorted = new SortedLinkedList() {IsSorted = false, Order = Order.Unsorted};
        }

        public void Add(int value)
        {
            //Add to tail by default; 
            var node = new Node {Value = value};
            if (IsEmpty)
            {
                Head = node;
                Tail = node;
                Sorted.IsSorted = true;
                Sorted.Order = Order.Ascending;
            }
            else
            {
                //Instead of enumerate to the last position we will use tail. O(1)
                Tail.Next = node;
                Tail = node;
                Sorted.IsSorted = false;
                Sorted.Order = Order.Unsorted;
            }
            //Increase Count and Update IsEmpty for o(1) operations
            Count++;
            IsEmpty = false;
        }

        public void AddFirst(int value)
        {
            var node = new Node {Value = value};
            if (IsEmpty)
            {
                Head = node;
                Tail = node;
                Sorted.IsSorted = true;
                Sorted.Order = Order.Ascending;
            }
            else
            {
                var currentHead = Head;
                Head = node;
                node.Next = currentHead;
                Sorted.IsSorted = false;
                Sorted.Order = Order.Unsorted;
            }
            Count++;
            IsEmpty = false;
        }

        public bool Remove(int value)
        {
            if (IsEmpty) return false;
            var node = Find(value, out var previousNode);
            if (node == null) return false;
            var currentNode = node;
            if (node == Head)
            {
                RemoveFirst();
            }
            else
            {
                var nextNode = currentNode.Next;
                if (nextNode == null)
                {
                    RemoveLast();
                }
                else
                {
                    previousNode.Next = nextNode;
                    Count--;
                }
            }
            return true;
        }

        public void RemoveLast()
        {
            if (IsEmpty) return;
            if (Tail == Head)
            {
                Head = null;
                Tail = null;
                Count = 0;
                IsEmpty = true;
            }
            else
            {
                var currentNode = Head;
                while (currentNode.Next != Tail)
                    currentNode = currentNode.Next;
                currentNode.Next = null;
                Tail = currentNode;
                Count--;
            }
        }

        public void RemoveFirst()
        {
            if (IsEmpty) return;
            var nextNode = Head.Next;
            if (nextNode == null)
            {
                Head = null;
                Tail = null;
                Count = 0;
                IsEmpty = true;
            }
            else
            {
                Head = nextNode;
                Count--;
            }
        }

        public Node Find(int value, out Node previousNode)
        {
            var currentNode = Head;
            previousNode = null;
            while (currentNode != null)
            {
                if (currentNode.Value == value) return currentNode;
                previousNode = currentNode;
                currentNode = currentNode.Next;
            }
            return null;
        }

        public bool Contains(int value)
        {
            var node = Head;
            while (node != Tail)
            {
                if (node.Value == value) return true;
                node = node.Next;
            }
            return false;
        }

        public IEnumerator GetEnumerator()
        {
            var node = Head;
            while (Head != Tail)
            {
                yield return node.Value;
                node = node.Next;
            }
        }

        public void Print(Order order)
        {
            Sort(order);
            var node = Head;
            while (node != null)
            {
                Console.Write($"{node.Value} -> ");
                node = node.Next;
            }
            Console.Write("null");
        }


        /*
         * LinkedList Count: 1764
         * Reverse list STACK PASS Time elapsed: 00:00:00.0150190
         * Reverse list SINGLE PASS Time elapsed: 00:00:00.0010021
         */

        public void ReverseEfficient()
        {
            /*
             * Reverse in-place - More Efficient
             * Time O(N)
             * Space O(1)
             */

            Node previous = null;
            var current = Head;
            Tail = current;
            while (current != null)
            {
                if (current.Next == null) Head = current;
                var next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }
            Tail.Next = null;
            Sorted.IsSorted = false;
            Sorted.Order = Order.Unsorted;
        }

        public void Reverse()
        {
            /*
             * Reverse implementing a Stack
             * Time O(N)
             * Space O(N)
             */
            var stack = new Stack<Node>();
            var currentNode = Head;
            while (currentNode != null)
            {
                stack.Push(currentNode);
                currentNode = currentNode.Next;
            }
            Head = stack.Pop();
            currentNode = Head;
            while (stack.Count > 0)
            {
                currentNode.Next = stack.Pop();
                currentNode = currentNode.Next;
            }
            currentNode.Next = null;
            Tail = currentNode;
            Sorted.IsSorted = false;
            Sorted.Order = Order.Unsorted;
        }

        public bool IsCyclic()
        {
            return false;
        }

        public bool InsertAt(int index)
        {
            return false;
        }

        public void Sort(Order order)
        {
            if (Sorted.IsSorted && Sorted.Order == order) return;
            //Implement QuickSort, MergeSort
            switch (order)
            {
                case Order.Ascending:
                    break;
                case Order.Descending:

                    break;
                case Order.Unsorted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(order), order, null);
            }
            Sorted.IsSorted = true;
            Sorted.Order = order;
        }
    }

    public enum Order
    {
        Ascending = 1,
        Descending = 2,
        Unsorted = 3
    }

    public class SortedLinkedList
    {
        public bool IsSorted { get; set; }
        public Order Order { get; set; }
    }

    public class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }
    }
}