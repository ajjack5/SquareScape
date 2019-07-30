using SquareScape.Shared.Enums;
using System;

namespace SquareScape.Shared.Commands
{
    public class GameCommand : IGameCommand
    {
        public GameCommands Command { get; set; }
        public Tuple<Guid, object> Data { get; set; }
    }
}
