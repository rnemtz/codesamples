using System;

namespace CodeExercises.Internal
{
    public class Queue
    {
        public StackNode head;

        public void Add(int value)
        {
            var node = new StackNode(value);
            if (head == null)
            {
                head = node;
            }
            else
            {
                var current = head;
                while (current.Node != null)
                {
                    current = current.Node;
                }

                current.Node = node;
            }
        }

        public int Remove()
        {
            if (IsEmpty()) throw new Exception("Queue is empty");

            var value = head.Value;
            head = head.Node;

            return value;
        }

        public int Peek()
        {
            if (IsEmpty()) throw new Exception("Queue is empty");

            return head.Value;
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }
}
