using System;
using System.Collections.Generic;
using System.Text;

namespace JJ2ListServerLib.DataClasses
{
    public class GameServer
    {
        public int Priority { get; set; } = 255;
        public string IP { get; set; }
        public ushort Port { get; set; }
        public ServerLocation Location { get; set; }
        public bool IsPrivate { get; set; }
        public byte GameType { get; set; }
        public char[] Version { get; set; } = {'2','4',' ',' ' };
        public long Uptime { get; set; } = -1;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public byte PlayerCount { get; set; }
        public byte PlayerLimit { get; set; }
        public string Name { get; set; } 
    }

    public enum ServerLocation
    {
        LOCAL,
        MIRROR,
        MANUAL,
        CLONE
    }
}
