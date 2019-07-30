namespace SquareScape.Shared.Updates
{
    public class GameUpdate : IGameUpdate
    {
        public string GameState { get; set; }
        public string IPAddress { get; set; }
    }
}
