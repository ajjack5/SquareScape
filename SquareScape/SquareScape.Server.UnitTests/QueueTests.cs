using Shouldly;
using SquareScape.Common.Commands;
using SquareScape.Common.Updates;
using SquareScape.Server.Queue;
using System.Linq;
using Xunit;

namespace SquareScape.Server.UnitTests
{
    public class QueueTests
    {
        private IRecieverQueue<IGameUpdate> _queue;

        public QueueTests()
        {
            _queue = new RecieverQueue<IGameUpdate>();
        }

        [Fact]
        public void Queue_CanPush()
        {
            IGameUpdate o = new GameUpdate { IPAddress = "STRING", GameState = "SomeState" };
            _queue.Push(o);
            _queue.Size().ShouldBe(1);
        }

        [Fact]
        public void Queue_CanPull_RemovingPulledObjects()
        {
            IGameUpdate o1 = new GameUpdate { IPAddress = "STRING", GameState = "1" };
            IGameUpdate o2 = new GameUpdate { IPAddress = "STRING", GameState = "2" };

            _queue.Push(o1);
            _queue.Push(o2);

            var result = _queue.Pull();
            result.GameState.ShouldBe("1");

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
            IGameUpdate o1 = new GameUpdate { IPAddress = "STRING", GameState = "1" };
            IGameUpdate o2 = new GameUpdate { IPAddress = "STRING", GameState = "2" };
            IGameUpdate o3 = new GameUpdate { IPAddress = "STRING", GameState = "3" };

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
            IGameUpdate o1 = new GameUpdate { IPAddress = "STRING", GameState = "1" };
            IGameUpdate o2 = new GameUpdate { IPAddress = "STRING", GameState = "2" };
            IGameUpdate o3 = new GameUpdate { IPAddress = "STRING", GameState = "3" };

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
    }
}
