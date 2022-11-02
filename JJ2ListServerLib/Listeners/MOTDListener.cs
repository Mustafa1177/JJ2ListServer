using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using JJ2ListServerLib.DataClasses;

namespace JJ2ListServerLib.Listeners
{
    public class MOTDListener
    {
        public ServerList SourceServerList { get; set; }

        private TcpListener sckt;

        public MOTDListener(ServerList srcServerList = null)
        {
            SourceServerList = srcServerList != null? srcServerList : new ServerList();
        }

        public bool StartListener(int port = 10058, IPAddress ipa = null)
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

        public bool StopListener(int port = 10058, IPAddress ipa = null)
        {
            if (sckt != null)
                sckt.Stop();
            return true;
        }

        public void SendMessageOfTheDay(string value, TcpClient client)
        {
            byte[] packet = Encoding.ASCII.GetBytes(!string.IsNullOrEmpty(value)? value : "\r\n");
            client.Client.Send(packet);
        }    

        private  System.Threading.ManualResetEvent tcpClientConnected = new System.Threading.ManualResetEvent(false);

        // Accept one client connection asynchronously.
        private void DoBeginAcceptTcpClient(TcpListener listener)
        {
            // Set the event to nonsignaled state.
            tcpClientConnected.Reset();

            // Start to listen for connections from a client.
            Console.WriteLine("[] Waiting for a connection...");

            // Accept the connection.
            // BeginAcceptSocket() creates the accepted socket.
            listener.BeginAcceptTcpClient(new AsyncCallback(OnClientConnect), listener);

            // Wait until a connection is made and processed before
            // continuing.
            /////////tcpClientConnected.WaitOne();
        }

        private void OnClientConnect(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            GoHandleNewClient(ar);
            
          
            // Process the connection here. (Add the client to a
            // server table, read data, etc.)
            Console.WriteLine("[] Client connected completed");

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
                SendMessageOfTheDay(SourceServerList.MessageOfTheDay, client);
                client.Client.Disconnect(false);
                client.Close();
            }
        }

    }
}
