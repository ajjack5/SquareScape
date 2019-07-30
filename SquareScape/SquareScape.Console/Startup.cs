using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SquareScape.Shared.Updates;
using SquareScape.Server;
using SquareScape.Server.Converters;
using SquareScape.Server.Queue;

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
                .AddSingleton<UpdateToCommandConverter, UpdateToCommandConverter>()
                .AddSingleton<IReceiverQueue<IGameUpdate>, ReceiverQueue<IGameUpdate>>()
                .AddSingleton<IUpdateReceiver, UpdateReceiver>()
                .AddSingleton<IUpdateBroadcaster, UpdateBroadcaster>();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
