using SquareScape.Shared.Commands;
using SquareScape.Shared.Enums;
using SquareScape.Shared.GameState;
using SquareScape.Shared.Models;
using SquareScape.Shared.Updates;
using System;

namespace SquareScape.Shared.Converters
{
    public class CommandDecoder : ICommandDecoder
    {
        private readonly IGameState _gameState;

        public CommandDecoder(IGameState gameState)
        {
            _gameState = gameState;
        }

        public IGameCommand Decode(IGameUpdate gameUpdate)
        {
            //002x7000000-0040-0000-0000-000000005555x1000y7899
            string commandData = gameUpdate.GameState.Substring(0, 3); //002
            string playerIdData = gameUpdate.GameState.Substring(3, 36); //x7000000-0040-0000-0000-000000005555
            string mainData = gameUpdate.GameState.Substring(39, gameUpdate.GameState.Length - 39); //10007899

            GameCommands command = GetCommand(commandData);

            IGameCommand gameCommand = new GameCommand
            {
                Command = command,
                Data = ProcessData(command, playerIdData, mainData, gameUpdate.IPAddress)
            };

            return gameCommand;
        }

        private GameCommands GetCommand(string commandData)
        {
            switch (commandData)
            {
                case "000":
                    break;

                case "001":
                    return GameCommands.Login;

                case "002":
                    return GameCommands.Position;
            }

            return GameCommands.None;
        }

        private Tuple<Guid, object> ProcessData(GameCommands command, string playerIdData, string mainData, string IPAddress)
        {
            switch (command)
            {
                case GameCommands.None:
                    break;

                case GameCommands.Login:
                    _gameState.PlayersLoggedIn.AddOrUpdate(Guid.Parse(playerIdData), IPAddress, (key, oldvalue) => IPAddress);
                    return new Tuple<Guid, object>(Guid.Parse(playerIdData), IPAddress);

                case GameCommands.Position:
                    PlayerCoordinates playerCoordinates = new PlayerCoordinates()
                    {
                        X = uint.Parse(mainData.Substring(0, 4)),
                        Y = uint.Parse(mainData.Substring(4, 4)),
                    };
                    _gameState.PlayerCoordinates.AddOrUpdate(Guid.Parse(playerIdData), playerCoordinates, (key, oldvalue) => playerCoordinates);
                    return new Tuple<Guid, object>(Guid.Parse(playerIdData), playerCoordinates);
            }

            return null;
        }
    }
}
