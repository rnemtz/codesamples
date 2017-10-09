namespace CodeExercises.DataStructures
{
    public class DoubleLinkedList<T>
    {
        public int Count { get; set; }
        public bool IsReadOnly => false;
        private DoubleLinkedListNode<T> Head { get; set; }
        private DoubleLinkedListNode<T> Tail { get; set; }

        public void AddFirst(DoubleLinkedListNode<T> node)
        {
            var temp = Head;
            Head = node;
            Head.Next = temp;
            if (Count == 1)
                Tail = Head;
            else
            {
                temp.Previous = Head;
            }
            Count++;

        }

        public void AddLast(DoubleLinkedListNode<T> node)
        {
            if (Count == 0)
                Head = node;
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
            }
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
                Tail.Previous.Next = null;
                Tail = Tail.Previous;
            }
            Count--;
        }

        public void RemoveFirst()
        {
            if (Count == 0) return;
            Head = Head.Next;
            Count--;
            if (Count == 0)
            {
                Tail = null;
            }
            else
            {
                Head.Previous = null;
            }
        }
    }

    public class DoubleLinkedListNode<T>
    {
        public DoubleLinkedListNode(T value)
        {
            Value = value;
        }

        public DoubleLinkedListNode<T> Next { get; set; }
        public DoubleLinkedListNode<T> Previous { get; set; }
        public T Value { get; set; }
    }
}