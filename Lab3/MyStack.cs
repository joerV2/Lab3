using ProjectVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class MyStack<T>
    {
        private ArrayDecorator<T> _arrayStack;
        private MyLinkedList<T> _linkedListStack;
        private bool _useArray;
        private int _count;

        public MyStack(bool useArray, int capacity = 10)
        {
            _useArray = useArray;
            _count = 0;

            if (useArray)
            {
                _arrayStack = new ArrayDecorator<T>(new T[capacity]);
            }
            else
            {
                _linkedListStack = new MyLinkedList<T>();
            }
        }

        public int Count => _count;

        public void Push(T item)
        {
            if (_useArray)
            {
                if (_count >= _arrayStack.Length)
                    throw new InvalidOperationException("Stack overflow");
                _arrayStack[_count] = item;
            }
            else
            {
                _linkedListStack.Add(item);
            }
            _count++;
        }

        public T Pop()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty");

            T item;
            if (_useArray)
            {
                item = _arrayStack[_count - 1];
                _count--;
            }
            else
            {
                item = _linkedListStack.GetLast();
                _linkedListStack.RemoveLast();
            }

            return item;
        }

        public T Peek()
        {
            if (_count == 0)
                throw new InvalidOperationException("Stack is empty");

            return _useArray ? _arrayStack[_count - 1] : _linkedListStack.GetLast();
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }
    }
}
