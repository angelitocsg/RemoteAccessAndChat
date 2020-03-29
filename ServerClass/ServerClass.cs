using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Server
{
    public enum enStatus
    {
        ON = 0,
        BUSY = 1,
        AWAY = 2,
        INVISIBLE = 3,
        OFFLINE = 4
    }

    public class ServerClass : MarshalByRefObject, IRemoteClass
    {
        /// <summary>
        /// Sessões
        /// </summary>
        private static List<SessionClass> _Sessions;

        /// <summary>
        /// Sessões
        /// </summary>
        public static List<SessionClass> Sessions
        {
            get { return _Sessions; }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public ServerClass()
        {
            _Sessions = new List<SessionClass>();
            _Sessions.Add(eSessionType.Server, null);
        }

        #region IRemoteClass Members

        /// <summary>
        /// Obter sessão do servidor
        /// </summary>
        /// <returns></returns>
        public SessionClass GetServerSession()
        {
            SessionClass oResult = (from a in Sessions
                                    where a.SessionType == eSessionType.Server
                                    select a).FirstOrDefault();

            return oResult;
        }

        /// <summary>
        /// Obter sessão do servidor
        /// </summary>
        /// <returns></returns>
        public SessionClass GetUserSession(string sHostId)
        {
            SessionClass oResult = (from a in Sessions
                                    where a.SessionType == eSessionType.Client
                                    from b in a.Users
                                    where b.HostId == sHostId
                                    select a).FirstOrDefault();

            return oResult;
        }

        /// <summary>
        /// Entrar na sessão
        /// </summary>
        /// <param name="sUserName"></param>
        /// <returns></returns>
        public bool DoLogin(string sUserName, string sHostId)
        {
            var vIsConnected = (from a in Sessions
                                where a.SessionType == eSessionType.Server
                                from b in a.Users
                                where b.HostId == sHostId
                                select b).FirstOrDefault();

            if (vIsConnected != null)
            {
                throw (new Exception("Há outro usuário conectado com o mesmo nome!"));
            }
            else
            {
                var vUser = GetServerSession().Users.Add(sUserName, sHostId);
                Sessions.Add(eSessionType.Client, vUser);
            }

            return true;
        }

        /// <summary>
        /// Entrar numa sessão especifica
        /// </summary>
        /// <param name="User"></param>
        /// <param name="Session"></param>
        /// <returns></returns>
        public bool EnterSession(UserClass User, SessionClass Session)
        {
            Session.Users.Add(User);
            return true;
        }

        /// <summary>
        /// Mudar status
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="eStatus"></param>
        /// <returns></returns>
        public bool DoChangeStatus(string sHostId, enStatus eStatus)
        {
            var vIsConnected = (from a in Sessions
                                where a.SessionType == eSessionType.Server
                                from b in a.Users
                                where b.HostId == sHostId
                                select b).FirstOrDefault();

            // Valida se o usuário está conectado
            if (vIsConnected != null)
            {
                vIsConnected.Status = eStatus;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Sair da sessão
        /// </summary>
        /// <param name="sHostId"></param>
        /// <returns></returns>
        public bool DoLogout(string sHostId)
        {
            var vConnectedSessions = from a in Sessions
                                     from b in a.Users
                                     where b.HostId == sHostId
                                     select new { Session = a, User = b };

            var vUserSession = (from a in Sessions
                                where a.SessionUser.HostId == sHostId
                                select a).FirstOrDefault();

            // Remove usuário das sessões
            foreach (var x in vConnectedSessions)
            {
                x.Session.Users.Remove(x.User);
            }

            // Remove sessão privada do usuário
            if (vUserSession != null) { Sessions.Remove(vUserSession); }

            return true;
        }

        /// <summary>
        /// Verifica se o usuário está online
        /// </summary>
        /// <param name="sHostId"></param>
        /// <returns></returns>
        public enStatus GetUserStatus(string sHostId)
        {
            var vIsConnected = (from a in Sessions
                                where a.SessionType == eSessionType.Server
                                from b in a.Users
                                where b.HostId == sHostId
                                select b).FirstOrDefault();

            if (vIsConnected != null)
            {
                return vIsConnected.Status;
            }
            else
            {
                return enStatus.OFFLINE;
            }
        }

        /// <summary>
        /// Obter objeto do usuário
        /// </summary>
        /// <param name="sHostId"></param>
        /// <returns></returns>
        public UserClass GetUser(string sHostId)
        {
            var vIsConnected = (from a in Sessions
                                where a.SessionType == eSessionType.Server
                                from b in a.Users
                                where b.HostId == sHostId
                                select b).FirstOrDefault();

            if (vIsConnected != null)
            {
                return vIsConnected;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Lista de usuários conectados
        /// </summary>
        /// <returns></returns>
        public object[] GetConnectedUser()
        {
            var vListUsers = (from a in Sessions
                              where a.SessionType == eSessionType.Server
                              from b in a.Users
                              select b).ToList();

            List<UserClass> lstUsers = new List<UserClass>();

            foreach (var vUser in vListUsers)
            {
                // Conectado a mais de 30 minutos, remove sessão
                if (vUser.LoginDateTime < DateTime.Now.AddMinutes(-30))
                {
                    DoLogout(vUser.HostId);
                }
                else
                    // Não está invisível
                    if (vUser.Status != enStatus.INVISIBLE)
                    {
                        lstUsers.Add(vUser);
                    }
            }

            return lstUsers.ToArray();
        }

        /// <summary>
        /// Lista de sessões ativas
        /// </summary>
        /// <returns></returns>
        public object[] GetActiveSessions()
        {
            var vListSessions = (from a in Sessions
                                 select a).ToList();

            List<string> lstSessions = new List<string>();

            foreach (var vSession in vListSessions)
            {
                if (vSession.SessionType == eSessionType.Server)
                    lstSessions.Add(vSession.SessionType + ": " + vSession.SessionId);
                else
                    lstSessions.Add(vSession.SessionType + ": " + vSession.SessionUser.HostId);
            }

            return lstSessions.ToArray();
        }

        /// <summary>
        /// Enviar mensagem
        /// </summary>
        /// <param name="oMessage"></param>
        /// <param name="sToUser"></param>
        /// <returns></returns>
        public bool SendMessage(UserClass oFromUser, UserClass oToUser, object oMessage)
        {
            var vUser = (from a in Sessions
                         where a.SessionType == eSessionType.Server // nessa sessão
                         from b in a.Users
                         where b.HostId == oToUser.HostId // para esse usuário
                         select b).FirstOrDefault();

            if (vUser != null)
            {
                vUser.NewMessage(oFromUser.UserName, oFromUser.HostId, oMessage);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Recupera mensagens do usuário
        /// </summary>
        /// <param name="sHostId"></param>
        /// <returns></returns>
        public MessageClass[] GetMessages(string sHostId)
        {
            List<MessageClass> lstRetorno = new List<MessageClass>();

            var vIsConnected = (from a in Sessions
                                where a.SessionType == eSessionType.Server
                                from b in a.Users
                                where b.HostId == sHostId
                                select b).FirstOrDefault();

            if (vIsConnected != null)
            {
                lstRetorno = vIsConnected.GetMessages();
            }

            return lstRetorno.ToArray();
        }

        #endregion

        /// <summary>
        /// Identificação do client/server
        /// </summary>
        /// <returns></returns>
        public static string GetHostId()
        {
            string sMachineName = System.Environment.MachineName;
            string sOSVersion = System.Environment.OSVersion.VersionString;
            string sHASHMD5 = CalculateMD5Hash(sMachineName + sOSVersion).Substring(0, 9);
            string sServerKey = sHASHMD5.Substring(0, 3) + "-" + sHASHMD5.Substring(3, 3) + "-" + sHASHMD5.Substring(6, 3);
            return sServerKey;
        }

        private static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Calcula posição relativa da tela do cliente com a tela do server
        /// </summary>
        /// <param name="iX"></param>
        /// <param name="iY"></param>
        /// <param name="iImgWidth"></param>
        /// <param name="iImgHeight"></param>
        private void CalcRelativePosition(ref int iX, ref  int iY, int iImgWidth, int iImgHeight)
        {
            // Calcular posição relativa a imagem do client
            int iScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int iScreenHeight = Screen.PrimaryScreen.Bounds.Height;

            double iFatorWidth = ((double)iScreenWidth * 100) / (double)iImgWidth;
            double iFatorHeight = ((double)iScreenHeight * 100) / (double)iImgHeight;

            iX = Convert.ToInt32(iX * ((double)iFatorWidth / 100));
            iY = Convert.ToInt32(iY * ((double)iFatorHeight / 100));
        }

        #region IRemoteClass Members

        /// <summary>
        /// Muda posição do mouse
        /// </summary>
        /// <param name="iX"></param>
        /// <param name="iY"></param>
        public void SetMousePosition(int iX, int iY, int iImgWidth, int iImgHeight)
        {
            try
            {
                CalcRelativePosition(ref iX, ref iY, iImgWidth, iImgHeight);

                Cursor.Position = new Point(iX, iY);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Clicar com o mouse
        /// </summary>
        public void SetMouseClick(MouseButtons eMouseButtons, MousePress eMousePress, int iX, int iY, int iImgWidth, int iImgHeight)
        {
            try
            {
                CalcRelativePosition(ref iX, ref iY, iImgWidth, iImgHeight);

                if (eMouseButtons == MouseButtons.Left)
                {
                    switch (eMousePress)
                    {
                        case MousePress.DOWN: WindowsControlClass.mouse_event((uint)WindowsControlClass.MouseEventTFlags.LEFTDOWN, (uint)iX, (uint)iY, 0, UIntPtr.Zero);
                            break;
                        case MousePress.UP: WindowsControlClass.mouse_event((uint)WindowsControlClass.MouseEventTFlags.LEFTUP, (uint)iX, (uint)iY, 0, UIntPtr.Zero);
                            break;
                        case MousePress.CLICK:
                            WindowsControlClass.mouse_event((uint)WindowsControlClass.MouseEventTFlags.LEFTDOWN, (uint)iX, (uint)iY, 0, UIntPtr.Zero);
                            WindowsControlClass.mouse_event((uint)WindowsControlClass.MouseEventTFlags.LEFTUP, (uint)iX, (uint)iY, 0, UIntPtr.Zero);
                            break;
                    }
                }
                else
                    if (eMouseButtons == MouseButtons.Right)
                    {
                        switch (eMousePress)
                        {
                            case MousePress.DOWN: WindowsControlClass.mouse_event((uint)WindowsControlClass.MouseEventTFlags.RIGHTDOWN, (uint)iX, (uint)iY, 0, UIntPtr.Zero);
                                break;
                            case MousePress.UP: WindowsControlClass.mouse_event((uint)WindowsControlClass.MouseEventTFlags.RIGHTUP, (uint)iX, (uint)iY, 0, UIntPtr.Zero);
                                break;
                            case MousePress.CLICK:
                                WindowsControlClass.mouse_event((uint)WindowsControlClass.MouseEventTFlags.RIGHTDOWN, (uint)iX, (uint)iY, 0, UIntPtr.Zero);
                                WindowsControlClass.mouse_event((uint)WindowsControlClass.MouseEventTFlags.RIGHTUP, (uint)iX, (uint)iY, 0, UIntPtr.Zero);
                                break;
                        }
                    }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            System.Console.WriteLine("Botão do mouse pressionado...");
        }

        /// <summary>
        /// Digitação
        /// </summary>
        /// <param name="sKeyPressed"></param>
        public void SendKeyPress(string sKeyPressed)
        {
            try
            {
                IntPtr oIntPtr = WindowsControlClass.GetForegroundWindow();
                WindowsControlClass.SetForegroundWindow(oIntPtr);
                SendKeys.SendWait(sKeyPressed);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            System.Console.WriteLine("Tecla [" + sKeyPressed + "] pressionada...");
        }

        /// <summary>
        /// Salva tela no buffer da sessão
        /// </summary>
        /// <returns></returns>
        public bool SetScreen(SessionClass oSession, Int64 oQuality, bool bGrayScale)
        {
            bool bResult = true;
            ImageWrapper oImageWrapper = null;

            Graphics oGraphics;
            int iScreenWidth = Screen.PrimaryScreen.Bounds.Width;
            int iScreenHeight = Screen.PrimaryScreen.Bounds.Height;
            //armazena a imagem no bitmap
            Bitmap oBitmap = new Bitmap(iScreenWidth, iScreenHeight);
            //copia  a tela no bitmap
            oGraphics = Graphics.FromImage(oBitmap);
            oGraphics.CopyFromScreen(Point.Empty, Point.Empty, Screen.PrimaryScreen.Bounds.Size);
            //atribui a imagem ao picturebox exibindo-a
            oBitmap.SetResolution(72, 72);
            oImageWrapper = new ImageWrapper(oBitmap, oQuality, bGrayScale);

            oSession.SessionBuffer.Write(oImageWrapper.Bytes);

            return bResult;
        }

        /// <summary>
        /// Recupera tela do buffer da sessão
        /// </summary>
        /// <param name="oSession"></param>
        /// <returns></returns>
        public ImageWrapper GetScreen(SessionClass oSession)
        {
            ImageWrapper oImageWrapper = new ImageWrapper(oSession.SessionBuffer.Read());
            return oImageWrapper;
        }

        #endregion
    }
}