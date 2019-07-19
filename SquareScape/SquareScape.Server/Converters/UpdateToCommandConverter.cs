using SquareScape.Common.Commands;
using SquareScape.Common.Enums;
using SquareScape.Common.Models;
using SquareScape.Common.Updates;
using System;

namespace SquareScape.Server.Converters
{
    public class UpdateToCommandConverter
    {
        private readonly GameStateOrchestrator _gameStateOrchestrator;

        public UpdateToCommandConverter(GameStateOrchestrator gameStateOrchestrator)
        {
            _gameStateOrchestrator = gameStateOrchestrator;
        }

        public IGameCommand ParseCommand(IGameUpdate gameUpdate)
        { 
            //if (gameUpdate.GameState.Length < 9)
            //{
            //    throw new NotSupportedException("Contract violation; Data received must be atleast 9 characters in length.");
            //}
            
            string commandData = gameUpdate.GameState.Substring(0, 3);
            string playerIdData = gameUpdate.GameState.Substring(3, 36);
            string mainData = gameUpdate.GameState.Substring(38, gameUpdate.GameState.Length - 39);

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
                    _gameStateOrchestrator.PlayersLoggedIn.AddOrUpdate(Guid.Parse(playerIdData), IPAddress, (key, oldvalue) => IPAddress);
                    return new Tuple<Guid, object>(Guid.Parse(mainData), IPAddress);

                case GameCommands.Position:
                    PlayerCoordinates playerCoordinates = new PlayerCoordinates()
                    {
                        X = uint.Parse(mainData.Substring(0, 4)),
                        Y = uint.Parse(mainData.Substring(4, 4)),
                    };
                    _gameStateOrchestrator.PlayerCoordinates.AddOrUpdate(Guid.Parse(playerIdData), playerCoordinates, (key, oldvalue) => playerCoordinates);
                    return new Tuple<Guid, object>(Guid.Parse(mainData), playerCoordinates);
            }

            return null;
        }
    }
}
