using SquareScape.Shared.Commands;

namespace SquareScape.Client.Converters
{
    public interface ICommandEncoder
    {
        string Encode(IGameCommand gameCommand);
    }
}
