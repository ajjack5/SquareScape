using System.Collections.Generic;

namespace SquareScape.Server.Queue
{
    public interface IQueue<T> where T : class
    {
        int Size();
        void Push(T item);
        T Pull();
        IEnumerable<T> PullBatch(int count);
    }
}
