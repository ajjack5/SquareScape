using SquareScape.Client.Converters;
using SquareScape.Client.Sockets;
using SquareScape.Shared.Commands;
using System.Net.Sockets;
using System.Threading;

namespace SquareScape.Client.Engine
{
    public class ClientGameState : IClientGameState
    {
        public ConcurrentDictionary<Guid, string> PlayersLoggedIn = new ConcurrentDictionary<Guid, string>();
        public ConcurrentDictionary<Guid, PlayerCoordinates> PlayerCoordinates = new ConcurrentDictionary<Guid, PlayerCoordinates>();
    }
}
