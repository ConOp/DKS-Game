using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ServerApp
{
    class Player                                                        //[server-side] handling player's related data & logic
    {
        public int player_id;
        public string username;
        public Vector3 position;                                        //player's position
        public Quaternion rotation;                                     //player's rotation

        public Player(int id, string usern, Vector3 spawn_position)     //constructor for initializations
        {
            player_id = id;
            username = usern;
            position = spawn_position;
            rotation = Quaternion.Identity;

            //inputs = new bool[4];
        }
    }
}
