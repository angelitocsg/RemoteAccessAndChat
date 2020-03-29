using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using Server;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting;
using System.Net;

namespace ClientApp
{
    public partial class frmClientLogin : Form
    {
        public frmClientLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos
                if (!string.IsNullOrEmpty(txtServidor.Text))
                    if (!string.IsNullOrEmpty(txtPorta.Text))
                        if (!string.IsNullOrEmpty(txtApp.Text))
                            if (!string.IsNullOrEmpty(txtUsuario.Text))
                            {
                                Properties.Settings.Default.Usuario = txtUsuario.Text;
                                Properties.Settings.Default.Save();

                                // Conectar
                                RemotingConfiguration.Configure("Remoting.config", false);
                                frmClientUsers.oHttpClientChannel = new HttpClientChannel();
                                ChannelServices.RegisterChannel(frmClientUsers.oHttpClientChannel, false);

                                frmClientUsers.oRemoteServer = (IRemoteClass)Activator.GetObject(typeof(IRemoteClass), "http://" + txtServidor.Text + ":" + txtPorta.Text + "/" + txtApp.Text);
                                frmClientUsers.oRemoteServer.DoLogin(txtUsuario.Text, Server.ServerClass.GetHostId());

                                if (frmClientUsers.oRemoteServer == null)
                                {
                                    System.Windows.Forms.MessageBox.Show("Erro ao conectar!");
                                    ChannelServices.UnregisterChannel(frmClientUsers.oHttpClientChannel);
                                }
                                else
                                {
                                    frmClientUsers.oMyUser = frmClientUsers.oRemoteServer.GetUser(Server.ServerClass.GetHostId());
                                    new frmClientUsers().Show();
                                    this.Hide();
                                }
                            }
                            else
                                System.Windows.Forms.MessageBox.Show("Preencha o usuário!");
                        else
                            System.Windows.Forms.MessageBox.Show("Preencha a aplicação!");
                    else
                        System.Windows.Forms.MessageBox.Show("Preencha a porta!");
                else
                    System.Windows.Forms.MessageBox.Show("Preencha o servidor!");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void frmClientLogin_Shown(object sender, EventArgs e)
        {
            try
            {
                txtServidor.Text = Properties.Settings.Default.Server;
                txtPorta.Text = Properties.Settings.Default.Port.ToString();
                txtApp.Text = Properties.Settings.Default.AppURI;
                txtUsuario.Text = Properties.Settings.Default.Usuario;
            }
            catch { }
        }
    }
}
