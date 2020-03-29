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
    public partial class frmServerScreen : Form
    {
        private SessionClass oViewSession;

        public frmServerScreen(SessionClass oViewSession)
        {
            this.oViewSession = oViewSession;
            InitializeComponent();
        }

        private static bool _bRecebendo = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_bRecebendo) { return; } 
                
                _bRecebendo = true;

                ImageWrapper oImageWrapper = frmClientUsers.oRemoteServer.GetScreen(oViewSession);

                if (oImageWrapper != null)
                {
                    pict.Image = oImageWrapper.ToImage();
                }
                else
                {
                    timer1.Enabled = false;
                    System.Windows.Forms.MessageBox.Show("Você não tem permissão para essa ação!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                timer1.Enabled = false;
                System.Windows.Forms.MessageBox.Show("[timer1_Tick]" + ex.Message);
                System.Windows.Forms.MessageBox.Show("[timer1_Tick]" + ex.StackTrace);
                System.Environment.Exit(0);
            }

            _bRecebendo = false;
        }

        private void pict_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                int iX = e.X;
                int iY = e.Y;
                try
                {
                    int iImgWidth = pict.Width;
                    int iImgHeight = pict.Height;

                    frmClientUsers.oRemoteServer.SetMousePosition(iX, iY, iImgWidth, iImgHeight);
                }
                catch { }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[pict_MouseMove]" + ex.Message);
                System.Environment.Exit(0);
            }
        }

        private void pict_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                try
                {
                    int iX = e.X;
                    int iY = e.Y;

                    int iImgWidth = pict.Image.Width;
                    int iImgHeight = pict.Image.Height;

                    frmClientUsers.oRemoteServer.SetMouseClick(e.Button, MousePress.CLICK, iX, iY, iImgWidth, iImgHeight);
                    frmClientUsers.oRemoteServer.SetMouseClick(e.Button, MousePress.CLICK, iX, iY, iImgWidth, iImgHeight);
                }
                catch { }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[pict_DoubleClick]" + ex.Message);
            }
        }

        private void pict_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                try
                {
                    int iX = e.X;
                    int iY = e.Y;

                    int iImgWidth = pict.Image.Width;
                    int iImgHeight = pict.Image.Height;

                    frmClientUsers.oRemoteServer.SetMouseClick(e.Button, MousePress.DOWN, iX, iY, iImgWidth, iImgHeight);
                }
                catch { }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[pict_MouseDown]" + ex.Message);
            }
        }

        private void pict_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                try
                {
                    int iX = e.X;
                    int iY = e.Y;

                    int iImgWidth = pict.Image.Width;
                    int iImgHeight = pict.Image.Height;

                    frmClientUsers.oRemoteServer.SetMouseClick(e.Button, MousePress.UP, iX, iY, iImgWidth, iImgHeight);
                }
                catch { }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[pict_MouseUp]" + ex.Message);
            }
        }

        private void frmServerScreen_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                string sKeyPressed = string.Empty;

                sKeyPressed += e.KeyChar;

                frmClientUsers.oRemoteServer.SendKeyPress(sKeyPressed);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("[pict_MouseUp]" + ex.Message);
            }
        }

        private void frmServerScreen_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    string sKeyPressed = string.Empty;

            //    if (e.Alt) { sKeyPressed += "{ALTDOWN}"; }
            //    if (e.Shift) { sKeyPressed += "{SHIFTDOWN}"; }
            //    if (e.Control) { sKeyPressed += "{CTRLDOWN}"; }

            //    frmClientUsers.oRemoteServer.SendKeyPress(sKeyPressed);
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show("[pict_MouseUp]" + ex.Message);
            //}
        }

        private void frmServerScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    string sKeyPressed = string.Empty;

            //    if (e.Alt) { sKeyPressed += "{ALTUP}"; }
            //    if (e.Shift) { sKeyPressed += "{SHIFTUP}"; }
            //    if (e.Control) { sKeyPressed += "{CTRLUP}"; }

            //    frmClientUsers.oRemoteServer.SendKeyPress(sKeyPressed);
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show("[pict_MouseUp]" + ex.Message);
            //}
        }
    }
}