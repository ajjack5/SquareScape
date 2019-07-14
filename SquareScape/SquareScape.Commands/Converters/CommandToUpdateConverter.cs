using SquareScape.Common.Commands;

namespace SquareScape.Common.Converters
{
    public static class CommandToUpdateConverter
    {
        public static IGameCommand ParseUpdate(this IGameCommand gameCommand)
        {
            /* Converts game commands into a game updates for the client to interpret
             * First  3 characters    = The type of update the server has actioned.  (position update)
             * Remaining characters   = Data attached                                (x coords, y coords, vector?)
            */

            return null;
        }
    }
}
