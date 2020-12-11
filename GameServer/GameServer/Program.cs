using System;
using System.Threading;

namespace GameServer
{
    class Program
    {
        private static bool running = false;
        static void Main(string[] args)
        {
            Console.Title = "Multiplayer Game Server"; //rename title in console window
            Server.Start(4, 26950);                 //maxplayers, port number to listen
            running = true;
            Thread main = new Thread(new ThreadStart(MainThread));
            main.Start();

            //Console.ReadKey();                      //console window stays open until key is pressed
        }

        private static void MainThread() {
            Console.WriteLine($"Main thread is running at {Constants.ticks_per_sec} per second");
            DateTime next = DateTime.Now;

            while (running) {
                while (next < DateTime.Now) {
                    GameLogic.Update();
                    next = next.AddMilliseconds(Constants.mils_per_tick);
                    if (next > DateTime.Now) {
                        Thread.Sleep(next - DateTime.Now);
                    }
                }
            }
        }
    }
}
