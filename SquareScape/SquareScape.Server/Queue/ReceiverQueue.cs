using System.Collections.Generic;
using System.Linq;

namespace SquareScape.Server.Queue
{
    public class ReceiverQueue<T> : IReceiverQueue<T> where T : class
    {
        private IList<T> _queue;

        public ReceiverQueue()
        {
            _queue = new List<T>();
        }

        public T Pull()
        {
            T item = _queue.LastOrDefault();

            if (item == null)
            {
                return null;
            }

            _queue.RemoveAt(_queue.Count - 1);
            return item;
        }

        public IEnumerable<T> PullBatch(int count)
        {
            IList<T> items = new List<T>();

            for (int i = 0; i < count; i++)
            {
                T item = _queue.LastOrDefault();

                if (item == null)
                {
                    break;
                }

                items.Add(item);
                _queue.RemoveAt(_queue.Count - 1);
            }

            return items.AsEnumerable();
        }

        public void Push(T item)
        {
            _queue.Insert(0, item);
        }

        public int Size()
        {
            return _queue.Count;
        }
    }
}
