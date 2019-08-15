using SquareScape.Shared.Queue;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SquareScape.Client.Sockets
{
    public class UpdateGatherer : IUpdateGatherer
    {
        private readonly IReceiverQueue<string> _queue;

        private UdpClient _receivingUdpClient = new UdpClient(20001);
        private IPEndPoint _serverEndpoint;

        public UpdateGatherer(IReceiverQueue<string> queue)
        {
            _queue = queue;
            _serverEndpoint = new IPEndPoint(IPAddress.Any, 0); // TODO change port / ip to a global config
        }

        // we can 'netstat -an' to see all taken local ports
        public void BeginReceiving()
        {
            while (true)
            {
                // Blocks until a message returns on this socket from a remote host.
                byte[] receiveBytes = _receivingUdpClient.Receive(ref _serverEndpoint);
                string gameState = Encoding.ASCII.GetString(receiveBytes);
                _queue.Push(gameState);
            }
        }
    }
}
