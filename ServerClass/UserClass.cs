using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class UserClass : MarshalByRefObject
    {
        private DateTime _LoginDateTime;

        public DateTime LoginDateTime
        {
            get { return _LoginDateTime; }
        }

        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private string _HostId;

        public string HostId
        {
            get { return _HostId; }
        }

        private enStatus _Status;

        public enStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private List<MessageClass> _Messages;

        public List<MessageClass> Messages
        {
            get { return _Messages; }
        }

        public UserClass(string pUserName, string pHostId)
        {
            _UserName = pUserName;
            _HostId = pHostId;
            _Status = enStatus.ON;
            _LoginDateTime = DateTime.Now;
            _Messages = new List<MessageClass>();
        }

        /// <summary>
        /// Nova mensagem
        /// </summary>
        /// <param name="sFromUserName"></param>
        /// <param name="sFromHostId"></param>
        /// <param name="oMessage"></param>
        /// <returns></returns>
        public bool NewMessage(string sFromUserName, string sFromHostId, object oMessage)
        {
            Messages.Add(new MessageClass(sFromUserName, sFromHostId, oMessage));
            return true;
        }

        /// <summary>
        /// Retorna lista de mensagens
        /// </summary>
        /// <returns></returns>
        public List<MessageClass> GetMessages()
        {
            List<MessageClass> lstRetorno = new List<MessageClass>();

            for (int i = 0; i < Messages.Count; i++)
            {
                lstRetorno.Add(Messages[i]);
                Messages.Remove(Messages[i]);
                i--;
            }

            return lstRetorno;
        }
    }
}