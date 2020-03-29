using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AC86.ServerApp
{
    public partial class frmMonitor : Form
    {
        static Server.ChannelControlClass oChannelControl;

        public frmMonitor()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void frmMonitor_Shown(object sender, EventArgs e)
        {
            oChannelControl = new Server.ChannelControlClass();
            oChannelControl.CreateServerChannel(System.Windows.Forms.SystemInformation.ComputerName, 3030, "support");
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // Usuários conectados
                Server.UserClass[] a = (Server.UserClass[])Server.ChannelControlClass.oRemoteServer.GetConnectedUser();
                lConnUsers.Items.Clear();
                foreach (var x in a) { lConnUsers.Items.Add(x.UserName + " (" + x.HostId + ")"); }

                // Sessões ativas
                object[] o = Server.ChannelControlClass.oRemoteServer.GetActiveSessions();
                lActSessions.Items.Clear();
                foreach (string x in o) { lActSessions.Items.Add(x); }
            }
            catch { }
        }
    }
}
