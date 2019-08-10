using Microsoft.Extensions.DependencyInjection;
using SquareScape.Client.Engine;
using System;
using System.Windows.Forms;

namespace SquareScape.Client
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(StartupClient());
        }

        private static Client StartupClient()
        {
            IServiceProvider serviceProvider = Startup.ConfigureServices();
            return serviceProvider.GetRequiredService<Client>();
        }
    }
}
