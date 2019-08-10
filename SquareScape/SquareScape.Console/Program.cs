using Microsoft.Extensions.DependencyInjection;
using SquareScape.Server.Engine;
using System;

namespace SquareScape.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = Startup.ConfigureServices();

            IEngine engine = serviceProvider.GetRequiredService<IEngine>();
            engine.Start();
        }
    }
}
