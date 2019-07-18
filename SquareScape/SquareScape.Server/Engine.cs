﻿using SquareScape.Common.Commands;
using SquareScape.Common.Updates;
using SquareScape.Server.Converters;
using SquareScape.Server.Queue;
using System;
using System.Collections.Generic;
using System.Timers;

namespace SquareScape.Server
{
    public class Engine
    {
        private readonly IUpdateReceiver _reciever;
        private readonly IUpdateBroadcaster _broadcaster;
        private readonly IReceiverQueue<IGameUpdate> _queue;
        private readonly GameStateOrchestrator _gameStateOrchestrator;
        private readonly UpdateToCommandConverter _converter;

        private const int _GAMETICK = 100;
        private const int _BATCHSIZE = 200;

        public Engine(IUpdateReceiver reciever, IUpdateBroadcaster broadcaster, IReceiverQueue<IGameUpdate> queue, GameStateOrchestrator gameStateOrchestrator, UpdateToCommandConverter converter)
        {
            _reciever = reciever;
            _broadcaster = broadcaster;
            _queue = queue;
            _gameStateOrchestrator = gameStateOrchestrator;
            _converter = converter;
        }

        public void Start()
        {
            _reciever.Listen();
            InitialiseTimer();
        }

        private void InitialiseTimer()
        {
            Timer timer = new Timer();
            timer.Elapsed += ProcessGameTick;
            timer.Interval = _GAMETICK;
            timer.Enabled = true;
            timer.AutoReset = true;
        }

        private void ProcessGameTick(object source, ElapsedEventArgs e)
        {
            Console.Out.WriteLine($"Attempting to update from a list containing {_queue.Size()} updates.");

            IEnumerable<IGameUpdate> gameUpdates = _queue.PullBatch(_BATCHSIZE);
            IList<IGameCommand> gameCommands = new List<IGameCommand>();

            foreach (var gameUpdate in gameUpdates)
            {
                IGameCommand command = _converter.ParseCommand(gameUpdate);
                gameCommands.Add(command);
            }



            // merge all game commands into 1 data packet
            foreach (var gameCommand in gameCommands)
            {
                gameCommand.Data = null;
                // do something here
            }

            // broadcast the packet to all currently logged in clients
            string gameState = null;
            _broadcaster.Broadcast(gameState);
        }
    }
}
