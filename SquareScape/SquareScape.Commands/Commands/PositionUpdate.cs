﻿namespace SquareScape.Commands.Commands
{
    public class PositionUpdate : IGameUpdate
    {
        public string GameState { get; set; }
        public string IPAddress { get; set; }
    }
}
