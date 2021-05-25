namespace Tasks
{
    public class Node<T>
    {
        public Node(T data, Node<T> previousNode = null, Node<T> nextNode = null)
        {
            Data = data;
            NextNode = nextNode;
            PreviousNode = previousNode;
        }

        public T Data { get; set; }

        public Node<T> NextNode { get; set; }

        public Node<T> PreviousNode { get; set; }
    }
}
