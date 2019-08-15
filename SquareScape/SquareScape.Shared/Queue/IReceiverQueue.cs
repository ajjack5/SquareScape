using System.Collections.Generic;

namespace SquareScape.Shared.Queue
{
    public interface IReceiverQueue<T> where T : class
    {
        int Size();
        void Push(T item);
        T Pull();
        IEnumerable<T> PullBatch(int count);
    }
}
