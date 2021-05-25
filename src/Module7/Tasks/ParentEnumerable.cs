using System;
using System.Collections;
using System.Collections.Generic;

namespace Tasks
{
    public class ParentEnumerable<T> : IEnumerable<T>
    {
        protected const int SecondIndex = 1;

        public int Length { get; set; }

        protected int LastIndex => Length - 1;

        protected Node<T> Root { get; set; }

        protected Node<T> Head { get; set; }

        public void Push(T item)
        {
            if (Length <= SecondIndex)
            {
                CreateStructureWhenPushFirstItems(item);
            }
            else
            {
                var newNode = new Node<T>(item, Head);
                Head.NextNode = newNode;
                Head = newNode;
            }

            Length++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CustomEnumerator<T>(this, Root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected void RemoveNode(Node<T> node)
        {
            CheckNull(node);

            if (LastIndex == default)
            {
                Root = null;
                Head = null;
                Length--;

                return;
            }

            if (LastIndex == SecondIndex)
            {
                if (Head.Equals(node))
                {
                    Root.NextNode = null;
                    Head = Root;
                }
                else
                {
                    Head.PreviousNode = null;
                    Root = Head;
                }

                Length--;

                return;
            }

            if (node.PreviousNode is null)
            {
                Root = node.NextNode;
                Root.PreviousNode = null;
                Length--;

                return;
            }

            if (node.NextNode is null)
            {
                Head = node.PreviousNode;
                Head.NextNode = null;
            }
            else
            {
                node.NextNode.PreviousNode = node.PreviousNode;
                node.PreviousNode.NextNode = node.NextNode;
            }

            Length--;
        }

        protected Node<T> FindNodeByIndex(int index)
        {
            ValidateIndex(index, LastIndex);

            Node<T> needNode;
            int counter;

            if (IndexCloseToHead(index))
            {
                needNode = Head;
                counter = LastIndex;

                while (counter != index)
                {
                    needNode = needNode.PreviousNode;
                    counter--;
                }
            }
            else
            {
                needNode = Root;
                counter = default;

                while (counter != index)
                {
                    needNode = needNode.NextNode;
                    counter++;
                }
            }

            return needNode;
        }

        protected void ValidateIndex(int index, int length)
        {
            ValidateIfListEmpty();

            if (index > length || index < default(int))
            {
                throw new IndexOutOfRangeException();
            }
        }

        protected void ValidateIfListEmpty()
        {
            if (Head is null && Root is null)
            {
                throw new IndexOutOfRangeException();
            }
        }

        protected void CheckNull<T>(T t)
        {
            if (EqualityComparer<T>.Default.Equals(t, default(T)))
            {
                throw new InvalidOperationException();
            }
        }

        private void CreateStructureWhenPushFirstItems(T item)
        {
            if (Length == default)
            {
                var newNode = new Node<T>(item);
                Root = newNode;
                Head = newNode;

                return;
            }

            if (Length == SecondIndex)
            {
                var newNode = new Node<T>(item, Root);
                Head = newNode;
                Root.NextNode = Head;
            }
        }

        private bool IndexCloseToHead(int index)
        {
            return Math.Abs(index - Length) >= Math.Abs(index - default(int));
        }
    }
}
