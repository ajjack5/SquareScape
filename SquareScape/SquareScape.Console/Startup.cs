using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SquareScape.Shared.Updates;
using SquareScape.Server.Queue;
using SquareScape.Server.Engine;
using SquareScape.Server.Sockets;
using SquareScape.Shared.Converters;
using SquareScape.Shared.GameState;

namespace SquareScape.Console
{
    static class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            IServiceCollection serviceProvider = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddConsole();
                    loggingBuilder.SetMinimumLevel(LogLevel.Debug);
                })
                .AddSingleton<IEngine, Engine>()
                .AddSingleton<IServerGameState, ServerGameState>()
                .AddSingleton<IGameState, IServerGameState>()
                .AddSingleton<ICommandDecoder, CommandDecoder>()
                .AddSingleton<IReceiverQueue<IGameUpdate>, ReceiverQueue<IGameUpdate>>()
                .AddSingleton<IUpdateReceiver, UpdateReceiver>()
                .AddSingleton<IUpdateBroadcaster, UpdateBroadcaster>();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
