using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private int _length = 0;
        private readonly Node<T> _node;

        public DoublyLinkedList()
        {
            _node = new Node<T>(default(T));
            _node.PreviousNode = _node;
            _node.NextNode = _node;
        }

        public int Length => _length;

        public void Add(T item)
        {
            var newNode = new Node<T>(item)
            {
                NextNode = _node,
                PreviousNode = _node.PreviousNode
            };

            _node.PreviousNode.NextNode = newNode;
            _node.PreviousNode = newNode;

            _length++;
        }

        public void AddAt(int index, T item)
        {
            if (index > _length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var newNode = new Node<T>(item);
            var node = GetNodeByIndex(index);

            newNode.NextNode = node;
            newNode.PreviousNode = node.PreviousNode;

            node.PreviousNode.NextNode = newNode;
            node.PreviousNode = newNode;

            _length++;
        }

        public T ElementAt(int index)
        {
            if (index > _length - 1 || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            return GetNodeByIndex(index).Data;
        }

        public void Remove(T item)
        {
            var node = _node.NextNode;
            var iteration = 0;

            while (!node.Data.Equals(item) && iteration != _length - 1)
            {
                iteration++;
                node = node.NextNode;
            }

            if (node.Data.Equals(item))
            {
                node.PreviousNode.NextNode = node.NextNode;
                node.NextNode.PreviousNode = node.PreviousNode;

                _length--;
            }
        }

        public T RemoveAt(int index)
        {
            if (_length == 0 || index > _length - 1 || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var node = GetNodeByIndex(index);

            node.PreviousNode.NextNode = node.NextNode;
            node.NextNode.PreviousNode = node.PreviousNode;

            _length--;

            return node.Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CustomEnumerator<T>(_node, _length);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node<T> GetNodeByIndex(int index)
        {
            int iteration = 0;
            var currentNode = _node.NextNode;

            if (_length / 2 >= index)
            {
                while (iteration != index)
                {
                    iteration++;
                    currentNode = currentNode.NextNode;
                }

                return currentNode;
            }
            else
            {
                currentNode = _node;
                while (iteration != _length - index)
                {
                    iteration++;
                    currentNode = _node.PreviousNode;
                }

                return currentNode;
            }
        }
    }
}
