using SquareScape.Client.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SquareScape.Client
{
    public partial class Client : Form
    {
        private IClientEngine _clientEngine;

        //private List<Player> players;
        //private readonly int modifier = 20;

        public Client()
        {
            InitializeComponent();
            //players = new List<Player>();
            //players.Add(new Player(Color.Aqua));

            _clientEngine = new ClientEngine();
            _clientEngine.Start();

            Login();
            BeginRender();
        }

        private void Login()
        {
            //IGameCommand loginCommand = new LoginCommand();
            // create a button to login
            // create a service for the tcp sender / udp receiver
            // send this login command to the server
            // client or server to create the initial square / server in a random location
            // initialise the game world
            // then begin rendering below
        }

        private void BeginRender()
        {
            while (true)
            {

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
