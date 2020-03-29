using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using Server;
using System.Collections;
using System.Runtime.Remoting.Channels.Http;
using Microsoft.VisualBasic;

namespace ClientApp
{
    public partial class frmClientUsers : Form
    {
        public static TcpChannel oTcpChannel;
        public static HttpClientChannel oHttpClientChannel;
        public static IRemoteClass oRemoteServer;
        public static Hashtable htOpenChats = new Hashtable();
        public static Hashtable htUserRefObj = new Hashtable();
        public static string sServerKey;
        public static UserClass oMyUser;

        public frmClientUsers()
        {
            InitializeComponent();
        }

        private void LoadUsers()
        {
            try
            {
                object[] lstUsers = oRemoteServer.GetConnectedUser();
                UserClass oUser;

                lstUsuarios.Clear();
                htUserRefObj = new Hashtable();


                for (int i = 0; i < lstUsers.GetLength(0); i++)
                {
                    oUser = (UserClass)lstUsers[i];

                    if (oUser.UserName != oMyUser.UserName && !string.IsNullOrEmpty(oMyUser.UserName))
                    {
                        lstUsuarios.Items.Add(oUser.UserName + " (" + oUser.HostId + ")", (int)oUser.Status);
                        htUserRefObj.Add(oUser.UserName + " (" + oUser.HostId + ")", oUser);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[LoadUsers]" + "\r\n" +
                                                     "Message: " + ex.Message + "\r\n" +
                                                     "StackTrace: " + ex.StackTrace);
                System.Environment.Exit(0);
            }
        }

        private void LoadChatMsgs()
        {
            try
            {
                MessageClass[] lstMessages = oRemoteServer.GetMessages(oMyUser.UserName);
                MessageClass oMsgClass;

                for (int i = 0; i < lstMessages.GetLength(0); i++)
                {
                    oMsgClass = (MessageClass)lstMessages[i];

                    if (htOpenChats.Contains(oMsgClass.FromUserName))
                    {
                        ((frmClientChat)htOpenChats[oMsgClass.FromUserName]).LoadMsg(oMsgClass.FromUserName, oMsgClass.Message);

                        ((frmClientChat)htOpenChats[oMsgClass.FromUserName]).Show();
                        ((frmClientChat)htOpenChats[oMsgClass.FromUserName]).TopMost = true;
                        ((frmClientChat)htOpenChats[oMsgClass.FromUserName]).TopMost = false;
                    }
                    else
                    {
                        htOpenChats.Add(oMsgClass.FromUserName, new frmClientChat(oMyUser, oRemoteServer.GetUserStatus(oMsgClass.FromUserName)));
                        ((frmClientChat)htOpenChats[oMsgClass.FromUserName]).LoadMsg(oMsgClass.FromUserName, oMsgClass.Message);

                        ((frmClientChat)htOpenChats[oMsgClass.FromUserName]).TopMost = true;
                        ((frmClientChat)htOpenChats[oMsgClass.FromUserName]).TopMost = false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[LoadChatMsgs]" + "\r\n" +
                                                     "Message: " + ex.Message + "\r\n" +
                                                     "StackTrace: " + ex.StackTrace);
                System.Environment.Exit(0);
            }
        }

        private void frmClientUsers_Shown(object sender, EventArgs e)
        {
            try
            {
                int iHeight = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
                int iWidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
                this.Height = iHeight;
                this.Left = iWidth - 200;

                cmbStatus.SelectedIndex = 0;
                cmbQuality.SelectedIndex = 5;

                lblUsuario.Text = oMyUser.UserName;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[frmClientUsers_Shown]" + ex.StackTrace);
                System.Environment.Exit(0);
            }
        }

        private void notifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.Visible) { this.Show(); }

            this.TopMost = true;
            this.TopMost = false;
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                frmClientUsers.oRemoteServer.DoChangeStatus(lblUsuario.Text, (enStatus)cmbStatus.SelectedIndex);
                this.LoadUsers();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[cmbStatus_SelectedIndexChanged]" + ex.StackTrace);
            }
        }

        private void timerUsers_Tick(object sender, EventArgs e)
        {
            this.LoadUsers();
            this.LoadChatMsgs();
        }

        private void lstUsuarios_DoubleClick(object sender, EventArgs e)
        {
            if (htOpenChats.Contains(lstUsuarios.FocusedItem.Text))
            {
                ((frmClientChat)htOpenChats[lstUsuarios.FocusedItem.Text]).Show();
                ((frmClientChat)htOpenChats[lstUsuarios.FocusedItem.Text]).TopMost = true;
                ((frmClientChat)htOpenChats[lstUsuarios.FocusedItem.Text]).TopMost = false;
            }
            else
            {
                var vUser = (UserClass)htUserRefObj[lstUsuarios.FocusedItem.Text];
                htOpenChats.Add(lstUsuarios.FocusedItem.Text, new frmClientChat(vUser, oRemoteServer.GetUserStatus(vUser.HostId)));
            }
        }

        private void btnViewServer_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string sServerKey = Interaction.InputBox("Informe a chave de acesso ao servidor", "Visualizar servidor", "", 100, 200);

            //    if (!string.IsNullOrEmpty(sServerKey))
            //    {
            //        Int64 oQuality = Convert.ToInt64(cmbQuality.SelectedItem);
            //        bool bGrayScale = chkGrayScale.Checked;
            //        frmServerScreen oServerScreen = new frmServerScreen(oQuality, bGrayScale, sServerKey);
            //        oServerScreen.Show();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show("[btnViewServer_Click]" + ex.StackTrace);
            //}
        }

        private void frmClientUsers_FormClosed(object sender, FormClosedEventArgs e)
        {
            oRemoteServer.DoLogout(oMyUser.HostId );
            System.Environment.Exit(0);
        }
    }
}
