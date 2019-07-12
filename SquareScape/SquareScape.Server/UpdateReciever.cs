using SquareScape.Commands.Commands;
using SquareScape.Server.Queue;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SquareScape.Server
{
    public class UpdateReciever
    {
        private readonly RecieverQueue<IGameUpdate> _queue;

        public const int PORT = 20000;
        public IPAddress localAddr = IPAddress.Parse("localhost");

        public UpdateReciever(RecieverQueue<IGameUpdate> queue)
        {
            _queue = queue;
        }

        public void Listen()
        {
            TcpListener server = new TcpListener(localAddr, PORT);
            server.Start();

            Thread serverThread = ListenerThread(server);
            serverThread.Start();
        }

        private Thread ListenerThread(TcpListener server)
        {
            Thread tcpThread = new Thread(() =>
            {
                byte[] bytes = new byte[1024];
                string data = null;

                while (true)
                {
                    TcpClient tcpClient = server.AcceptTcpClient();

                    data = null;

                    NetworkStream stream = tcpClient.GetStream();

                    int i;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        _queue.Push(new PositionUpdate { IPAddress = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString(), GameState = data });
                    }
                }
            });

            return tcpThread;
        }


    }
}
