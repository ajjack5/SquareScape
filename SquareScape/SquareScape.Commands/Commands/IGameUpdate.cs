namespace SquareScape.Common.Commands
{
    public interface IGameUpdate
    {
        string GameState { get; set; }
        string IPAddress { get; set; }
    }
}
