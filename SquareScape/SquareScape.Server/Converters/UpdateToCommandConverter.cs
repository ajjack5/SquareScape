using SquareScape.Common.Commands;
using SquareScape.Common.Enums;
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
            /* Converts game updates into a game command for the server to interpret
             * First  3 characters    = The type of command the client has actioned. (position update)
             * Remaining characters   = Data attached                                (x coords, y coords, vector?)
            */
            
            if (gameUpdate.GameState.Length < 9)
            {
                throw new NotSupportedException("Contract violation; Data received must be atleast 9 characters in length.");
            }
            
            string commandData = gameUpdate.GameState.Substring(0, 3);
            string mainData = gameUpdate.GameState.Substring(4, gameUpdate.GameState.Length - 3);

            GameCommands command = GetCommand(commandData);

            IGameCommand gameCommand = new GameCommand
            {
                Command = command,
                Data = ProcessData(mainData, command, gameUpdate.IPAddress)
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

       /* LOGIN >>>>>
        * Data = command: "001"
        *        guid= "fsda934jtm-34my-4m5k-dmfgk-6m76s"
        * IPAddress = "192.168.1.1"
        */

        private object ProcessData(string mainData, GameCommands command, string IPAddress)
        {
            switch (command)
            {
                case GameCommands.None:
                    break;
                case GameCommands.Login:
                    _gameStateOrchestrator.PlayersLoggedIn.AddOrUpdate(Guid.Parse(mainData), IPAddress, null);
                    return new Tuple<Guid, string>(Guid.Parse(mainData), IPAddress);
                case GameCommands.Position:
                    return null;
            }

            return null;
        }
    }
}
