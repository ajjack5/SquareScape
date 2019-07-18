using System.Drawing;
using System.Windows.Forms;

namespace SquareScape.Client.Entities
{
    public class Player : PictureBox
    {
        public Player(Color color)
        {
            base.BackColor = color;
        }
    }
}
