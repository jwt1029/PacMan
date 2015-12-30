using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackMan
{
    class Monster
    {
        private PictureBox image = new PictureBox();   // 이미지
        private bool alive = true; // 살아있는지
        private int revive = 10;            // 부활시간
        private List<Point> monsterBlock;
        private List<Point> blocksPoint;
        private int speed = 10;
        private List<Keys> direction = new List<Keys>();
        private Keys toward;
        private Keys next;
        private bool close = false;
        private string name;
        private Random rand = new Random(Guid.NewGuid().GetHashCode());
        private ImageList imageList;

        public Monster(string name, List<Point> monsterBlock, List<Point> blocksPoint, PictureBox picture, ImageList imgList)
        {
            this.monsterBlock = monsterBlock;
            this.blocksPoint = blocksPoint;
            this.image = picture;
            this.name = name;
            this.imageList = imgList;
            image.Size = new Size(25, 25);
            image.Location = monsterBlock[rand.Next(1,2)];
            image.Visible = true;
            image.Image = imageList.Images[0];
            direction.Add(Keys.Up);
            direction.Add(Keys.Down);
            direction.Add(Keys.Left);
            direction.Add(Keys.Right);
        }

        public void reviving()
        {
            if (revive != 0)
                revive--;
            if (revive == 5)
            {
                image.Location = monsterBlock[rand.Next(1, 2)];
                alive = true;
                //image.Location = monsterBlock[new Random().Next(monsterBlock.Count)];
                image.Visible = true;

                Thread move = new Thread(new ThreadStart(moveComponent));
                move.Start();
            }
            //if (revive == 0)
            //    reviveTime.Stop();
        }
        private bool start = true;
        private void moveComponent()
        {
            Random ran = new Random(Guid.NewGuid().GetHashCode());
            toward = Keys.Up;
            while (!close && alive)
            {
                
                /* NOW */
                if (toward == Keys.Up && image.Location.Y >= 42)
                    if (!collisionImage(0, -2))
                        image.Location = new Point(image.Location.X, image.Location.Y - 2);
                    else
                    {
                        Keys rmv = toward;
                        direction.Remove(rmv);
                        toward = direction[rand.Next(3)];
                        direction.Add(rmv);
                        if (start)
                        {
                            if (rand.NextDouble() >= 0.5)
                                toward = Keys.Left;
                            else
                                toward = Keys.Right;
                            start = false;
                        }
                    }
                else if (toward == Keys.Down && image.Location.Y <= 338)
                    if (!collisionImage(0, 2))
                        image.Location = new Point(image.Location.X, image.Location.Y + 2);
                    else
                    {
                        Keys rmv = toward;
                        direction.Remove(rmv);
                        toward = direction[rand.Next(3)];
                        direction.Add(rmv);
                    }
                else if (toward == Keys.Left && image.Location.X >= 28)
                    if (!collisionImage(-2, 0))
                        image.Location = new Point(image.Location.X - 2, image.Location.Y);
                    else
                    {
                        Keys rmv = toward;
                        direction.Remove(rmv);
                        toward = direction[rand.Next(3)];
                        direction.Add(rmv);
                    }
                else if (toward == Keys.Right && image.Location.X <= 574)
                    if (!collisionImage(2, 0))
                        image.Location = new Point(image.Location.X + 2, image.Location.Y);
                    else
                    {
                        Keys rmv = toward;
                        direction.Remove(rmv);
                        toward = direction[rand.Next(3)];
                        direction.Add(rmv);
                    }
                else
                {
                    Keys rmv = toward;
                    direction.Remove(rmv);
                    toward = direction[rand.Next(3)];
                    direction.Add(rmv);
                }
                Thread.Sleep(speed);
            }
        }

        
        private bool collisionImage(int x, int y)
        {
            Point[] newPos = new Point[4];
            newPos[0] = new Point(image.Location.X + 1, image.Location.Y + 1);
            newPos[1] = new Point(image.Location.X + 23, image.Location.Y + 1);
            newPos[2] = new Point(image.Location.X + 23, image.Location.Y + 23);
            newPos[3] = new Point(image.Location.X + 1, image.Location.Y + 23);


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

        public int getReviveTime()
        {
            return revive;
        }

        public void setImage(Image img)
        {
            image.Image = img;
        }

        public void dead()
        {
            alive = false;
            start = true;
            image.Visible = false;
            image.Location = new Point(0, 0);
            revive = 10;
            reviving();
            //reviveTime.Start();
        }

        public void setClose()
        {
            image.Dispose();
            close = true;
        }

        public Point getPoint()
        {
            return image.Location;
        }

        public void getDizzy()
        {
            speed = 30;
            image.Image = imageList.Images[1];
            Thread.Sleep(4000);
            for (int i = 0; i < 3; i++)
            {
                image.Image = imageList.Images[0];
                Thread.Sleep(300);
                image.Image = imageList.Images[1];
                Thread.Sleep(300);
            }
            image.Image = imageList.Images[0];
            speed = 10;
        }
    }
}
