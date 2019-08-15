using SquareScape.Shared.Models;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace SquareScape.Server.Engine
{
    public class ServerGameState : IServerGameState
    {
        public ConcurrentDictionary<Guid, string> PlayersLoggedIn { get; set; } = new ConcurrentDictionary<Guid, string>();
        public ConcurrentDictionary<Guid, PlayerCoordinates> PlayerCoordinates { get; set; } = new ConcurrentDictionary<Guid, PlayerCoordinates>();
        public ConcurrentDictionary<Guid, UdpClient> ConnectedClients { get; set; } = new ConcurrentDictionary<Guid, UdpClient>();
    }
}
