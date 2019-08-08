using SquareScape.Shared.Commands;
using SquareScape.Shared.Enums;

namespace SquareScape.Client.Converters
{
    public class CommandEncoder : ICommandEncoder
    {
        public string Encode(IGameCommand gameCommand)
        {
            string gameCommandString = "";
            ////002x7000000-0040-0000-0000-000000005555x1000y7899
            //string commandData = gameUpdate.GameState.Substring(0, 3); //002
            //string playerIdData = gameUpdate.GameState.Substring(3, 36); //x7000000-0040-0000-0000-000000005555
            //string mainData = gameUpdate.GameState.Substring(38, gameUpdate.GameState.Length - 39); //10007899

            gameCommandString += GetCommandString(gameCommand);
            //gameCommandString += playerdataToString;
            //gameCommandString += mainData.ToString;

            return gameCommandString;
        }

        private string GetCommandString(IGameCommand gameCommand)
        {
            switch (gameCommand.Command)
            {
                case GameCommands.None:
                    break;

                case GameCommands.Login:
                    return "001";

                case GameCommands.Position:
                    return "002";
            }

            return null;
        }

        //private Tuple<Guid, object> ProcessData(GameCommands command, string playerIdData, string mainData, string IPAddress)
        //{
        //    switch (command)
        //    {
        //        case GameCommands.None:
        //            break;

        //        case GameCommands.Login:
        //            _gameStateOrchestrator.PlayersLoggedIn.AddOrUpdate(Guid.Parse(playerIdData), IPAddress, (key, oldvalue) => IPAddress);
        //            return new Tuple<Guid, object>(Guid.Parse(mainData), IPAddress);

        //        case GameCommands.Position:
        //            PlayerCoordinates playerCoordinates = new PlayerCoordinates()
        //            {
        //                X = uint.Parse(mainData.Substring(0, 4)),
        //                Y = uint.Parse(mainData.Substring(4, 4)),
        //            };
        //            _gameStateOrchestrator.PlayerCoordinates.AddOrUpdate(Guid.Parse(playerIdData), playerCoordinates, (key, oldvalue) => playerCoordinates);
        //            return new Tuple<Guid, object>(Guid.Parse(mainData), playerCoordinates);
        //    }

        //    return null;
        //}

    }
}
