using System.Net.Sockets;

namespace SquareScape.Client.Sockets
{
    public interface IUpdateSender
    {
        TcpClient TcpClient { get; set; }

        void Send(string encodedGameCommand);
    }
}
