using SquareScape.Common.Commands;
using SquareScape.Common.Converters;
using SquareScape.Common.Updates;
using SquareScape.Server.Queue;
using System;
using System.Collections.Generic;
using System.Timers;

namespace SquareScape.Server
{
    public class Engine
    {
        private readonly UpdateReciever _reciever;
        private readonly IRecieverQueue<IGameUpdate> _queue;

        private const int _GAMETICK = 100;
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

        private void InitialiseTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += UpdateImporter;
            timer.Interval = _GAMETICK;
            timer.Enabled = true;
            timer.AutoReset = true;
        }

        private void UpdateImporter(object source, ElapsedEventArgs e)
        {
            Console.Out.WriteLine($"Attempting to update from a list containing {_queue.Size()} updates.");

            IEnumerable<IGameUpdate> gameUpdates = _queue.PullBatch(_BATCHSIZE);
            IList<IGameCommand> gameCommands = new List<IGameCommand>();

            foreach (var gameUpdate in gameUpdates)
            {
                IGameCommand command = gameUpdate.ParseCommand();
                gameCommands.Add(command);
            }

            // im wondering if we even need a converter / deconverter ?
            // we could just push all commands into a list and concat the list together
            // then push a giant string ther each client to ionterpret individually ?
        }
    }
}
