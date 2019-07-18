using SquareScape.Common.Models;
using System;
using System.Collections.Concurrent;

namespace SquareScape.Server
{
    public abstract class GameStateOrchestrator
    {
        public ConcurrentDictionary<Guid, string> PlayersLoggedIn = new ConcurrentDictionary<Guid, string>();
        public ConcurrentDictionary<Guid, PlayerCoordinates> PlayerCoordinates = new ConcurrentDictionary<Guid, PlayerCoordinates>();
    }
}
