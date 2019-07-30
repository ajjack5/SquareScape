using System;
using SquareScape.Shared.Enums;

namespace SquareScape.Shared.Commands
{
    public class LoginCommand : IGameCommand
    {
        public GameCommands Command { get => GameCommands.Login; set => Command = value; }
        public Tuple<Guid, object> Data { get; set; }
    }
}
