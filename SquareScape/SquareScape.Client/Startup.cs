﻿using Microsoft.Extensions.DependencyInjection;
using SquareScape.Client.Engine;
using SquareScape.Client.Sockets;
using SquareScape.Shared.Converters;
using SquareScape.Shared.GameState;

namespace SquareScape.Client
{
    static class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            IServiceCollection serviceProvider = new ServiceCollection()
                .AddSingleton<Client, Client>()
                .AddSingleton<IReceiverQueue, ReceiverQueue>()
                .AddSingleton<IClientEngine, ClientEngine>()
                .AddSingleton<IClientGameState, ClientGameState>()
                .AddSingleton<IGameState, IClientGameState>()
                .AddSingleton<IUpdateGatherer, UpdateGatherer>()
                .AddSingleton<IUpdateSender, UpdateSender>()
                .AddSingleton<ICommandEncoder, CommandEncoder>();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
