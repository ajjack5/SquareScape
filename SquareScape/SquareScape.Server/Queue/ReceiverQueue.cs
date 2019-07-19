using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SquareScape.Server.Queue
{
    public class ReceiverQueue<T> : IReceiverQueue<T> where T : class
    {
        private ConcurrentQueue<T> _queue;

        public ReceiverQueue()
        {
            _queue = new ConcurrentQueue<T>();
        }

        public T Pull()
        {
            T item = null;
            if (!_queue.TryDequeue(out item))
            {
                return null;
            }

            return item;
        }

        public IEnumerable<T> PullBatch(int count)
        {
            IList<T> items = new List<T>();

            for (int i = 0; i < count; i++)
            {
                T item = null;
                if (!_queue.TryDequeue(out item))
                {
                    break;
                }

                if (item == null)
                {
                    break;
                }

                items.Add(item);
            }

            return items.AsEnumerable();
        }

        public void Push(T item)
        {
            _queue.Enqueue(item);
        }

        public int Size()
        {
            return _queue.Count;
        }
    }
}
