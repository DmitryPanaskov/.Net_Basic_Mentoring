using System;
using Tasks.DoNotChange;

namespace Tasks
{
    public class HybridFlowProcessor<T> : ParentEnumerable<T>, IHybridFlowProcessor<T>
    {
        public T Dequeue()
        {
            CheckNull(Length);

            var needItem = Root;
            RemoveNode(needItem);

            return needItem.Data;
        }

        public void Enqueue(T item) => Push(item);

        public T Pop()
        {
            CheckNull(Length);

            var needItem = Head;
            RemoveNode(needItem);

            return needItem.Data;
        }
    }
}
