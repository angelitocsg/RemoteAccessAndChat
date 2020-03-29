using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public static class Extensions
    {
        public static UserClass Add(this List<UserClass> pUsers, string pUserName, string pHostId)
        {
            UserClass oUser = new UserClass(pUserName, pHostId);
            pUsers.Add(oUser);
            return oUser;
        }
        /// <summary>
        /// Adicionar sessão
        /// </summary>
        /// <param name="pSessions">Sessões</param>
        /// <param name="pSessionType">Tipo</param>
        /// <param name="pUser">Usuário dono da sessão</param>
        /// <returns></returns>
        public static SessionClass Add(this List<SessionClass> pSessions, eSessionType pSessionType, UserClass pUser)
        {
            SessionClass oSession = new SessionClass(pSessionType, pUser);
            if (pUser != null) { oSession.Users.Add(pUser); }
            pSessions.Add(oSession);
            return oSession;
        }
    }
}