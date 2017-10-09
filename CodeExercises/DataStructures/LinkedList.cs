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