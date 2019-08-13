using SquareScape.Shared.Commands;
using SquareScape.Shared.Updates;
using SquareScape.Server.Converters;
using SquareScape.Server.Queue;
using System;
using System.Collections.Generic;
using System.Timers;
using SquareScape.Server.Sockets;

namespace SquareScape.Server.Engine
{
    public class Engine : IEngine
    {
        private readonly IUpdateReceiver _reciever;
        private readonly IUpdateBroadcaster _broadcaster;
        private readonly IReceiverQueue<IGameUpdate> _queue;
        private readonly IServerGameState _serverGameState;
        private readonly ICommandDecoder _decoder;
        rivate readonly ICommandEncoder _encoder;

        private const int _GAMETICK = 100;
        private const int _BATCHSIZE = 200;

        public Engine(IUpdateReceiver reciever, IUpdateBroadcaster broadcaster, IReceiverQueue<IGameUpdate> queue,
            IServerGameState serverGameState, ICommandDecoder decoder, ICommandEncoder encoder)
        {
            _reciever = reciever;
            _broadcaster = broadcaster;
            _queue = queue;
            _gameStateOrchestrator = gameStateOrchestrator;
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
                // process any / all interactions here based off the current game state orchestrator
                // merge all game commands into 1 data packet string, including any additional interactions

                // hmm @Mike, what i'm thinking is:
                // we create an interface for a new 'required' game state orchestrator, but only for universal required properties eg player coordinates, players logged in etc.. 
                // we move the interface and the game state orchestrator to the shared project
                // we change the current DI for game state orchestrator
                // we rename the game state orchestrator to 'RequiredGameStateOrchestrator'
                // we then create 2 new classes, a ServerGameStateOrchestrator, and a ClientGameStateOrchestrator which both inherit the Required Orchestrator.

                // we then move both the encoder and decoder to the 'shared' projec so that both the client and server can serialize/deserialise ther game commands.
                // then once thats done, the for loop just below here will be able to create a serialised list of encoded game commands that we can just add to 1 giant string
                // that string 'gameState', can then be broadcast to all the clients

                // the client will then have to dependency inject the now shared 'decoder' into its own solution and then be able to decode the string of commands back to a list of game commands
                // each command will be stored into the client's game state
                // Note: the client's game state is just for the client to keep track of player coords etc for graphical rendering purposes 
                // it's not actually the source of truth (thats the server)
                // and then we 'render' each command on the client form depending on the client's game state update.

                // and that should be the login and position commands done. ?
                foreach (var playerCoordinate in _serverGameState.PlayerCoordinates)
                {
                    //gameState = SharedCommandParser?.AddCoordinate(playerCoordinate);
                    string encodedPlayerCoordinate = _encoder.Encode(playerCoordinate); // convert to IGameCommand first.
                    gamestate += "_" + encodedPlayerCoordinate;
                }
            }

            // broadcast the game state packet/s to all currently logged in clients
            _broadcaster.Broadcast(gameState);
        }
    }
}
