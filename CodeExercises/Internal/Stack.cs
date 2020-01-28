using System;

namespace CodeExercises.Internal
{
    public class Stack
    {
        private StackNode head;

        public int Pop()
        {
            if (head == null)
            {
                throw new Exception("Stack is empty");
            }

            var value = head.Value;
            head = head.Node;

            return value;
        }

        public void Push(int value)
        {
            var node = new StackNode(value);

            node.Node = head;
            head = node;
        }

        public int Peek()
        {
            if (head == null)
            {
                throw new Exception("Stack is empty");
            }
            return head.Value;
        }

        public bool IsEmpty()
        {
            return head == null;
        }
    }

    public class StackNode
    {
        public int Value { get; set; }
        public StackNode Node { get; set; }

        public StackNode(int value)
        {
            Value = value;
        }
    }
}
