using System;
using System.Net.Sockets;
using System.Text;

namespace SquareScape.Client.Sockets
{
    public class UpdateSender : IUpdateSender
    {
        public TcpClient TcpClient { get; set; }

        public void Send(string encodedGameCommand)
        {
            try
            {
                byte[] bytesToSend = Encoding.ASCII.GetBytes(encodedGameCommand);

                using (NetworkStream stream = TcpClient.GetStream())
                {
                    stream.Write(bytesToSend, 0, bytesToSend.Length);
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
                throw;
            }
        }
    }
}