namespace PackMan
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.startBt = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.exitBt = new System.Windows.Forms.Button();
            this.mapBt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // startBt
            // 
            this.startBt.Location = new System.Drawing.Point(12, 211);
            this.startBt.Name = "startBt";
            this.startBt.Size = new System.Drawing.Size(75, 23);
            this.startBt.TabIndex = 0;
            this.startBt.Text = "START";
            this.startBt.UseVisualStyleBackColor = true;
            this.startBt.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(91, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Stencil Std", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 49);
            this.label1.TabIndex = 2;
            this.label1.Text = "PAC MAN";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // exitBt
            // 
            this.exitBt.Location = new System.Drawing.Point(197, 211);
            this.exitBt.Name = "exitBt";
            this.exitBt.Size = new System.Drawing.Size(75, 23);
            this.exitBt.TabIndex = 2;
            this.exitBt.Text = "EXIT";
            this.exitBt.UseVisualStyleBackColor = true;
            this.exitBt.Click += new System.EventHandler(this.button2_Click);
            // 
            // mapBt
            // 
            this.mapBt.Location = new System.Drawing.Point(104, 211);
            this.mapBt.Name = "mapBt";
            this.mapBt.Size = new System.Drawing.Size(75, 23);
            this.mapBt.TabIndex = 1;
            this.mapBt.Text = "MAP EDIT";
            this.mapBt.UseVisualStyleBackColor = true;
            this.mapBt.Visible = false;
            this.mapBt.Click += new System.EventHandler(this.mapBt_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.mapBt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.exitBt);
            this.Controls.Add(this.startBt);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startBt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button exitBt;
        private System.Windows.Forms.Button mapBt;
    }
}

