using System;
using System.Net.Sockets;
using System.Text;

namespace SquareScape.Client.Sockets
{
    public class UpdateSender : IUpdateSender
    {
        public string EncodedGameCommand { get; set; }

        public void BeginSending(TcpClient client)
        {
            while (true)
            {
                if (EncodedGameCommand != null)
                {
                    try
                    {
                        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(EncodedGameCommand);
                        EncodedGameCommand = null;

                        using (NetworkStream stream = client.GetStream())
                        {
                            stream.Write(bytesToSend, 0, bytesToSend.Length);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine(e.Message);
                    }
                }
            }
        }      
    }
}
