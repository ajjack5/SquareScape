using System.Net.Sockets;
using System.Threading;

namespace SquareScape.Client
{
    public class ClientEngine : IClientEngine
    {
        public const int PORT = 20000;
        public const string HOSTNAME = "127.0.0.1";

        private IUpdateGatherer _updateGatherer;
        private IUpdateSender _updateSender;

        private Thread _senderThread;
        private Thread _gathererThread;

        public ClientEngine()
        {
            _updateGatherer = new UpdateGatherer();
            _updateSender = new UpdateSender();

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
    }
}
