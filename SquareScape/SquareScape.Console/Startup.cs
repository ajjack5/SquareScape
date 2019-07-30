using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SquareScape.Shared.Updates;
using SquareScape.Server.Converters;
using SquareScape.Server.Queue;
using SquareScape.Server.Engine;
using SquareScape.Server.Sockets;

namespace SquareScape.Console
{
    public static class Startup
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
                .AddSingleton<Engine, Engine>()
                .AddSingleton<GameStateOrchestrator, GameStateOrchestrator>()
                .AddSingleton<ICommandDecoder, CommandDecoder>()
                .AddSingleton<IReceiverQueue<IGameUpdate>, ReceiverQueue<IGameUpdate>>()
                .AddSingleton<IUpdateReceiver, UpdateReceiver>()
                .AddSingleton<IUpdateBroadcaster, UpdateBroadcaster>();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
