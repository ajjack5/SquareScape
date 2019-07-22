using System;
using SquareScape.Common.Enums;

namespace SquareScape.Common.Commands
{
    public class LoginCommand : IGameCommand
    {
        public GameCommands Command { get => GameCommands.Login; set => Command = value; }
        public Tuple<Guid, object> Data { get; set; }
    }
}
