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
        private List<string> map = File.ReadAllText("map.txt").Split('|')[0].Split(' ').ToList();
        private List<string> monster = File.ReadAllText("map.txt").Split('|')[1].Split(' ').ToList();
        private List<Monster> monsters = new List<Monster>();
        private List<PictureBox> monsterImage = new List<PictureBox>();
        private bool close = false;
        private Keys nowKey, nextKey;
        private List<Point> foodPoint = new List<Point>();
        private List<PictureBox> cookies = new List<PictureBox>();
        private List<Point> monsterPoint = new List<Point>();
        private int life = 3;
        private int score = 0;
        private int overpower;
        private bool pause = false;

        public GameField()
        {
            InitializeComponent();
            player.Image = playerList.Images[0];
            map.Remove("0");
            map.Remove("");
            monster.Remove("0");
            monster.Remove("");

            foreach (string str in monster)
                monsterPoint.Add(((PictureBox)(this.Controls["pictureBox" + str])).Location);
            
            monsters.Add(new Monster("M1", monsterPoint, blocksPoint, monsterA, monsterList));
            monsters.Add(new Monster("M2", monsterPoint, blocksPoint, monsterB, monsterList));
            monsters.Add(new Monster("M3", monsterPoint, blocksPoint, monsterC, monsterList));
            monsters.Add(new Monster("M4", monsterPoint, blocksPoint, monsterD, monsterList));

            cookies.Add(((PictureBox)(this.Controls["pictureBox49"])));
            cookies.Add(((PictureBox)(this.Controls["pictureBox69"])));
            cookies.Add(((PictureBox)(this.Controls["pictureBox233"])));
            cookies.Add(((PictureBox)(this.Controls["pictureBox251"])));

            foreach (PictureBox picture in cookies)
                picture.Image = mapList.Images[3];

            foreach (string str in map)
            {
                /* SET BLOCKS */
                ((PictureBox)(this.Controls["pictureBox" + str])).Image = mapList.Images[2];
                blocksPoint.Add(((PictureBox)(this.Controls["pictureBox" + str])).Location);
            }
            foreach (string str in monster)
            {
                /* SET MONSTER ZONE */
                ((PictureBox)(this.Controls["pictureBox" + str])).Image = mapList.Images[1];
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
                        score += 10;
                        foreach (PictureBox picture in pictures)
                            if (picture.Location == point)
                                picture.Image = mapList.Images[1];
                        if (foodPoint.Count == 0)
                        {
                            timer1.Stop();
                            for (int i = 1; i < 300; i++)
                                ((PictureBox)(this.Controls["pictureBox" + i.ToString()])).Dispose();
                            foreach (Monster m in monsters)
                                m.setClose();
                            player.Visible = false;
                            Thread gameEnd = new Thread(new ThreadStart(END));
                            gameEnd.Start();
                        }
                    }
                }

                bool found = false;
                foreach (PictureBox cookie in cookies)
                {
                    foreach (Point point in points)
                        if (cookie.Location == point)
                        {
                            cookie.Image = mapList.Images[1];
                            cookies.Remove(cookie);
                            overpower = 4;
                            foreach (Monster m in monsters)
                            {
                                Thread dizzy = new Thread(new ParameterizedThreadStart(eatCookie));
                                dizzy.Start(m);
                            }
                            found = true;
                            break;
                        }
                    if (found)
                        break;
                }

                Thread.Sleep(10);
            }
        }

        private void eatCookie(object obj)
        {
            Monster m = (Monster)obj;
            m.getDizzy();
            overpower--;
        }

        private void END()
        {
            MessageBox.Show("You Win!" + Environment.NewLine + "Score : " + score);
        }

        private void GameField_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread move = new Thread(new ThreadStart(movePlayer));
            move.Start();
            Thread character = new Thread(new ThreadStart(charcterFace));
            character.Start();
            Thread mCheck = new Thread(new ThreadStart(monsterCheck));
            mCheck.Start();
            timer1.Start();
            Thread ptCheck = new Thread(new ThreadStart(pointCheck));
            ptCheck.Start();
            monsterCount.Start();

            Thread music = new Thread(new ThreadStart(soundPlay));
            music.Start();
        }

        private void soundPlay()
        {
            /*WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = "Melody of The night.mp3";
            wplayer.controls.play();*/
        }

        private void pointCheck()
        {
            while (foodPoint.Count > 0 && !close)
            {
                string pt = score.ToString();
                int len = pt.Length;
                for (int i = 0; i < 5 - len; i++)
                    pt = "0" + pt;
                scoreBoard.Text = " " + pt;
            }
        }

        private void monsterCheck()
        {
            while (foodPoint.Count > 0 && !close)
            {
                object[] obj = collisionMonster(0, 0);
                if ((bool)obj[0])
                {
                    if (overpower > 0)
                    {
                        Monster m = (Monster)obj[1];
                        //monsterPoint.Remove(m.Location);
                        m.dead();
                        score += 200;
                    }
                    else
                    {
                        if (life == 3)
                            life3.Visible = false;
                        if (life == 2)
                            life2.Visible = false;
                        if (life == 1)
                            life1.Visible = false;
                        life--;
                        if (life == 0)
                            gameOver();
                        for (int i = 0; i < 3; i++)
                        {
                            player.Visible = false;
                            Thread.Sleep(300);
                            player.Visible = true;
                            Thread.Sleep(300);
                        }
                    }
                }
            }
        }

        private void gameOver()
        {
            MessageBox.Show("Game Over");
            timer1.Stop();
        }

        private object[] collisionMonster(int x, int y)
        {
            Point[] newPos = new Point[4];
            newPos[0] = new Point(player.Location.X, player.Location.Y);
            newPos[1] = new Point(player.Location.X + 27, player.Location.Y);
            newPos[2] = new Point(player.Location.X + 27, player.Location.Y + 27);
            newPos[3] = new Point(player.Location.X, player.Location.Y + 27);


            for (int index = 0; index < 4; index++)
            {
                newPos[index].X += x;
                newPos[index].Y += y;

                foreach (Monster m in monsters)
                {
                    Point mon = m.getPoint();
                    if (mon.X <= newPos[index].X && mon.X + 25 >= newPos[index].X)
                    {
                        if (mon.Y <= newPos[index].Y && mon.Y + 25 >= newPos[index].Y)
                        {
                            object[] obj = new object[2];
                            obj[0] = true;
                            obj[1] = m;
                            return obj;
                        }
                    }
                }
            }

            return new object[] {false};
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
                    if (nowKey == Keys.Right)
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
            foreach (Monster m in monsters)
                m.setClose();
            close = true;
        }

        private void monsterCount_Tick(object sender, EventArgs e)
        {
            foreach (Monster m in monsters)
                if(m.getReviveTime() != 0)
                     m.reviving();
        }

        private int spendTime = 0;
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            spendTime++;
            int min = spendTime / 60;
            int sec = spendTime % 60;
            string minutes = (min / 10 == 0) ? "0" + min : min.ToString();
            string seconds = (sec / 10 == 0) ? "0" + sec : sec.ToString();
            timeBoard.Text = " " + minutes + " : " + seconds;
        }

    }
}
