using System;
using System.Net.Sockets;
using System.Text;

namespace SquareScape.Client
{
    public class UpdateSender : IUpdateSender
    {
        public UpdateSender()
        {

        }

        public void BeginSending(TcpClient client)
        {
            while (true)
            {
                string data = null; // TODO
                if (data != null)
                {
                    try
                    {
                        byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(data);
                        data = null;

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
