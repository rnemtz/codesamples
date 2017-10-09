using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeExercises.DataStructures
{
    public class Queue<T> : IEnumerable<T>
    {
        public LinkedList<T> Items = new LinkedList<T>();

        public int Count => Items.Count;

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enqueue(T item)
        {
            Items.AddLast(item);
        }

        public T Dequeue()
        {
            if (Items.Count == 0) throw new InvalidOperationException("The queue is empty");

            var value = Items.First.Value;
            Items.RemoveFirst();
            return value;
        }

        public T Peek()
        {
            if (Items.Count == 0) throw new InvalidOperationException("The queue is empty");
            return Items.First.Value;
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}