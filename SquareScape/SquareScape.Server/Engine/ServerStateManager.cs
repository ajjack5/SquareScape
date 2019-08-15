using System.Net.Sockets;

namespace SquareScape.Server.Engine
{
    public class ServerStateManager : IServerStateManager
    {
        private readonly IServerGameState _serverGameState;

        public ServerStateManager(IServerGameState serverGameState)
        {
            _serverGameState = serverGameState;
        }

        public void Process()
        {
            foreach(var playersLoggedIn in _serverGameState.PlayersLoggedIn)
            {
                if(!_serverGameState.ConnectedClients.ContainsKey(playersLoggedIn.Key))
                {
                    UdpClient client = new UdpClient(playersLoggedIn.Value, 20001);
                    _serverGameState.ConnectedClients.AddOrUpdate(playersLoggedIn.Key, client, (key, oldvalue) => client);
                }
            }
        }
    }
}
