using SquareScape.Common.Enums;
using System;

namespace SquareScape.Common.Commands
{
    public class GameCommand : IGameCommand
    {
        public GameCommands Command { get; set; }
        public object Data { get; set; }
    }
}
