using SquareScape.Shared.Commands;
using SquareScape.Shared.Updates;
using SquareScape.Server.Queue;
using System;
using System.Collections.Generic;
using System.Timers;
using SquareScape.Server.Sockets;
using SquareScape.Shared.Converters;
using SquareScape.Shared.Enums;

namespace SquareScape.Server.Engine
{
    public class Engine : IEngine
    {
        private readonly IUpdateReceiver _reciever;
        private readonly IUpdateBroadcaster _broadcaster;
        private readonly IReceiverQueue<IGameUpdate> _queue;
        private readonly IServerGameState _serverGameState;
        private readonly ICommandDecoder _decoder;
        private readonly ICommandEncoder _encoder;

        private const int _GAMETICK = 100;
        private const int _BATCHSIZE = 200;

        public Engine(IUpdateReceiver reciever, IUpdateBroadcaster broadcaster, IReceiverQueue<IGameUpdate> queue,
            IServerGameState serverGameState, ICommandDecoder decoder, ICommandEncoder encoder)
        {
            _reciever = reciever;
            _broadcaster = broadcaster;
            _queue = queue;
            _serverGameState = serverGameState;
            _decoder = decoder;
            _encoder = encoder;
        }

        public void Start()
        {
            _reciever.Listen();
            InitialiseGameTickTimer();
        }

        private void InitialiseGameTickTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += ProcessGameTick;
            timer.Interval = _GAMETICK;
            timer.Enabled = true;
            timer.AutoReset = true;
        }

        private void ProcessGameTick(object source, ElapsedEventArgs e)
        {
            Console.Out.WriteLine($"Attempting to update from a list containing {_queue.Size()} updates.");

            IEnumerable<IGameUpdate> gameUpdates = _queue.PullBatch(_BATCHSIZE);
            IList<IGameCommand> gameCommands = new List<IGameCommand>();

            foreach (var gameUpdate in gameUpdates)
            {
                gameCommands.Add(_decoder.Decode(gameUpdate));
            }

            string gameState = "";

            foreach (var gameCommand in gameCommands)
            {
                // TODO process current game state interactions and add IGameCommands in here before taking the current state.... TODO in some kind of previous function.

                // after login works, we do player movement, then refactor the shit out of everything we've done to make it as efficient as possible
                // then stress the the shit out of it etc until we agree the square movement is working well and distributed.
                foreach (var playerLoggedIn in _serverGameState.PlayersLoggedIn)
                {
                    IGameCommand command = new GameCommand() // TODO move to an extension method or extend the service to do this
                    {
                        Command = GameCommands.Login,
                        Data = new Tuple<Guid, object>(playerLoggedIn.Key, playerLoggedIn.Value)
                    };

                    string encodedResult = _encoder.Encode(command);
                    gameState += "_" + encodedResult;
                }

                // and that should be the login and position commands done. ?
                foreach (var playerCoordinate in _serverGameState.PlayerCoordinates)
                {
                    IGameCommand command = new GameCommand()
                    {
                        Command = GameCommands.Position,
                        Data = new Tuple<Guid, object>(playerCoordinate.Key, playerCoordinate.Value)
                    };

                    string encodedResult = _encoder.Encode(command);
                    gameState += "_" + encodedResult;
                }
            }

            _broadcaster.Broadcast(gameState);
        }
    }
}
