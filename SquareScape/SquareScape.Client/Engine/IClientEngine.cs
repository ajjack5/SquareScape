using SquareScape.Shared.Commands;

namespace SquareScape.Client.Engine
{
    public interface IClientEngine
    {
        void Start();
        void SendGameCommand(IGameCommand gameCommand);
    }
}
