﻿using SquareScape.Shared.Commands;
using SquareScape.Shared.Updates;
using SquareScape.Server.Converters;
using SquareScape.Server.Queue;
using System;
using System.Collections.Generic;
using System.Timers;
using SquareScape.Server.Sockets;

namespace SquareScape.Server.Engine
{
    public class Engine
    {
        private readonly IUpdateReceiver _reciever;
        private readonly IUpdateBroadcaster _broadcaster;
        private readonly IReceiverQueue<IGameUpdate> _queue;
        private readonly GameStateOrchestrator _gameStateOrchestrator;
        private readonly ICommandDecoder _converter;

        private const int _GAMETICK = 100;
        private const int _BATCHSIZE = 200;

        public Engine(IUpdateReceiver reciever, IUpdateBroadcaster broadcaster, IReceiverQueue<IGameUpdate> queue,
            GameStateOrchestrator gameStateOrchestrator, ICommandDecoder converter)
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
            InitialiseGameTickTimer();
        }

        private void InitialiseGameTickTimer()
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
            //string gameState = "";

            foreach (var gameUpdate in gameUpdates)
            {
                IGameCommand command = _converter.Decode(gameUpdate);
            }

            // process any interactions here using the orchestrator
            // merge all game commands into 1 data packet string, including any additional interactions
            //foreach (var gameCommand in gameCommands)
            //{
            //separate by _ ??? idk
            //}

            // should the game orchestrator be netstandard and used also by the client
            // this way we can share interactions? idk? 1 source of truth ..

            // broadcast the packet to all currently logged in clients
            _broadcaster.Broadcast("");
        }
    }
}