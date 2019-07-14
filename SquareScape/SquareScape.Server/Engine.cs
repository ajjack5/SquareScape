using SquareScape.Common.Commands;
using SquareScape.Server.Queue;
using System.Collections.Generic;
using System.Timers;

namespace SquareScape.Server
{
    public class Engine
    {
        private readonly UpdateReciever _reciever;
        private readonly IRecieverQueue<IGameUpdate> _queue;
        private const int _GAMETICK = 200;
        private const int _BATCHSIZE = 200;
        private IList<string> _connectedClients;

        public Engine(UpdateReciever reciever, IRecieverQueue<IGameUpdate> queue)
        {
            _reciever = reciever;
            _queue = queue;
            _connectedClients = new List<string>();
        }

        public void Start()
        {
            _reciever.Listen();
            InitialiseTimer();
        }

        private async void UpdateImporterAsync(object source, ElapsedEventArgs e)
        {
            foreach (var item in _queue.PullBatch(_BATCHSIZE))
            {
                //TODO
                //Determine what update is for
                //Add new IP/Remove IP/Game Action
            }
        }

        private void InitialiseTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += UpdateImporterAsync;
            timer.Interval = _GAMETICK;
            timer.Enabled = true;
            timer.AutoReset = true;
        }
    }
}
