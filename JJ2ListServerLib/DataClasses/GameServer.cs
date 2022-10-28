using System;
using System.Collections.Generic;
using System.Text;

namespace JJ2ListServerLib.DataClasses
{
    public class GameServer
    {
        public string IP { get; set; }
        public ushort Port { get; set; }
        public string Name { get; set; }
        public bool PrivateServer { get; set; }
        public byte GameType { get; set; }
        public char[] Version { get; set; } = new char[4];
        public byte NumOfPlayers { get; set; }
        public byte MaxPlayers { get; set; }
        public long Uptime { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public ServerLocation Location { get; set; }
    }

    public enum ServerLocation
    {
        LOCAL,
        MIRROR,
        MANUAL
    }
}
