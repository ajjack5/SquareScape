namespace SquareScape.Server
{
    public interface IUpdateBroadcaster
    {
        void Broadcast(string gameState);
    }
}
