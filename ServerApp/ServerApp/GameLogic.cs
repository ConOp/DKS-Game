using System;
using System.Collections.Generic;
using System.Text;

namespace ServerApp
{
    class GameLogic
    {
        public static void Update()
        {
            ThreadManager.UpdateMainThread();
        }
    }
}
