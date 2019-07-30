using SquareScape.Shared.Commands;

namespace SquareScape.Client.Converters
{
    interface ICommandEncoder
    {
        string Encode(IGameCommand gameCommand);
    }
}
