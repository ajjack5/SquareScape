namespace SquareScape.Shared.GameState
{
    public interface IRequiredGameState : IGameState
    {
        ConcurrentDictionary<Guid, string> PlayersLoggedIn;
        ConcurrentDictionary<Guid, PlayerCoordinates> PlayerCoordinates;
    }
}
