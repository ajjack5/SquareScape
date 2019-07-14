using SquareScape.Common.Commands;
using SquareScape.Common.Enums;
using SquareScape.Common.Updates;
using System;

namespace SquareScape.Common.Converters
{
    public static class UpdateToCommandConverter
    {
        public static IGameCommand ParseCommand(this IGameUpdate gameUpdate)
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

            IGameCommand gameCommand = new GameCommand
            {
                Command = GetCommand(commandData),
                //Data = GetData(mainData) // TODO how do we get the dynamically changing based off the command?
            };

            return null;
        }

        private static GameCommands GetCommand(string commandData)
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
    }
}
