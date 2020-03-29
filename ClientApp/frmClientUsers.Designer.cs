namespace ClientApp
{
    partial class frmClientUsers
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Online", System.Windows.Forms.HorizontalAlignment.Left);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientUsers));
            this.lstUsuarios = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lblUsuario = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.timerUsers = new System.Windows.Forms.Timer(this.components);
            this.btnViewServer = new System.Windows.Forms.Button();
            this.cmbQuality = new System.Windows.Forms.ComboBox();
            this.chkGrayScale = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lstUsuarios
            // 
            this.lstUsuarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstUsuarios.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstUsuarios.FullRowSelect = true;
            this.lstUsuarios.GridLines = true;
            listViewGroup1.Header = "Online";
            listViewGroup1.Name = "vwGrpOnline";
            listViewGroup1.Tag = "Online";
            this.lstUsuarios.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.lstUsuarios.LargeImageList = this.imageList;
            this.lstUsuarios.Location = new System.Drawing.Point(0, 91);
            this.lstUsuarios.MultiSelect = false;
            this.lstUsuarios.Name = "lstUsuarios";
            this.lstUsuarios.Size = new System.Drawing.Size(192, 182);
            this.lstUsuarios.SmallImageList = this.imageList;
            this.lstUsuarios.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstUsuarios.TabIndex = 0;
            this.lstUsuarios.UseCompatibleStateImageBehavior = false;
            this.lstUsuarios.View = System.Windows.Forms.View.List;
            this.lstUsuarios.DoubleClick += new System.EventHandler(this.lstUsuarios_DoubleClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "msn-online.png");
            this.imageList.Images.SetKeyName(1, "msn-ocupado.png");
            this.imageList.Images.SetKeyName(2, "msn-ausente.png");
            this.imageList.Images.SetKeyName(3, "msn-invisivel.png");
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Clique no usuário para iniciar o bate-papo";
            this.notifyIcon.BalloonTipTitle = "AC86 Support";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseMove);
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(3, 8);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(0, 18);
            this.lblUsuario.TabIndex = 1;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Online",
            "Ocupado",
            "Ausente",
            "Invisível"});
            this.cmbStatus.Location = new System.Drawing.Point(0, 35);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(192, 21);
            this.cmbStatus.TabIndex = 2;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // timerUsers
            // 
            this.timerUsers.Enabled = true;
            this.timerUsers.Interval = 10000;
            this.timerUsers.Tick += new System.EventHandler(this.timerUsers_Tick);
            // 
            // btnViewServer
            // 
            this.btnViewServer.Location = new System.Drawing.Point(0, 62);
            this.btnViewServer.Name = "btnViewServer";
            this.btnViewServer.Size = new System.Drawing.Size(99, 23);
            this.btnViewServer.TabIndex = 3;
            this.btnViewServer.Text = "Visualizar servidor";
            this.btnViewServer.UseVisualStyleBackColor = true;
            this.btnViewServer.Click += new System.EventHandler(this.btnViewServer_Click);
            // 
            // cmbQuality
            // 
            this.cmbQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuality.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbQuality.FormattingEnabled = true;
            this.cmbQuality.Items.AddRange(new object[] {
            ((long)(5)),
            ((long)(10)),
            ((long)(25)),
            ((long)(40)),
            ((long)(50)),
            ((long)(75)),
            ((long)(100))});
            this.cmbQuality.Location = new System.Drawing.Point(105, 64);
            this.cmbQuality.Name = "cmbQuality";
            this.cmbQuality.Size = new System.Drawing.Size(59, 21);
            this.cmbQuality.TabIndex = 4;
            // 
            // chkGrayScale
            // 
            this.chkGrayScale.AutoSize = true;
            this.chkGrayScale.Location = new System.Drawing.Point(171, 67);
            this.chkGrayScale.Name = "chkGrayScale";
            this.chkGrayScale.Size = new System.Drawing.Size(15, 14);
            this.chkGrayScale.TabIndex = 5;
            this.chkGrayScale.UseVisualStyleBackColor = true;
            // 
            // frmClientUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 273);
            this.Controls.Add(this.chkGrayScale);
            this.Controls.Add(this.cmbQuality);
            this.Controls.Add(this.btnViewServer);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.lstUsuarios);
            this.MaximizeBox = false;
            this.Name = "frmClientUsers";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Usuários conectados";
            this.Shown += new System.EventHandler(this.frmClientUsers_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmClientUsers_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lstUsuarios;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Timer timerUsers;
        private System.Windows.Forms.Button btnViewServer;
        private System.Windows.Forms.ComboBox cmbQuality;
        private System.Windows.Forms.CheckBox chkGrayScale;
    }
}

