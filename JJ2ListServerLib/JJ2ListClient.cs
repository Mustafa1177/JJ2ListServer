using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace JJ2ListServerLib
{
    public static class JJ2ListClient
    {
        public static string DownloadASCIIList(string hostname = "list.jazzjackrabbit.com", int port = 10057)
        {
            TcpClient client = new TcpClient(hostname,port);
            client.Connect(hostname, port);
            if (client.Connected)
            {
                byte[] buffer = new byte[1024 * 4];
                var dataLength = client.Client.Receive(buffer);
                if(dataLength > 0)
                {
                    string recv = Encoding.ASCII.GetString(buffer, 0, dataLength);
                    return recv;
                }
            }
            return string.Empty;
        }

        public static List<DataClasses.GameServer> ParseASCIIList(string hostname = "list.jazzjackrabbit.com", int port = 10057)
        {
            return null;
        }

        }
}
