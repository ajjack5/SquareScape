using SquareScape.Shared.Models;
using System;
using System.Collections.Concurrent;

namespace SquareScape.Server.Engine
{
    public class ServerGameState : IServerGameState
    {
        public ConcurrentDictionary<Guid, string> PlayersLoggedIn = new ConcurrentDictionary<Guid, string>();
        public ConcurrentDictionary<Guid, PlayerCoordinates> PlayerCoordinates = new ConcurrentDictionary<Guid, PlayerCoordinates>();
    }
}
