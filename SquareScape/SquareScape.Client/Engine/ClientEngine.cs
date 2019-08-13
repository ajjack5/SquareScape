using SquareScape.Client.Sockets;
using SquareScape.Shared.Commands;
using SquareScape.Shared.Converters;
using System.Net.Sockets;
using System.Threading;

namespace SquareScape.Client.Engine
{
    public class ClientEngine : IClientEngine
    {
        public const int PORT = 20000;
        public const string HOSTNAME = "127.0.0.1";

        private IUpdateGatherer _updateGatherer;
        private IUpdateSender _updateSender;
        private ICommandEncoder _commandEncoder;

        private Thread _gathererThread;
        private Thread _processorThread;

        public ClientEngine(IUpdateGatherer updateGatherer, IUpdateSender updateSender, ICommandEncoder commandEncoder)
        {
            _updateGatherer = updateGatherer;
            _updateSender = updateSender;
            _commandEncoder = commandEncoder;

            _gathererThread = CreateUpdateGathererThread();
            _processorThread = CreateProcessorThread();
        }

        public void Start()
        {
            _gathererThread.Start();
            _updateSender.TcpClient = new TcpClient(HOSTNAME, PORT);
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
                // while true
                    // pull items off the injected concurrent queue
                    // determine what action is required
                    // perhaps DI the client back into the engine here
                        // then call the relevant client function to process the graphic etc..
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
