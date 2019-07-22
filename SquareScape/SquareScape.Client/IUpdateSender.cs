using System.Net.Sockets;

namespace SquareScape.Client
{
    public interface IUpdateSender
    {
        void BeginSending(TcpClient client);
    }
}
