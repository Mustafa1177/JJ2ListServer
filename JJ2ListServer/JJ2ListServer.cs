using System;
using System.Linq;
using JJ2ListServerLib.Listeners;
namespace JJ2ListServerLib
{
    public class JJ2ListServer
    {
        public ServerList ServerList { get; set; } = new ServerList();
        public ListServerSettings Settings { get; set; } = new ListServerSettings();
        public ASCIIListListener AsciListener { get; set; };

    }
}
