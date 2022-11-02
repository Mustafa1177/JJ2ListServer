using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;
using JJ2ListServerLib.Listeners;
using JJ2ListServerLib.DataClasses;

namespace JJ2ListServerLib
{
    public class ListCloneClient
    {
        public List<string> ExternalListSources { get; set; } = new List<string>();
        public ServerList ClonedListResult { get; set; } = new ServerList();


        private Thread MainThread;
        public bool Started { get; set; } = false;
        public int Interval { get; set; }

        public ListCloneClient(List<string> listServers = null) 
        {
            if (listServers != null)
                ExternalListSources = listServers;
        }

        /// <summary></summary>
        /// <param name="loopDelayInterval">Delay between each clone process</param>
        public void StartClient(int loopDelayInterval = 30 * 1000)
        {
            this.Interval = loopDelayInterval;
            MainThread = new Thread(new ThreadStart(MainProc));
            MainThread.Start();
        }

        void MainProc()
        {
            try
            {
                if (Interval >= 0)
                {
                    Started = true;
                    while (true)
                    {
                        GoClone();
                        Thread.Sleep(Interval);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Started = false;
            }
        }

       public void GoClone()
        {
            try
            {
                ServerList clone = new ServerList();
                clone.AddRange(CloneExistingExternalAsciiLists());
                this.ClonedListResult = clone;
            }
            catch (Exception ex)
            {
            }
        }

        public List<GameServer> CloneExistingExternalAsciiLists(int port = 10057)
        {
            List<GameServer> res = new List<GameServer>();
            foreach (string listServer in ExternalListSources)
            {
                var servers = JJ2ListClient.ParseASCIIList(JJ2ListClient.DownloadASCIIList(listServer, port));
                res.AddRange(servers);
            }
            return res;
        }

    }
}
