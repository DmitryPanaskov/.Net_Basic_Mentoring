using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : ParentEnumerable<T>, IDoublyLinkedList<T>
    {
        public void Add(T item) => Push(item);

        public void AddAt(int index, T item)
        {
            if (index == Length)
            {
                Push(item);
                return;
            }

            var currentNode = FindNodeByIndex(index);
            var newNode = new Node<T>(item, currentNode.PreviousNode, currentNode);

            currentNode.PreviousNode = newNode;
            Length++;
        }

        public T ElementAt(int index)
        {
            var needNode = FindNodeByIndex(index);
            return needNode.Data;
        }

        public void Remove(T item)
        {
            ValidateIfListEmpty();

            var currentItem = Root;
            var counter = default(int);

            while (!currentItem.Data.Equals(item))
            {
                currentItem = currentItem.NextNode;
                counter++;

                if (counter == Length)
                {
                    return;
                }
            }

            if (currentItem.Data.Equals(item))
            {
                RemoveNode(currentItem);
            }
        }

        public T RemoveAt(int index)
        {
            var needNode = FindNodeByIndex(index);

            RemoveNode(needNode);

            return needNode.Data;
        }
    }
}
