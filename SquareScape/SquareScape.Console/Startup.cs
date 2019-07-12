using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SquareScape.Commands.Commands;
using SquareScape.Server;
using SquareScape.Server.Queue;

namespace SquareScape.Console
{
    public static class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            IServiceCollection serviceProvider = new ServiceCollection()
                .AddSingleton<Engine, Engine>()
                .AddSingleton<IRecieverQueue<IGameUpdate>, RecieverQueue<IGameUpdate>>()
                .AddSingleton<UpdateReciever, UpdateReciever>();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
