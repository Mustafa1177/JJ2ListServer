using System;
using System.Linq;
using System.Net;
using JJ2ListServerLib.Listeners;
namespace JJ2ListServerLib
{
    public class JJ2ListServer
    {
        public ServerList ServerList { get; set; } = new ServerList();
        public ListServerSettings Settings { get; set; } = new ListServerSettings();
        public ASCIIListListener AsciiListener { get; set; }
        public MOTDListener MotdListener { get; set; }

        public JJ2ListServer()
        {
            AsciiListener = new ASCIIListListener(ServerList);
            MotdListener = new MOTDListener(ServerList);
        }

        public bool StartServer(int binaryListPort = 10053, int GameServerRegisterationPort = 10054, int serverStatusPort = 10055, int listSynchronizationPort = -1, int asciiListPort = 10057, int motdPort = 10058, int jsonList = 10060, IPAddress ipa = null)
        {
            if (asciiListPort >= 0)
            {
                AsciiListener.StartListener(asciiListPort, ipa);
            }

            if (motdPort >= 0)
            {
                MotdListener.StartListener(motdPort, ipa);
            }

            return true;
        }

    }
}
