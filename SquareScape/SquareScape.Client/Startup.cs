using Microsoft.Extensions.DependencyInjection;
using SquareScape.Client.Converters;
using SquareScape.Client.Engine;
using SquareScape.Client.Sockets;

namespace SquareScape.Client
{
    static class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            IServiceCollection serviceProvider = new ServiceCollection()
                .AddSingleton<Client, Client>()
                .AddSingleton<IClientEngine, ClientEngine>()
                .AddSingleton<IUpdateGatherer, UpdateGatherer>()
                .AddSingleton<IUpdateSender, UpdateSender>()
                .AddSingleton<ICommandEncoder, CommandEncoder>();

            return serviceProvider.BuildServiceProvider();
        }
    }
}
