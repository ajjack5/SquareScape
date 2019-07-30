using SquareScape.Shared.Commands;
using SquareScape.Shared.Updates;

namespace SquareScape.Server.Converters
{
    public interface ICommandDecoder
    {
        IGameCommand Decode(IGameUpdate gameUpdate);
    }
}
