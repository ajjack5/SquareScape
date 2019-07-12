using SquareScape.Commands.Commands;
using SquareScape.Server.Queue;

namespace SquareScape.Server
{
    public class Engine
    {
        private static RecieverQueue<IGameUpdate> _queue;
        private UpdateReciever reciever;
        
        public Engine()
        {
            _queue = new RecieverQueue<IGameUpdate>();
            reciever = new UpdateReciever(_queue);
        }

        public void Start()
        {
            reciever.Listen();
        }
    }
}
