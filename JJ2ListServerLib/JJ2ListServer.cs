using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using JJ2ListServerLib.Listeners;
using JJ2ListServerLib.DataClasses;

namespace JJ2ListServerLib
{
    public class JJ2ListServer
    {
        public ServerList ManualServerList { get; set; } = new ServerList();
        public ListServerSettings Settings { get; set; } = new ListServerSettings();
        public ASCIIListListener AsciiListener { get; set; }
        public MOTDListener MotdListener { get; set; }
        public List<string> ExternalListSources { get; set; } = new List<string>();
        public ListCloneClient ListCloner { get; set; } = new ListCloneClient();

        public JJ2ListServer()
        {
            AsciiListener = new ASCIIListListener(ManualServerList);
            MotdListener = new MOTDListener(ManualServerList);
            AsciiListener.OnListRequest += AsciiListener_OnListRequest;
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

            ListCloner.ExternalListSources = this.ExternalListSources;
            ListCloner.StartClient(2 * 60 * 1000);

            return true;
        }

        public ServerList GetCombinedLists(ServerList additionalList = null)
        {
            ServerList res = new ServerList();
            if( additionalList != null)
                res.AddRange(additionalList);
            res.AddRange(ManualServerList);
            res.AddRange(ListCloner.ClonedListResult);
           // res.AddRange(CloneExistingExternalAsciiLists());  
            return res;
        }

        public List<GameServer> CloneExistingExternalAsciiLists(int port = 10057)
        {
            List<GameServer> res = new List<GameServer>();
            foreach(string listServer in ExternalListSources)
            {
                var servers = JJ2ListClient.ParseASCIIList(JJ2ListClient.DownloadASCIIList(listServer, port));
                res.AddRange(servers);
            }
            return res;
        }

        private void AsciiListener_OnListRequest(object sender, EndPoint remoteHost, ref ServerList srcList)
        {
            var listener = (ASCIIListListener)sender;

            //var servers = JJ2ListClient.ParseASCIIList(JJ2ListClient.DownloadASCIIList());
            

            ServerList tempList = GetCombinedLists();
      
            srcList = tempList;
        }


    }
}
