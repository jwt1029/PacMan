using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackMan
{
    public partial class GameField : Form
    {
        private List<PictureBox> pictures = new List<PictureBox>();
        private List<Point> blocksPoint = new List<Point>();
        private List<string> map = File.ReadAllText("map.txt").Split(' ').ToList();
        private bool close = false;
        private Keys nowKey, nextKey;
        private List<Point> foodPoint = new List<Point>();

        public GameField()
        {
            InitializeComponent();
            player.Image = playerList.Images[0];
            map.Remove("0");
            map.Remove("");
            foreach (string str in map)
            {
                /* SET BLOCKS */
                ((PictureBox)(this.Controls["pictureBox" + str])).Image = mapList.Images[2];
                blocksPoint.Add(((PictureBox)(this.Controls["pictureBox" + str])).Location);
            }
            for (int i = 1; i < 300; i++)
            {
                /* SET FOODS */
                if (((PictureBox)(this.Controls["pictureBox" + i.ToString()])).Image == null)
                {
                    ((PictureBox)(this.Controls["pictureBox" + i.ToString()])).Image = mapList.Images[0];
                    foodPoint.Add(((PictureBox)(this.Controls["pictureBox" + i.ToString()])).Location);
                }
                pictures.Add((PictureBox)(this.Controls["pictureBox" + i.ToString()]));
            }
        }

        private bool collisionPlayer(int x, int y)
        {
            Point[] newPos = new Point[4];
            newPos[0] = new Point(player.Location.X + 1, player.Location.Y + 1);
            newPos[1] = new Point(player.Location.X + 23, player.Location.Y + 1);
            newPos[2] = new Point(player.Location.X + 23, player.Location.Y + 23);
            newPos[3] = new Point(player.Location.X + 1, player.Location.Y + 23);


            for (int index = 0; index < 4; index++)
            {
                newPos[index].X += x;
                newPos[index].Y += y;

                foreach (Point block in blocksPoint)
                {
                    if (block.X <= newPos[index].X && block.X + 25 >= newPos[index].X)
                    {
                        if (block.Y <= newPos[index].Y && block.Y + 25 >= newPos[index].Y)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private void GameField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && player.Location.Y >= 42)
            {
                if (!collisionPlayer(0, -2))
                    nowKey = Keys.Up;
                else
                    nextKey = Keys.Up;
            }
            if (e.KeyCode == Keys.Down && player.Location.Y <= 338)
            {
                if (!collisionPlayer(0, 2))
                    nowKey = Keys.Down;
                else
                    nextKey = Keys.Down;
            }
            if (e.KeyCode == Keys.Left && player.Location.X >= 28)
            {
                if (!collisionPlayer(-2, 0))
                    nowKey = Keys.Left;
                else
                    nextKey = Keys.Left;
            }
            if (e.KeyCode == Keys.Right && player.Location.X <= 574)
            {
                if (!collisionPlayer(2, 0))
                    nowKey = Keys.Right;
                else
                    nextKey = Keys.Right;
            }
        }

        private void movePlayer()
        {
            while (!close)
            {
                /* NEXT KEY */
                if (nextKey == Keys.Up && player.Location.Y >= 42)
                {
                    if (!collisionPlayer(0, -2))
                    {
                        nowKey = nextKey;
                        player.Image = playerList.Images[4];
                        nextKey = Keys.JunjaMode;
                    }
                }
                if (nextKey == Keys.Down && player.Location.Y <= 338)
                {
                    if (!collisionPlayer(0, 2))
                    {
                        nowKey = nextKey;
                        player.Image = playerList.Images[3];
                        nextKey = Keys.JunjaMode;
                    }
                }
                if (nextKey == Keys.Left && player.Location.X >= 28)
                {
                    if (!collisionPlayer(-2, 0))
                    {
                        nowKey = nextKey;
                        player.Image = playerList.Images[2];
                        nextKey = Keys.JunjaMode;
                    }
                }
                if (nextKey == Keys.Right && player.Location.X <= 574)
                {
                    if (!collisionPlayer(2, 0))
                    {
                        nowKey = nextKey;
                        player.Image = playerList.Images[0];
                        nextKey = Keys.JunjaMode;
                    }
                }

                /* NOW KEY */
                if (nowKey == Keys.Up && player.Location.Y >= 42)
                {
                    if (!collisionPlayer(0, -2))
                        player.Location = new Point(player.Location.X, player.Location.Y - 2);
                }
                if (nowKey == Keys.Down && player.Location.Y <= 338)
                {
                    if (!collisionPlayer(0, 2))
                        player.Location = new Point(player.Location.X, player.Location.Y + 2);
                }
                if (nowKey == Keys.Left && player.Location.X >= 28)
                {
                    if (!collisionPlayer(-2, 0))
                        player.Location = new Point(player.Location.X - 2, player.Location.Y);
                }
                if (nowKey == Keys.Right && player.Location.X <= 574)
                {
                    if (!collisionPlayer(2, 0))
                        player.Location = new Point(player.Location.X + 2, player.Location.Y);
                }

                Point[] points = new Point[9];
                points[0] = player.Location;
                points[1] = new Point(player.Location.X - 1, player.Location.Y);
                points[2] = new Point(player.Location.X + 1, player.Location.Y);
                points[3] = new Point(player.Location.X, player.Location.Y - 1);
                points[4] = new Point(player.Location.X, player.Location.Y + 1);
                points[5] = new Point(player.Location.X - 1, player.Location.Y - 1);
                points[6] = new Point(player.Location.X - 1, player.Location.Y + 1);
                points[7] = new Point(player.Location.X + 1, player.Location.Y - 1);
                points[8] = new Point(player.Location.X + 1, player.Location.Y + 1);

                foreach (Point point in points)
                {
                    if (foodPoint.Contains(point))
                    {
                        foodPoint.Remove(point);
                        foreach (PictureBox picture in pictures)
                            if (picture.Location == point)
                                picture.Image = mapList.Images[1];
                    }
                }

                Thread.Sleep(10);
            }
        }

        private void GameField_Load(object sender, EventArgs e)
        {
            Thread move = new Thread(new ThreadStart(movePlayer));
            move.Start();
            Thread character = new Thread(new ThreadStart(charcterFace));
            character.Start();
        }

        private void charcterFace()
        {
            bool face = true;
            while (!close)
            {
                if (face)
                    player.Image = playerList.Images[1];
                else
                {
                    if(nowKey == Keys.Right)
                        player.Image = playerList.Images[0];
                    if (nowKey == Keys.Left)
                        player.Image = playerList.Images[2];
                    if (nowKey == Keys.Down)
                        player.Image = playerList.Images[3];
                    if (nowKey == Keys.Up)
                        player.Image = playerList.Images[4];
                }
                face = !face;
                Thread.Sleep(150);
            }
        }
        private void GameField_FormClosed(object sender, FormClosedEventArgs e)
        {
            close = true;
        }

    }
}
