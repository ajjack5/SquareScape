using SquareScape.Shared.Commands;
using SquareScape.Shared.Updates;

namespace SquareScape.Shared.Converters
{
    public interface ICommandDecoder
    {
        IGameCommand Decode(IGameUpdate gameUpdate);
        IGameCommand Decode(string gameUpdate);
    }
}
