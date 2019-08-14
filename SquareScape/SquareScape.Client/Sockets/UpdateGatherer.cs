using System;
using System.Net;
using System.Net.Sockets;

namespace SquareScape.Client.Sockets
{
    public class UpdateGatherer : IUpdateGatherer
    {
        private readonly IReceiverQueue _queue;

        private const int _BUFFER_SIZE = 8 * 1024; // TODO
        private UdpClient _receivingUdpClient = new UdpClient(20001);
        private IPEndPoint _serverEndpoint;

        public UpdateGatherer(IReceiverQueue queue)
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
                _queue.push(ASCII.GetString(receiveBytes));
            }
        }
    }
}
