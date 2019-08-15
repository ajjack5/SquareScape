using SquareScape.Shared.GameState;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace SquareScape.Server.Engine
{
    public interface IServerGameState : IGameState
    {
        ConcurrentDictionary<Guid, UdpClient> ConnectedClients { get; set; }
    }
}
