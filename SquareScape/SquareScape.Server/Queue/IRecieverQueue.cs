using System.Collections.Generic;

namespace SquareScape.Server.Queue
{
    public interface IRecieverQueue<T> where T : class
    {
        int Size();
        void Push(T item);
        T Pull();
        IEnumerable<T> PullBatch(int count);
    }
}
