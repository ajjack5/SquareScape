using Shouldly;
using SquareScape.Server.Queue;
using System.Linq;
using Xunit;

namespace SquareScape.Server.UnitTests
{
    public class QueueTests
    {
        private IQueue<RandomObject> _queue;

        public QueueTests()
        {
            _queue = new Queue<RandomObject>();
        }

        [Fact]
        public void Queue_CanPush()
        {
            RandomObject o = new RandomObject { Name = "something1" };
            _queue.Push(o);
            _queue.Size().ShouldBe(1);
        }

        [Fact]
        public void Queue_CanPull_RemovingPulledObjects()
        {
            RandomObject o1 = new RandomObject { Name = "1" };
            RandomObject o2 = new RandomObject { Name = "2" };

            _queue.Push(o1);
            _queue.Push(o2);

            var result = _queue.Pull();
            result.Name.ShouldBe("1");

            _queue.Size().ShouldBe(1);
        }

        [Fact]
        public void Queue_CanPull_ReturningNullWhenQueueIsEmpty()
        {
            var result = _queue.Pull();
            result.ShouldBeNull();
        }

        [Fact]
        public void Queue_CanPullBatch_RemovingPulledObjects()
        {
            RandomObject o1 = new RandomObject { Name = "1" };
            RandomObject o2 = new RandomObject { Name = "2" };
            RandomObject o3 = new RandomObject { Name = "3" };

            _queue.Push(o1);
            _queue.Push(o2);
            _queue.Push(o3);
            _queue.Size().ShouldBe(3);

            var result = _queue.PullBatch(2);

            result.Count().ShouldBe(2);
            _queue.Size().ShouldBe(1);
        }

        [Fact]
        public void Queue_CanPullBatch_WhenPullingMoreObjectsThanThereExists()
        {
            RandomObject o1 = new RandomObject { Name = "1" };
            RandomObject o2 = new RandomObject { Name = "2" };
            RandomObject o3 = new RandomObject { Name = "3" };

            _queue.Push(o1);
            _queue.Push(o2);
            _queue.Push(o3);
            _queue.Size().ShouldBe(3);

            var result = _queue.PullBatch(5);

            result.Count().ShouldBe(3);
            _queue.Size().ShouldBe(0);
        }

        [Fact]
        public void Queue_CanPullBatch_ReturningEmptyListWhenQueueIsEmpty()
        {
            var result = _queue.PullBatch(5000);
            result.ShouldNotBeNull();
            result.Count().ShouldBe(0);
        }

        private class RandomObject
        {
            public string Name { get; set; }
        }
    }
}
