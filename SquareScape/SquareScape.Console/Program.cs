using Microsoft.Extensions.DependencyInjection;
using SquareScape.Server.Engine;
using System;

namespace SquareScape.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = Startup.ConfigureServices();

            IEngine engine = serviceProvider.GetRequiredService<IEngine>();
            engine.Start();
        }
    }
}
