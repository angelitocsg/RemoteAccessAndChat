using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Server
{
    public interface IRemoteClass
    {
        /// <summary>
        /// Conectar-se ao servidor
        /// </summary>
        /// <param name="sUserName"></param>
        /// <param name="sHostId"></param>
        /// <returns></returns>
        bool DoLogin(string sUserName, string sHostId);
        /// <summary>
        /// Sair da sessão
        /// </summary>
        /// <param name="sHostId"></param>
        /// <returns></returns>
        bool DoLogout(string sHostId);
        /// <summary>
        /// Obter sessão do servidor
        /// </summary>
        /// <returns></returns>
        SessionClass GetServerSession();
        /// <summary>
        /// Obter sessão do usuário
        /// </summary>
        /// <returns></returns>
        SessionClass GetUserSession(string sHostId);
        /// <summary>
        /// Entrar numa sessão especifica
        /// </summary>
        /// <param name="User"></param>
        /// <param name="SessionId"></param>
        /// <returns></returns>
        bool EnterSession(UserClass User, SessionClass Session);

        /// <summary>
        /// Mudar status
        /// </summary>
        /// <param name="sHostId"></param>
        /// <param name="sStatus"></param>
        /// <returns></returns>
        bool DoChangeStatus(string sHostId, enStatus eStatus);
        /// <summary>
        /// Verifica se o usuário está online
        /// </summary>
        /// <param name="sHostId"></param>
        /// <returns></returns>
        enStatus GetUserStatus(string sHostId);
        /// <summary>
        /// Obter objeto do usuário
        /// </summary>
        /// <param name="sHostId"></param>
        /// <returns></returns>
        UserClass GetUser(string sHostId);
        /// <summary>
        /// Lista de usuários conectados
        /// </summary>
        /// <returns></returns>
        object[] GetConnectedUser();
        /// <summary>
        /// Lista de sessões ativas
        /// </summary>
        /// <returns></returns>
        object[] GetActiveSessions();
        /// <summary>
        /// Enviar mensagem
        /// </summary>
        /// <param name="oFromUser"></param>
        /// <param name="oToUser"></param>
        /// <param name="oMessage"></param>
        /// <returns></returns>
        bool SendMessage(UserClass oFromUser, UserClass oToUser, object oMessage);
        /// <summary>
        /// Recupera mensagens do usuário
        /// </summary>
        /// <param name="sHostId"></param>
        /// <returns></returns>
        MessageClass[] GetMessages(string sHostId);

        /// <summary>
        /// Muda posição do mouse
        /// </summary>
        /// <param name="iX"></param>
        /// <param name="iY"></param>
        void SetMousePosition(int iX, int iY, int iImgWidth, int iImgHeight);
        /// <summary>
        /// Click do mouse
        /// </summary>
        /// <param name="iX"></param>
        /// <param name="iY"></param>
        /// <param name="iImgWidth"></param>
        /// <param name="iImgHeight"></param>
        void SetMouseClick(MouseButtons eMouseButtons, MousePress eMousePress, int iX, int iY, int iImgWidth, int iImgHeight);
        /// <summary>
        /// Digitação
        /// </summary>
        /// <param name="sKeyPressed"></param>
        void SendKeyPress(string sKeyPressed);

        /// <summary>
        /// Salva tela no buffer da sessão
        /// </summary>
        /// <returns></returns>
        bool SetScreen(SessionClass oSession, Int64 oQuality, bool bGrayScale);

        /// <summary>
        /// Recupera tela do buffer da sessão
        /// </summary>
        /// <returns></returns>
        ImageWrapper GetScreen(SessionClass oSession);
    }
}
