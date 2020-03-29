using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Server;

namespace ClientApp
{
    public partial class frmClientChat : Form
    {
        private UserClass _FromUser;

        public UserClass FromUser
        {
            get { return _FromUser; }
        }

        private string sStatus;
        private string sTitle = "AC86 Support :: ";
        private enStatus eStatus;

        public frmClientChat(UserClass pFromUser, enStatus eStatus)
        {
            InitializeComponent();

            this._FromUser = pFromUser;
            this.eStatus = eStatus;

            SetTitle();

            this.Show();
        }

        private void SetTitle()
        {
            if (eStatus == enStatus.ON) { sStatus = " [Online]"; }
            if (eStatus == enStatus.BUSY) { sStatus = " [Ocupado]"; }
            if (eStatus == enStatus.AWAY) { sStatus = " [Ausente]"; }
            if (eStatus == enStatus.INVISIBLE) { sStatus = " [Off-line]"; }
            if (eStatus == enStatus.OFFLINE) { sStatus = " [Off-line]"; }

            this.Text = sTitle + FromUser.UserName + sStatus;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            frmClientUsers.oRemoteServer.SendMessage(FromUser, frmClientUsers.oMyUser, txtMensagem.Text);

            this.LoadMsg(frmClientUsers.oMyUser.UserName, txtMensagem.Text);

            txtMensagem.Text = string.Empty;
        }

        public void LoadMsg(string sUserName, object oMensagem)
        {
            if (!string.IsNullOrEmpty(oMensagem.ToString()))
            {
                txtMensagens.AppendText(sUserName + " diz: " + "\r\n" + oMensagem + "\r\n" + "\r\n");
                txtMensagens.ScrollToCaret();
                if (txtMensagem.Enabled) { txtMensagem.Focus(); }
            }
        }

        private void frmClientChat_FormClosed(object sender, FormClosedEventArgs e)
        {
            //frmClientUsers.htOpenChats.Remove(sUserName);
        }

        private void timerStatus_Tick(object sender, EventArgs e)
        {
            try
            {
                enStatus tmpStatus = eStatus;
                eStatus = frmClientUsers.oRemoteServer.GetUserStatus(FromUser.UserName);

                if (eStatus == enStatus.OFFLINE)
                {
                    txtMensagem.Enabled = false;
                    btnEnviar.Enabled = false;

                    if (tmpStatus != enStatus.OFFLINE) { LoadMsg("Servidor", "Usuário off-line..."); }
                }
                else
                {
                    txtMensagem.Enabled = true;
                    btnEnviar.Enabled = true;

                    if (tmpStatus == enStatus.OFFLINE && (eStatus != enStatus.OFFLINE && eStatus != enStatus.INVISIBLE))
                    {
                        LoadMsg("Servidor", "Usuário online...");
                    }
                }

                SetTitle();
            }
            catch (Exception ex)
            {
                System.Environment.Exit(0);
            }
        }

        private void frmClientChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void txtMensagem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Hide(); }
        }

        private void frmClientChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { this.Hide(); }
        }

        private void btnTela_Click(object sender, EventArgs e)
        {
            var vUser = frmClientUsers.oMyUser;
            var vRServer = frmClientUsers.oRemoteServer;
            var vSession = vRServer.GetUserSession(vUser.HostId);

            Timer oTimer = new Timer();
            oTimer.Tick += new EventHandler(delegate(object o, EventArgs ea)
            {
                vRServer.SetScreen(vSession, 40L, false);
            });

            oTimer.Interval = 100;
            oTimer.Enabled = true;
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            var vUser = FromUser;
            var vRServer = frmClientUsers.oRemoteServer;
            var vSession = vRServer.GetUserSession(FromUser.HostId);

            new frmServerScreen(vSession).Show();
        }
    }
}
