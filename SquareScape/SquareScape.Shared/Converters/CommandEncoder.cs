using SquareScape.Shared.Commands;
using SquareScape.Shared.Enums;

namespace SquareScape.Shared.Converters
{
    public class CommandEncoder : ICommandEncoder
    {
        public string Encode(IGameCommand gameCommand)
        {
            string gameCommandString = "";
            //002x7000000-0040-0000-0000-000000005555x1000y7899
            
            //002
            //x7000000-0040-0000-0000-000000005555
            //10007899

            gameCommandString += GetCommandString(gameCommand);
            gameCommandString += gameCommand.Data.Item1.ToString();
            gameCommandString += "ChangeMe"; // TODO

            return gameCommandString;
        }

        // TODO make this like Decoder with different commands etc.

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
    }
}
