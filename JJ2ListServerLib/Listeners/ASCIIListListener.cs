using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using JJ2ListServerLib.DataClasses;

namespace JJ2ListServerLib.Listeners
{
    public class ASCIIListListener
    {
        public ServerList SourceServerList { get; set; }

        private TcpListener sckt;

        public ASCIIListListener(ServerList srcServerList = null)
        {
            SourceServerList = srcServerList != null? srcServerList : new ServerList();
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
            sckt.Start();
            DoBeginAcceptTcpClient(sckt);
            return true;  
        }

        public bool StopListener(int port = 10057, IPAddress ipa = null)
        {
            if (sckt != null)
                sckt.Stop();
            return true;
        }

        public void SendList(TcpClient client)
        {
            byte[] packet = Encoding.ASCII.GetBytes(BuildASCIIServerList(this.SourceServerList));
            client.Client.Send(packet);
        }

        public static string BuildASCIIServerList(ServerList source)
        {
            List<ServerList> src = new List<ServerList>(1);
            src.Add(source);
            return BuildASCIIServerList(src);
        }

        public static string BuildASCIIServerList(List<ServerList> source)
        {
            StringBuilder res = new StringBuilder();
            DateTime d = DateTime.Now;
            foreach(ServerList l in source)
            {
                foreach (string serverID in l.Servers.Keys) //ID = "IP:port"
                {
                    GameServer server = l.Servers[serverID];
                    res.Append(server.IP); res.Append(':');
                    res.Append(server.Port); res.Append(' ');
                    res.Append(server.Location.ToString().ToLower()); res.Append(' ');
                    res.Append(server.IsPrivate? "private " : "public ");
                    res.Append(GetGameTypeName(server.GameType)); res.Append(' ');
                    res.Append("1." +  new string(server.Version,0,4)); res.Append(' ');
                    res.Append((int)d.Subtract(server.CreateTime).TotalSeconds); res.Append(' ');
                    res.Append(string.Format("[{0}/{1}] ",server.PlayerCount, server.PlayerLimit));
                    res.Append(server.Name);
                    res.Append("\r\n");
                }
            }
            if (res.Length == 0)
                res.Append("\r\n");
            return res.ToString();
        }

        public static string GetGameTypeName(byte gameType)
        {
            switch (gameType)
            {
                case 0:
                    return "sp";
                case 1:
                    return "coop";
                case 2:
                    return "battle";
                case 3:
                    return "race";
                case 4:
                    return "treasure";
                case 5:
                    return "ctf"; //"capture"
                default:
                    return "unknown";
            }
        }

        private  System.Threading.ManualResetEvent tcpClientConnected = new System.Threading.ManualResetEvent(false);

        // Accept one client connection asynchronously.
        private void DoBeginAcceptTcpClient(TcpListener listener)
        {
            // Set the event to nonsignaled state.
            tcpClientConnected.Reset();

            // Start to listen for connections from a client.
            Console.WriteLine("[ASCIIListListener] Waiting for a connection...");

            // Accept the connection.
            // BeginAcceptSocket() creates the accepted socket.
            listener.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), listener);

            // Wait until a connection is made and processed before
            // continuing.
            tcpClientConnected.WaitOne();
        }


        private void OnClientConnect(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            GoHandleNewClient(ar);
            
          
            // Process the connection here. (Add the client to a
            // server table, read data, etc.)
            Console.WriteLine("[ASCIIListListener] Client connected completed");

            // Signal the calling thread to continue.
            tcpClientConnected.Set();
            DoBeginAcceptTcpClient(listener);
        }

        private void GoHandleNewClient(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;

            // End the operation and send data.
            using (TcpClient client = listener.EndAcceptTcpClient(ar))
            {
                SendList(client);
                client.Client.Disconnect(false);
                client.Close();
            }
        }

    }
}
