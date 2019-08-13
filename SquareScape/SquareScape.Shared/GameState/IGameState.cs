using SquareScape.Shared.Models;
using System;
using System.Collections.Concurrent;

namespace SquareScape.Shared.GameState
{
    public interface IGameState
    {
        ConcurrentDictionary<Guid, string> PlayersLoggedIn { get; set; }
        ConcurrentDictionary<Guid, PlayerCoordinates> PlayerCoordinates { get; set; }
    }
}
