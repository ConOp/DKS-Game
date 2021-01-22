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

        private float moving_speed = 5f / Constants.ticks_per_sec;      //player's move speed (calculate like multiplying by unity's time.deltatime)
        private bool[] inputs;                                          //store inputs about movement sent from client

        public Player(int id, string usern, Vector3 spawn_position)     //constructor for initializations
        {
            player_id = id;
            username = usern;
            position = spawn_position;
            rotation = Quaternion.Identity;

            inputs = new bool[4];                                       //initialize the array (boolean for every keyword that was pressed)
        }

        public void SetInput(bool[] local_inputs, Quaternion local_rotation)
        {
            inputs = local_inputs;
            rotation = local_rotation;
        }

        public void Update()                                            //update player's new position (movement)
        {
            Vector2 input_direction = Vector2.Zero;
            if (inputs[0])
            {
                input_direction.Y += 1;
            }
            if (inputs[1])
            {
                input_direction.Y -= 1;
            }
            if (inputs[2])
            {
                input_direction.X += 1;
            }
            if (inputs[3])
            {
                input_direction.X -= 1;
            }
            Move(input_direction);
        }

        private void Move(Vector2 input_direction)                      //change player's movement and rotation
        {
            Vector3 forward = Vector3.Transform(new Vector3(0, 0, 1), rotation);                //represents player's forward direction
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, new Vector3(0, 1, 0)));    //vertical direction
            Vector3 move_direction = right * input_direction.X + forward * input_direction.Y;   //direction to move
            position += move_direction * moving_speed;

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }
    }
}
