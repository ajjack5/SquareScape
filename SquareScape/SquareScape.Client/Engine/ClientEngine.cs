using SquareScape.Client.Sockets;
using SquareScape.Shared.Commands;
using SquareScape.Shared.Converters;
using System.Net.Sockets;
using System.Threading;

namespace SquareScape.Client.Engine
{
    public class ClientEngine : IClientEngine
    {
        private const int _PORT = 20000;
        private const string _HOSTNAME = "127.0.0.1";
        private const int _BATCH_NUMBER = 100;

        private readonly IUpdateGatherer _updateGatherer;
        private readonly IUpdateSender _updateSender;
        private readonly ICommandEncoder _commandEncoder;
        private readonly IReceiverQueue _queue;

        private Thread _gathererThread;
        private Thread _processorThread;

        public ClientEngine(IUpdateGatherer updateGatherer, IUpdateSender updateSender, ICommandEncoder commandEncoder,
            IReceiverQueue queue)
        {
            _updateGatherer = updateGatherer;
            _updateSender = updateSender;
            _commandEncoder = commandEncoder;
            _queue = queue;

            _gathererThread = CreateUpdateGathererThread();
            _processorThread = CreateProcessorThread();
        }

        public void Start()
        {
            _gathererThread.Start();
            _processorThread.Start();
            _updateSender.TcpClient = new TcpClient(_HOSTNAME, _PORT);
        }

        private Thread CreateUpdateGathererThread()
        {
            Thread clientThread = new Thread(() =>
            {
                _updateGatherer.BeginReceiving();
            });

            return clientThread;
        }

        private Thread CreateProcessorThread()
        {
            Thread processorThread = new Thread(() => 
            {
                while (true)
                {
                    // while true
                        // pull items off the injected concurrent queue
                        // determine what action is required
                        // perhaps DI the client back into the engine here
                            // then call the relevant client function to process the graphic etc..
                    IEnumerable<string> gameUpdates? = _queue.PullBatch(_BATCH_NUMBER);
                    foreach (var gameUpdate? in gameUpdates)
                    {
                        update.saveInGameState(); ?
                        Action? action = update.determineAction(); ?
                        _client.ProcessClientAction(action);
                    }
                }
            });

            return processorThread;
        }

        public void SendGameCommand(IGameCommand gameCommand)
        {
            string encodedGameCommand = _commandEncoder.Encode(gameCommand);
            _updateSender.Send(encodedGameCommand);
        }
    }
}
