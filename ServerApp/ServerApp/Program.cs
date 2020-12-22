using System;

namespace ServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Multiplayer Game Server";              //rename title in cmd
            Server.StartServer(4, 26950);
            Console.ReadKey();                                      //exit
        }
    }
}
