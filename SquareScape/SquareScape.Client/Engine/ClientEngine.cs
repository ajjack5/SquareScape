using SquareScape.Client.Sockets;
using SquareScape.Shared.Commands;
using SquareScape.Shared.Converters;
using SquareScape.Shared.Queue;
using System.Net.Sockets;
using System.Threading;

namespace SquareScape.Client.Engine
{
    public class ClientEngine : IClientEngine
    {
        private const int _PORT = 20000;
        private const string _HOSTNAME = "127.0.0.1";

        private readonly IUpdateGatherer _updateGatherer;
        private readonly IUpdateSender _updateSender;
        private readonly ICommandEncoder _commandEncoder;
        private readonly ICommandDecoder _commandDecoder;
        private readonly IReceiverQueue<string> _queue;

        private Thread _gathererThread;
        private Thread _processorThread;

        public ClientEngine(IUpdateGatherer updateGatherer, IUpdateSender updateSender, ICommandEncoder commandEncoder,
            ICommandDecoder commandDecoder, IReceiverQueue<string> queue)
        {
            _updateGatherer = updateGatherer;
            _updateSender = updateSender;
            _commandEncoder = commandEncoder;
            _commandDecoder = commandDecoder;
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
                    string gameState = _queue.Pull();
                    if(gameState != null)
                    {
                        string[] gameCommands = gameState.Split("_");

                        foreach(var command in gameCommands)
                        {
                            if(command != null)
                            {
                                IGameCommand decodedCommand = _commandDecoder.Decode(command);
                            }
                        }
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
