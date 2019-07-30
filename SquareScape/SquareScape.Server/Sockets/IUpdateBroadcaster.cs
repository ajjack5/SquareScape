namespace SquareScape.Server.Sockets
{
    public interface IUpdateBroadcaster
    {
        void Broadcast(string gameState);
    }
}
