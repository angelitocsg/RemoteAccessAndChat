using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;

namespace Server
{
    public class ChannelControlClass
    {
        public static IRemoteClass oRemoteServer;

        /// <summary>
        /// Server
        /// </summary>
        /// <param name="sServerName"></param>
        /// <param name="iPort"></param>
        /// <param name="sAppURI"></param>
        public void CreateServerChannel(string sServerName, int iPort, string sAppURI)
        {
            try
            {
                //string sComputerName = System.Windows.Forms.SystemInformation.ComputerName;
                IPHostEntry oIPHostEntry = Dns.GetHostEntry(sServerName);
                IPAddress[] oIPAddress = oIPHostEntry.AddressList;

                RemotingConfiguration.Configure("Remoting.config", false);
                HttpServerChannel oHttpServerChannel = new HttpServerChannel(iPort);
                ChannelServices.RegisterChannel(oHttpServerChannel, false);
                RemotingConfiguration.RegisterWellKnownServiceType(System.Type.GetType("Server.ServerClass, Server"), sAppURI, WellKnownObjectMode.Singleton);

                oRemoteServer = (IRemoteClass)Activator.GetObject(typeof(IRemoteClass), "http://" + sServerName + ":" + iPort + "/" + sAppURI);
            }
            catch { }
        }
    }
}
