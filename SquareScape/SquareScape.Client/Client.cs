using SquareScape.Client.Engine;
using SquareScape.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace SquareScape.Client
{
    public partial class Client : Form
    {
        private readonly IClientEngine _clientEngine;

        //private List<Player> players;
        //private readonly int modifier = 20;

        public Client(IClientEngine clientEngine)
        {
            _clientEngine = clientEngine;

            InitializeComponent();
            ExecuteClient();
        }

        private void ExecuteClient()
        {
            _clientEngine.Start();

            Login();
            BeginRender();
        }

        private void Login()
        {
            string ipAddress = GetIpAddress();
            if (ipAddress != null)
            {
                IGameCommand loginCommand = new LoginCommand()
                {
                    Data = new Tuple<Guid, object>(Guid.NewGuid(), ipAddress)
                };

                _clientEngine.SendGameCommand(loginCommand);
            }

            // client or server to create the initial square / server in a random location
            // initialise the game world
            // then begin rendering below
        }

        private string GetIpAddress()
        {
            // string hostName = Dns.GetHostName()
            string hostName = "127.0.0.1";
            Ping ping = new Ping();
            var reply = ping.Send(hostName);

            if (reply.Status == IPStatus.Success)
            {
                return reply.Address.ToString();
            }
            return null;
        }
        


        // WRAP THIS IN A NEW THREAD OF ITS OWN
        private void BeginRender()
        {
            while (true)
            {
                // PULL ALL INFORMATION OFF THE GAME STATE, similar to how it happens in the server.
                // 
                // RENDER THE GRAPHICS FOR THAT GAME STATE
                // create some private functions below which update the UI
                // or create a new partial class service which does this

            }
        }

        
















        // Every X seconds which is defined in the timer properties get new position
        // TO-DO: Dynamic control creation and be able to control them
        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    Tuple<int, int> position = NewPosition();

        //    player.Location = new Point(position.Item1, position.Item2);
           
        //}

        //private Tuple<int, int> NewPosition()
        //{
        //    Point mouse = PointToClient(MousePosition);

        //    int potentialXPosition = player.Left + (mouse.X + modifier) - player.Location.X;
        //    potentialXPosition = Math.Max(potentialXPosition, 0);

        //    int potentialYPosition = player.Top + (mouse.Y + modifier) - player.Location.Y;
        //    potentialYPosition = Math.Max(potentialYPosition, 0);

        //    int widthDifference = player.Parent.Width - player.Width;
        //    int heightDifference = player.Parent.Height - player.Height;

        //    int x = Math.Min(potentialXPosition, widthDifference);
        //    int y = Math.Min(potentialYPosition, heightDifference);

        //    return new Tuple<int, int>(x, y);
        //}
    }
}
