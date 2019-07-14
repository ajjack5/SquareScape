namespace SquareScape.Common.Updates
{
    public interface IGameUpdate
    {
        string GameState { get; set; }
        string IPAddress { get; set; }
    }
}
