using SquareScape.Common.Enums;
using System;

namespace SquareScape.Common.Commands
{
    public class GameCommand : IGameCommand
    {
        public GameCommands Command { get; set; }
        public Tuple<Guid, object> Data { get; set; }
    }
}
