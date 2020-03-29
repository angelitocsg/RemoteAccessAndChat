using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class MessageClass : MarshalByRefObject
    {
        private DateTime _SendDate;

        public DateTime SendDate
        {
            get { return _SendDate; }
        }

        private string _FromHostId;

        public string FromHostId
        {
            get { return _FromHostId; }
        }

        private string _FromUserName;

        public string FromUserName
        {
            get { return _FromUserName; }
        }

        private object _Message;

        public object Message
        {
            get { return _Message; }
        }

        public MessageClass(string sFromUserName, string sFromHostId, object oMessage)
        {
            this._FromUserName = sFromUserName;
            this._FromHostId = sFromHostId;
            this._Message = oMessage;
            this._SendDate = DateTime.Now;
        }
    }
}
