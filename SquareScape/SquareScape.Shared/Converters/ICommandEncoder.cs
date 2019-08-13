using SquareScape.Shared.Commands;

namespace SquareScape.Shared.Converters
{
    public interface ICommandEncoder
    {
        string Encode(IGameCommand gameCommand);
    }
}
