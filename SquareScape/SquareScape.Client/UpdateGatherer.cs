using System.Net.Sockets;

namespace SquareScape.Client
{
    public class UpdateGatherer : IUpdateGatherer
    {
        private Socket _socket;
        private const int _BUFFER_SIZE = 8 * 1024; // TODO

        public UpdateGatherer()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // TODO - we just have to update this to dynamically send udp packets to each IP address / port of the client
        }

        public void BeginReceive()
        {
            while (true)
            {
                byte[] data = new byte[_BUFFER_SIZE];
                _socket.Receive(data, 0, data.Length, SocketFlags.None);
                // send data to service
            }

        }
    }
}
