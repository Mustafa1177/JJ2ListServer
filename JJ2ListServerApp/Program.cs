// See https://aka.ms/new-console-template for more information

JJ2ListServerLib.JJ2ListServer listServer = new JJ2ListServerLib.JJ2ListServer();
listServer.ExternalListSources.Add("list.jazzjackrabbit.com");
listServer.StartServer();
listServer.ManualServerList.Add(new JJ2ListServerLib.DataClasses.GameServer() { IP = "37.97.196.56", Port = 10052, Name = "CDF Server", Location = JJ2ListServerLib.DataClasses.ServerLocation.MIRROR, GameType = 5, PlayerLimit = 0, PlayerCount = 0 });
listServer.ManualServerList.Add(new JJ2ListServerLib.DataClasses.GameServer() { IP = "3.74.206.174", Port = 10052, Name = "-t3> Server", Location = JJ2ListServerLib.DataClasses.ServerLocation.MIRROR, GameType = 0 });
listServer.ManualServerList.Add(new JJ2ListServerLib.DataClasses.GameServer() { IP = "51.75.65.48", Port = 10052, Name = "H-Arena", Location = JJ2ListServerLib.DataClasses.ServerLocation.MIRROR, GameType = 5, PlayerLimit = 0, PlayerCount = 0 });
listServer.ManualServerList.MessageOfTheDay = "Works!";
Console.WriteLine("Welcome to Jazz2 Server List!");
while (true)
{
    Console.ReadLine();
}

