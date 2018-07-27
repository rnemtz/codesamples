using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeExercises.LinkedLists;

namespace CodeExercises.DataStructures
{
    public class Stack<T> : IEnumerable<T>
    {
        private LinkedList<T> _list = new LinkedList<T>();

        public void Push(T item)
        {
        }

        public T Pop()
        {
            if (_list.Count == 0) throw new InvalidOperationException("The Stack is empty.");
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

namespace CodeExercises.Stacks
{
    public class StackLinkedList<T> : IEnumerable where T : IComparable
    {
        public bool IsEmpty { get; set; }
        public int Count { get; set; }

        //LinkedList Implementation
        private LinkedList<T> _stackList;

        public StackLinkedList()
        {
            InitializeStack();
        }

        private void SetValues()
        {
            Count = _stackList.Count;
            IsEmpty = Count == 0;
        }

        private void InitializeStack()
        {
            IsEmpty = false;
            Count = 0;
            _stackList = new LinkedList<T>();
        }

        public void Push(T value)
        {
            _stackList.AddLast(value);
            SetValues();
        }

        public T Pop()
        {
            if (IsEmpty) throw new Exception("The Stack is Empty");

            var item = _stackList.Last();
            _stackList.RemoveLast();
            SetValues();
            return item;
        }

        public T Peek()
        {
            if (IsEmpty) throw new Exception("The Stack is Empty");
            return _stackList.Last();
        }

        public bool Contains(T value)
        {
            return _stackList.Contains(value);
        }

        public IEnumerator GetEnumerator()
        {
            return _stackList.GetEnumerator();
        }
    }

    public class StackArray<T> : IEnumerable where T : IComparable
    {
        public bool IsEmpty { get; set; }
        public int Count { get; set; }

        //Array Implementation
        private T[] _stackList;
        private int _currentSize;
        private int _lastIndex;

        public StackArray()
        {
            InitializeStack();
        }

        private void SetValues()
        {
            
            IsEmpty = Count == 0;
        }

        private void InitializeStack()
        {
            IsEmpty = false;
            Count = 0;
            _currentSize = 5;
            _lastIndex = 0;
            _stackList = new T[_currentSize];
            _stackList.Initialize();
        }

        //Automatically increase by double if needed
        private void DoubleArraySize()
        {
            var newSize = _currentSize * 2;
            var newArray = new T[newSize];
            newArray.Initialize();

            //Copy the current array items
            for (var i = 0; i < _lastIndex; i++) newArray[i] = _stackList[i];
            _stackList = newArray;
            _currentSize = _stackList.Length;

        }

        //Automatically reduce the size if needed
        private void HalveArraySize()
        {
            var newSize = _currentSize / 2;
            var newArray = new T[newSize];
            newArray.Initialize();

            //Copy the current array items
            for (var i = 0; i <= _lastIndex; i++) newArray[i] = _stackList[i];
            _stackList = newArray;
            _currentSize = _stackList.Length;
        }

        public void Push(T value)
        {
            if (_lastIndex == _currentSize) DoubleArraySize();
            _stackList[_lastIndex++] = value;
            Count++;
            SetValues();
        }

        public T Pop()
        {
            if (IsEmpty) throw new Exception("The Stack is Empty");

            var item = _stackList[_lastIndex-1];
            _stackList[_lastIndex---1] = default(T);

            if (_lastIndex < _currentSize / 2) HalveArraySize();

            Count--;
            SetValues();
            return item;
        }

        public T Peek()
        {
            if (IsEmpty) throw new Exception("The Stack is Empty");
            return _stackList[_lastIndex];
        }

        public bool Contains(T value)
        {
            return _stackList.Contains(value);
        }

        public IEnumerator GetEnumerator()
        {
            return _stackList.GetEnumerator();
        }
    }
}