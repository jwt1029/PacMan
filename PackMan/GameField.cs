using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackMan
{
    public partial class GameField : Form
    {
        private List<PictureBox> pictures = new List<PictureBox>();
        private List<Point> blocksPoint = new List<Point>();
        private List<string> map = File.ReadAllText("map.txt").Split(' ').ToList();
        public GameField()
        {
            InitializeComponent();
            player.Image = userList.Images[0];
            map.Remove("0");
            map.Remove("");
            foreach (string str in map)
            {
                ((PictureBox)(this.Controls["pictureBox" + str])).Image = mapList.Images[2];
                blocksPoint.Add(((PictureBox)(this.Controls["pictureBox" + str])).Location);
            }
            for (int i = 1; i < 300; i++)
            {
                if (((PictureBox)(this.Controls["pictureBox" + i.ToString()])).Image == null)
                    ((PictureBox)(this.Controls["pictureBox" + i.ToString()])).Image = mapList.Images[0];
                pictures.Add((PictureBox)(this.Controls["pictureBox" + i.ToString()]));
            }
        }

        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && player.Location.Y >= 42)
            {
                foreach (Point block in blocksPoint)
                    if (!(block.Y + 27 <= player.Location.Y) && block.X <= player.Location.X && player.Location.X <= block.X)
                        return;
                player.Location = new Point(player.Location.X, player.Location.Y - 2);
            }
            if (e.KeyCode == Keys.Down && player.Location.Y <= 338)
            {
                foreach (Point block in blocksPoint)
                    if (!(block.Y >= player.Location.Y + 27) && block.X <= player.Location.X && player.Location.X <= block.X)
                        return;
                player.Location = new Point(player.Location.X, player.Location.Y + 2);
            }
            if (e.KeyCode == Keys.Left && player.Location.X >= 28)
            {
                foreach (Point block in blocksPoint)
                    if (!(block.X + 27 <= player.Location.X) && block.Y <= player.Location.Y && player.Location.Y <= block.Y)
                        return;
                player.Location = new Point(player.Location.X - 2, player.Location.Y);
            }
            if (e.KeyCode == Keys.Right && player.Location.X <= 574)
            {
                foreach (Point block in blocksPoint)
                    if (!(block.X <= player.Location.X + 27) && block.Y <= player.Location.Y && player.Location.Y <= block.Y)
                        return;
                player.Location = new Point(player.Location.X + 2, player.Location.Y);
            }
        }

        private void player_LocationChanged(object sender, EventArgs e)
        {
        }
    }
}
