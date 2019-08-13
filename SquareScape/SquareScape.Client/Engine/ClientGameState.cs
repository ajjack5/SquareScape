using System;
using System.Collections.Concurrent;
using SquareScape.Shared.Models;

namespace SquareScape.Client.Engine
{
    public class ClientGameState : IClientGameState
    {
        public ConcurrentDictionary<Guid, string> PlayersLoggedIn { get; set; } = new ConcurrentDictionary<Guid, string>();
        public ConcurrentDictionary<Guid, PlayerCoordinates> PlayerCoordinates { get; set; } = new ConcurrentDictionary<Guid, PlayerCoordinates>();
    }
}
