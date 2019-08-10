using System;
using System.Net.Sockets;
using System.Text;

namespace SquareScape.Server.Sockets
{
    public class UpdateBroadcaster : IUpdateBroadcaster
    {
        private Socket _socket;
        private const int _BUFFER_SIZE = 8 * 1024;
        private BufferState _bufferState;

        public UpdateBroadcaster()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _bufferState = new BufferState();
        }

        public void Broadcast(string gameState)
        {
            // we just have to update this to dynamically send udp packets to each IP address / port of the client
            return;
            throw new NotImplementedException();

            byte[] data = Encoding.ASCII.GetBytes(gameState);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (asyncResult) =>
            {
                BufferState bufferState = (BufferState)asyncResult.AsyncState;
                int bytes = _socket.EndSend(asyncResult);
            }, _bufferState);
        }

        internal class BufferState {
            public byte[] buffer = new byte[_BUFFER_SIZE];
        }
    }
}
