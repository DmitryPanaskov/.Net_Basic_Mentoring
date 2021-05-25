using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Tasks
{
    public class CustomEnumerator<T> : IEnumerator<T>
    {
        private ParentEnumerable<T> _list;
        private Node<T> _currentNode;
        private bool _disposedValue;
        private int _iterator = -1;

        public CustomEnumerator(ParentEnumerable<T> list, Node<T> root)
        {
            _list = list;
            _currentNode = root;
        }

        object IEnumerator.Current => Current;

        public T Current
        {
            get
            {
                var returnNode = _currentNode;
                _currentNode = _currentNode.NextNode;

                return returnNode.Data;
            }
        }

        public bool MoveNext()
        {
            _iterator++;
            return _iterator < _list.Length;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }


        public void Dispose()
        {
            Dispose(disposing: true);
            System.GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
            {
                return;
            }

            if (disposing)
            {
                _list = null;
                _currentNode = null;
            }

            _disposedValue = true;
        }
    }
}