using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SquareScape.Server
{
    class UpdateReciever
    {
        public void UpdateReciever(TcpClient client)
        {
            NetworkStream strm = client.getStream();
            IFormatter formatter = new BinaryFormatter();

            while (strm)
            {
                object o = (object)formatter.Deserialize(strm);
            }
        }
    }
}
