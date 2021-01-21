using System;
using System.Threading;

namespace ServerApp
{
    class Program
    {
        private static bool running = false;

        static void Main(string[] args)
        {
            Console.Title = "Multiplayer Game Server";              //rename title in cmd
            running = true;
            Thread main = new Thread(new ThreadStart(MainThread));
            main.Start();
            Server.StartServer(4, 26950);
            //Console.ReadKey();                                      //exit
        }

        private static void MainThread()                                //simulate game loop (running)
        {
            Console.WriteLine($"Main thread is running at {Constants.ticks_per_sec} ticks per second");
            DateTime next = DateTime.Now;

            while (running)
            {
                while (next < DateTime.Now)                             //check if another tick needs to be executed
                {
                    GameLogic.Update();
                    next = next.AddMilliseconds(Constants.mils_per_tick);   //update time of next tick (that should happen)
                    if (next > DateTime.Now)                                //between ticks sleep in order to reduce cpu usage
                    {
                        Thread.Sleep(next - DateTime.Now);                  //sleep until it's time to execute next tick
                    }
                }
            }
        }
    }
}
