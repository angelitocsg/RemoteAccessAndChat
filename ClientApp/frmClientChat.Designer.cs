namespace ClientApp
{
    partial class frmClientChat
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
            this.txtMensagens = new System.Windows.Forms.RichTextBox();
            this.txtMensagem = new System.Windows.Forms.RichTextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.timerStatus = new System.Windows.Forms.Timer(this.components);
            this.btnTela = new System.Windows.Forms.Button();
            this.btnVer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMensagens
            // 
            this.txtMensagens.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMensagens.BackColor = System.Drawing.SystemColors.Window;
            this.txtMensagens.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMensagens.Location = new System.Drawing.Point(0, 2);
            this.txtMensagens.Name = "txtMensagens";
            this.txtMensagens.ReadOnly = true;
            this.txtMensagens.Size = new System.Drawing.Size(432, 277);
            this.txtMensagens.TabIndex = 1;
            this.txtMensagens.Text = "";
            // 
            // txtMensagem
            // 
            this.txtMensagem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMensagem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMensagem.Location = new System.Drawing.Point(0, 285);
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(349, 66);
            this.txtMensagem.TabIndex = 0;
            this.txtMensagem.Text = "";
            this.txtMensagem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMensagem_KeyDown);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnviar.Location = new System.Drawing.Point(355, 285);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(77, 34);
            this.btnEnviar.TabIndex = 2;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // timerStatus
            // 
            this.timerStatus.Enabled = true;
            this.timerStatus.Interval = 2000;
            this.timerStatus.Tick += new System.EventHandler(this.timerStatus_Tick);
            // 
            // btnTela
            // 
            this.btnTela.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTela.Location = new System.Drawing.Point(355, 325);
            this.btnTela.Name = "btnTela";
            this.btnTela.Size = new System.Drawing.Size(37, 21);
            this.btnTela.TabIndex = 3;
            this.btnTela.Text = "Tela";
            this.btnTela.UseVisualStyleBackColor = true;
            this.btnTela.Click += new System.EventHandler(this.btnTela_Click);
            // 
            // btnVer
            // 
            this.btnVer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVer.Location = new System.Drawing.Point(395, 325);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(37, 21);
            this.btnVer.TabIndex = 4;
            this.btnVer.Text = "Ver";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // frmClientChat
            // 
            this.AcceptButton = this.btnEnviar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 350);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.btnTela);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtMensagem);
            this.Controls.Add(this.txtMensagens);
            this.Name = "frmClientChat";
            this.Text = "AC86 Support";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmClientChat_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmClientChat_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmClientChat_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtMensagem;
        private System.Windows.Forms.Button btnEnviar;
        public System.Windows.Forms.RichTextBox txtMensagens;
        private System.Windows.Forms.Timer timerStatus;
        private System.Windows.Forms.Button btnTela;
        private System.Windows.Forms.Button btnVer;
    }
}