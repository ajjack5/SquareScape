using SquareScape.Common.Enums;

namespace SquareScape.Common.Commands
{
    public interface IGameCommand
    {
        GameCommands Command { get; set; }
        object Data { get; set; }
    }
}
