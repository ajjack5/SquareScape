using SquareScape.Common.Enums;

namespace SquareScape.Common.Commands
{
    public class GameCommand : IGameCommand
    {
        public GameCommands Command { get; set; }
        // Data Data { get; set; } -- also adjust in interface
    }
}
