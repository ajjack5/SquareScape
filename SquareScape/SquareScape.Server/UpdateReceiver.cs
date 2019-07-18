using SquareScape.Common.Updates;
using SquareScape.Server.Queue;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SquareScape.Server
{
    public class UpdateReceiver : IUpdateReceiver
    {
        private readonly IReceiverQueue<IGameUpdate> _queue;
 
        public const int PORT = 20000;
        public IPAddress localAddr = IPAddress.Parse("127.0.01");

        public UpdateReceiver(IReceiverQueue<IGameUpdate> queue)
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
                byte[] bytes = new byte[4096];
                string data = null;

                while (true)
                {
                    try
                    {
                        TcpClient tcpClient = server.AcceptTcpClient();

                        data = null;

                        using (NetworkStream stream = tcpClient.GetStream())
                        {
                            int i;

                            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                                _queue.Push(new GameUpdate { IPAddress = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString(), GameState = data });
                            }
                        }
                    } catch (Exception e)
                    {
                        Console.Out.WriteLine(e.Message);
                    }
                }
            });

            return tcpThread;
        }

    }
}
