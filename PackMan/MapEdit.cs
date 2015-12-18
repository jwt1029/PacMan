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
    public partial class MapEdit : Form
    {
        public bool[] map = new bool[300];
        public MapEdit()
        {
            InitializeComponent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            string name = ((PictureBox)sender).Name;
            if (map[int.Parse(name.Substring("picturebox".Length))])
            {
                map[int.Parse(name.Substring("picturebox".Length))] = false;
                ((PictureBox)sender).Image = mapInfo.Images[0];
            }
            else
            {
                map[int.Parse(name.Substring("picturebox".Length))] = true;
                ((PictureBox)sender).Image = mapInfo.Images[1];
            }
        }

        private void saveBt_Click(object sender, EventArgs e)
        {
            string content = "";
            for (int i = 1; i < 300; i++)
                if (map[i])
                    content += " " + i;
            File.WriteAllText("map.txt",content);
            MessageBox.Show("Save Complete!");
        }
    }
}
