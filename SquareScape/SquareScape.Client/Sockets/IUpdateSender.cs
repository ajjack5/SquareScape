using SquareScape.Shared.Commands;
using System.Net.Sockets;

namespace SquareScape.Client.Sockets
{
    public interface IUpdateSender
    {
        string EncodedGameCommand { get; set; }

        void BeginSending(TcpClient client);
    }
}
