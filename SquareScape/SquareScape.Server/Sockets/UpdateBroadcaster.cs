using SquareScape.Server.Engine;
using System.Text;

namespace SquareScape.Server.Sockets
{
    public class UpdateBroadcaster : IUpdateBroadcaster
    {
        private readonly IServerGameState _serverGameState;

        public UpdateBroadcaster(IServerGameState serverGameState)
        {
            _serverGameState = serverGameState;
        }

        public void Broadcast(string gameState)
        {
            byte[] data = Encoding.ASCII.GetBytes(gameState);
            foreach (var client in _serverGameState.ConnectedClients.Values)
            {
                client.Send(data, data.Length);
            };
        }
    }
}
