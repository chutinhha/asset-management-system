using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace AssMngSys
{
    class RecvEventArgs : EventArgs
    {
        private string sPid;
        private string sTid;
        public RecvEventArgs(string sPid, string sTid)
        {
            this.sPid = sPid;
            this.sTid = sTid;
        }
        public string SPid
        {
            get
            {
                return sPid;
            }
        }
        public string STid
        {
            get
            {
                return sTid;
            }
        }
    }
    class UdpState
    {
        public UdpClient u;
        public IPEndPoint e;
    }
}