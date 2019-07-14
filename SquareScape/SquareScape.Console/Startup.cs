using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SquareScape.Common.Commands;
using SquareScape.Server;
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
                })
                .AddSingleton<Engine, Engine>()
                .AddSingleton<IRecieverQueue<IGameUpdate>, RecieverQueue<IGameUpdate>>()
                .AddSingleton<UpdateReciever, UpdateReciever>();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
