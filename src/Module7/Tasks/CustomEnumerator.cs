using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks
{
    public class CustomEnumerator<T> : IEnumerator<T>
    {
        private readonly Node<T> _node;
        private readonly int _length;
        private Node<T> _currentNode;
        private int _iterator = -1;

        public CustomEnumerator(Node<T> node, int length)
        {
            _node = node;
            _currentNode = node;
            _length = length;
        }

        public T Current
        {
            get
            {
                if (_iterator == -1 || _iterator >= _length)
                {
                    throw new InvalidOperationException();
                }

                return _currentNode.Data;
            }
        }

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_iterator < _length - 1)
            {
                _iterator++;
                _currentNode = _currentNode.NextNode;

                return true;
            }

            return false;
        }

        public void Reset()
        {
            _currentNode = _node;
        }

        public void Dispose()
        {
        }
    }
}
