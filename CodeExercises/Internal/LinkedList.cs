using System;

namespace CodeExercises.Internal
{
    public class LinkedList
    {
        private LinkedListNode head;
        private LinkedListNode tail;

        public int Count { get; private set; }

        public LinkedList()
        {
            head = null;
            tail = null;
            Count = 0;
        }

        public void AddAfter(LinkedListNode node, int value)
        {
            if (node == null)
            {
                return;
            }

            var current = new LinkedListNode(value);
            var temp = node.Next;
            node.Next = current;
            current.Next = temp;
            Count++;
        }

        public void AddBefore(LinkedListNode node, int value)
        {
            if (node == null)
            {
                return;
            }

            var current = head;
            while (current.Next != null)
            {
                if (current.Next != node)
                {
                    current = current.Next;
                    continue;
                }

                var temp = new LinkedListNode(value);
                var next = current.Next;
                current.Next = temp;
                temp.Next = next;
                Count++;
                break;
            }

        }

        public void AddFirst(int value)
        {
            var temp = head;
            var node = new LinkedListNode(value);

            head = node;
            node.Next = temp;
            Count++;
        }

        public void AddLast(int value)
        {
            var node = new LinkedListNode(value);
            var current = head;
            if (current == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = node;
                tail = node;
            }

            Count++;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            Count = 0;
        }

        public LinkedListNode Find(int value)
        {
            if (head == null)
            {
                return null;
            }

            var current = head;
            while (current != null)
            {
                if (current.Value == value)
                {
                    return current;
                }

                current = current.Next;
            }

            return null;
        }
       
        public void Remove(LinkedListNode node)
        {
            if (node == null)
            {
                return;
            }

            var current = head;
            while (current != null)
            {
                if (current.Next == node)
                {
                    current.Next = node.Next;
                    break;
                }

                current = current.Next;
            }

            Count--;
        }

        public void RemoveFirst()
        {
            if (head == null)
            {
                return;
            }

            head = head.Next;
            Count--;
        }

        public void RemoveLast()
        {
            if (head == null) 
            {
                return;
            }

            var current = head;
            while (current.Next.Next != null)
            {
                current = current.Next;
            }

            current.Next = null;
            tail = current;
            Count--;
        }

        public void Print()
        {
            var current = head;
            while (current != null)
            {
                Console.WriteLine(current.Value);
                current = current.Next;
            }

            Console.WriteLine();
        }
    }

    public class LinkedListNode
    {
        public LinkedListNode(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public LinkedListNode Next { get; set; }
    }
}
