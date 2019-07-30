using SquareScape.Shared.Enums;
using System;

namespace SquareScape.Shared.Commands
{
    public interface IGameCommand
    {
        GameCommands Command { get; set; }
        Tuple<Guid, object> Data { get; set; }
    }
}
