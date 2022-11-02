using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JJ2ListServerLib.DataClasses;

namespace JJ2ListServerLib
{
    public class ServerList
    {
        public Dictionary<string, GameServer> Servers { get; set; } = new Dictionary<string, GameServer>();
        public string MessageOfTheDay { get; set; } = "";

        public bool Add(GameServer server)
        {
            string key = server.IP + ":" + server.Port;
            if (!Servers.ContainsKey(key))
            {
                Servers.Add(key, server);
                return true;
            }
            return false;
        }

        public void AddRange(ICollection<GameServer> servers)
        {
            foreach (GameServer server in servers)
                Add(server);
        }

        public void AddRange(ServerList list)
        {
            foreach (string serverID in list.Servers.Keys)
                Add(list.Servers[serverID]);
        }

        public static ServerList operator +(ServerList c1, ServerList c2)
        {
            ServerList res = new ServerList();
            res.AddRange(c1);
            res.AddRange(c2);
            return res;
        }

        public void Remove(GameServer server)
        {
            string key = server.IP + ":" + server.Port;
            if (Servers.ContainsKey(key))
                Servers.Remove(key);
        }

        public void Remove(string ip, int port)
        {
            string key = ip + ":" + port;
            if (Servers.ContainsKey(key))
                Servers.Remove(key);
        }

        public void Remove(string endPoint)
        {
            if (Servers.ContainsKey(endPoint))
                Servers.Remove(endPoint);
        }

        public bool Contains(string endPoint)
        {
            return Servers.ContainsKey(endPoint);
        }

        public bool Contains(string ip, int port)
        {
            return Servers.ContainsKey(ip + ":" + port);
        }

    }
}
