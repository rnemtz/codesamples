using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercises.DataStructures
{
    public class Stack<T>: IEnumerable<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();

        public void Push(T item)
        {
            
        }

        public T Pop()
        {
            if (_list.Count== 0) throw new InvalidOperationException("The Stack is empty.");
            var value = _list.First.Value;
            _list.RemoveFirst();
            return value;
        }

        public T Peek()
        {
            if (_list.Count == 0) throw new InvalidOperationException("The Stack is empty.");
            return _list.First.Value;
        }

        public int Count => _list.Count;

        public void Clear()
        {
            _list.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
