using Microsoft.Extensions.DependencyInjection;
using SquareScape.Server;
using System;

namespace SquareScape.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider serviceProvider = Startup.ConfigureServices();

            Engine engine = serviceProvider.GetRequiredService<Engine>();
            engine.Start();
        }
    }
}
