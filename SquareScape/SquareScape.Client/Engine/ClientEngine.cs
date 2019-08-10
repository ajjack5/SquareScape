using SquareScape.Client.Converters;
using SquareScape.Client.Sockets;
using SquareScape.Shared.Commands;
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

        public ClientEngine(IUpdateGatherer updateGatherer, IUpdateSender updateSender, ICommandEncoder commandEncoder)
        {
            _updateGatherer = updateGatherer;
            _updateSender = updateSender;
            _commandEncoder = commandEncoder;

            _gathererThread = CreateUpdateGathererThread();
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

        public void SendGameCommand(IGameCommand gameCommand)
        {
            string encodedGameCommand = _commandEncoder.Encode(gameCommand);
            _updateSender.Send(encodedGameCommand);
        }
    }
}
