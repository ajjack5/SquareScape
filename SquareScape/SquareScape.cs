using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace SquareScape.Client
{
    public partial class SquareScape : Form
    {
        private bool left = false;
        private bool right = false;
        private bool up = false;
        private bool down = false;
        
        public SquareScape()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(SquareScape_KeyDown);
        }

        void SquareScape_KeyDown(object sender, KeyEventArgs e)
        {
            int x = pictureBox1.Location.X;
            int y = pictureBox1.Location.Y;


            if (e.KeyCode == Keys.Right && up)
            {
                x += 1;
                y += 1;
            }
            else if (e.KeyCode == Keys.Right && down)
            {
                x += 1;
                y -= 1;
            }
            else if (e.KeyCode == Keys.Left && up)
            {
                x -= 1;
                y += 1;
            }
            else if (e.KeyCode == Keys.Left && down)
            {
                x -= 1;
                y -= 1;
            }
            else if (e.KeyCode == Keys.Right)
            {
                x += 1;
            }
            else if (e.KeyCode == Keys.Left)
            {
                x -= 1;
            }
            else if (e.KeyCode == Keys.Up)
            {
                y -= 1;
            }
            else if (e.KeyCode == Keys.Down)
            {
                y += 1;
            } 

            pictureBox1.Location = new Point(x, y);
        }

        private void SquareScape_Load(object sender, EventArgs e)
        {

        }
    }
}
