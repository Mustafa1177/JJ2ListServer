// See https://aka.ms/new-console-template for more information

JJ2ListServerLib.JJ2ListServer listServer = new JJ2ListServerLib.JJ2ListServer();
listServer.StartServer();
listServer.ServerList.Add(new JJ2ListServerLib.DataClasses.GameServer() { IP = "37.97.196.56", Port = 10052, Name = "CDF Server", Location = JJ2ListServerLib.DataClasses.ServerLocation.MIRROR, GameType = 5, PlayerLimit = 1, PlayerCount = 0 });
listServer.ServerList.Add(new JJ2ListServerLib.DataClasses.GameServer() { IP = "3.74.206.174", Port = 10052, Name = "-t3> Server", Location = JJ2ListServerLib.DataClasses.ServerLocation.MIRROR, GameType = 2 });
listServer.ServerList.Add(new JJ2ListServerLib.DataClasses.GameServer() { IP = "51.75.65.48", Port = 10052, Name = "ak Server", Location = JJ2ListServerLib.DataClasses.ServerLocation.MIRROR, GameType = 5, PlayerLimit = 1, PlayerCount = 0 });
listServer.ServerList.MessageOfTheDay = "Works!";
Console.WriteLine("Welcome to Jazz2 Server List!");
while (true)
{
    Console.ReadLine();
}

