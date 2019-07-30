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

        private Thread _senderThread;
        private Thread _gathererThread;

        public ClientEngine()
        {
            _updateGatherer = new UpdateGatherer();
            _updateSender = new UpdateSender();
            _commandEncoder = new CommandEncoder();

            _senderThread = UpdateSender();
            _gathererThread = UpdateGatherer();
        }

        public void Start()
        {
            _senderThread.Start();
            _gathererThread.Start();
        }

        private Thread UpdateSender()
        {
            TcpClient client = new TcpClient(HOSTNAME, PORT);

            Thread clientThread = new Thread(() =>
            {
                _updateSender.BeginSending(client);
            });

            return clientThread;
        }

        private Thread UpdateGatherer()
        {
            Thread clientThread = new Thread(() =>
            {
                _updateGatherer.BeginReceive();
            });

            return clientThread;
        }

        public void SendGameCommand(IGameCommand gameCommand)
        {
            string encodedGameCommand = _commandEncoder.Encode(gameCommand);
            _updateSender.EncodedGameCommand = encodedGameCommand;
        }
    }
}
