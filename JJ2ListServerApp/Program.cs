// See https://aka.ms/new-console-template for more information

JJ2ListServerLib.JJ2ListServer listServer = new JJ2ListServerLib.JJ2ListServer();
listServer.StartServer();

listServer.ServerList.MessageOfTheDay = "Works!";
Console.WriteLine("Welcome to Jazz2 Server List!");
while (true)
{
    Console.ReadLine();
}

