namespace ClientApp
{
    partial class frmServerScreen
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
            this.components = new System.ComponentModel.Container();
            this.pict = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).BeginInit();
            this.SuspendLayout();
            // 
            // pict
            // 
            this.pict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pict.Location = new System.Drawing.Point(0, 0);
            this.pict.Name = "pict";
            this.pict.Size = new System.Drawing.Size(292, 273);
            this.pict.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pict.TabIndex = 0;
            this.pict.TabStop = false;
            this.pict.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pict_MouseDoubleClick);
            this.pict.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pict_MouseDown);
            this.pict.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pict_MouseMove);
            this.pict.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pict_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 125;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmServerScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.pict);
            this.Name = "frmServerScreen";
            this.Text = "Visualizando tela de: ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmServerScreen_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmServerScreen_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmServerScreen_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pict)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pict;
        private System.Windows.Forms.Timer timer1;

    }
}