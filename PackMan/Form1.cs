﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PackMan
{
    public partial class Form1 : Form
    {
        private int egg = 0;
        public Form1()
        {
            InitializeComponent();
        }
        GameField gm;
        private void button1_Click(object sender, EventArgs e)
        {
            gm = new GameField();
            gm.Show();
            WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
            wplayer.URL = "Melody of The night.mp3";
            wplayer.controls.play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(gm != null)
                gm.Close();
            this.Close();
        }

        private void mapBt_Click(object sender, EventArgs e)
        {
            MapEdit edit = new MapEdit();
            edit.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            egg++;
            if(egg == 5)
                mapBt.Visible = true;
        }
    }
}
