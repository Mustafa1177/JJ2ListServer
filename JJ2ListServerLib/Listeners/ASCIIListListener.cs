using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace JJ2ListServerLib.Listeners
{
    public class ASCIIListListener
    {
        private TcpListener sckt;
        public ASCIIListListener(int port = 10057)
        {

        }

        public bool StartListener(int port = 10057, IPAddress ipa = null)
        {
            if(sckt != null)
            {
                if (sckt.Server != null )
                    sckt.Server.Close();
                sckt.Stop();
            }
            
            sckt = new TcpListener(ipa == null ? IPAddress.Any : ipa, port);
            return true;
                
        }
    }
}
