using SquareScape.Common.Enums;
using System;

namespace SquareScape.Common.Commands
{
    public interface IGameCommand
    {
        GameCommands Command { get; set; }
        Tuple<Guid, object> Data { get; set; }
    }
}
