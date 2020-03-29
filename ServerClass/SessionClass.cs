using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public enum eSessionType
    {
        Server,
        Client
    }

    public class BufferClass : MarshalByRefObject
    {
        private int ReadPos = 0;
        private int WritePos = 0;
        private int FramePerSec = 10; // 10 frames
        private int BufferSize = 5; // 5 segundos
        private Dictionary<int, byte[]> Buffer;

        public BufferClass()
        {
            Buffer = new Dictionary<int,byte[]>();
        }

        public void Write(byte[] bBytes)
        {
            if (Buffer.ContainsKey(WritePos))
            {
                Buffer[WritePos] = bBytes;
            }
            else
            {
                Buffer.Add(WritePos, bBytes);
            }
            WritePos++;

            if (WritePos > (FramePerSec * BufferSize)) { WritePos = 0; }
        }

        public byte[] Read()
        {
            byte[] bResult = null;

            if (Buffer.ContainsKey(ReadPos))
            {
                bResult= Buffer[ReadPos];
            }
            ReadPos++;

            if (ReadPos > (FramePerSec * BufferSize)) { ReadPos = 0; }

            return bResult;
        }
    }

    [Serializable]
    public class SessionClass : MarshalByRefObject
    {
        private string _SessionId;

        public string SessionId
        {
            get { return _SessionId; }
        }

        private eSessionType _SessionType;

        public eSessionType SessionType
        {
            get { return _SessionType; }
        }

        private DateTime _SessionDateTime;

        public DateTime SessionDateTime
        {
            get { return _SessionDateTime; }
        }

        private UserClass _SessionUser;

        public UserClass SessionUser
        {
            get { return _SessionUser; }
        }

        private BufferClass _SessionBuffer;

        public BufferClass SessionBuffer
        {
            get { return _SessionBuffer; }
        }

        private List<UserClass> _Users;

        public List<UserClass> Users
        {
            get { return _Users; }
        }

        public SessionClass(eSessionType SessionType, UserClass SessionUser)
        {
            _SessionUser = SessionUser;
            _SessionId = System.Guid.NewGuid().ToString();
            _SessionDateTime = DateTime.Now;
            _SessionBuffer = new BufferClass();
            _Users = new List<UserClass>();
        }
    }
}
