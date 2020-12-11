using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class Player
    {
        public int player_id;
        public string username;

        public Vector3 position;
        public Quaternion rotation;
        private float moveSpeed = 5f/Constants.ticks_per_sec;
        private bool[] inputs;

        public Player(int id, string usern, Vector3 spawn_position) {
            player_id = id;
            username = usern;
            position = spawn_position;
            rotation = Quaternion.Identity;

            inputs = new bool[4];
        }

        public void SetInput(bool[] local_inputs, Quaternion local_rotation) {
            inputs = local_inputs;
            rotation = local_rotation;
        }

        public void Update() {
            Vector2 input_direction = Vector2.Zero;
            if (inputs[0]) {
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

        private void Move(Vector2 input_direction) {
            Vector3 forward = Vector3.Transform(new Vector3(0,0,1),rotation);
            Vector3 right = Vector3.Normalize(Vector3.Cross(forward, new Vector3(0,1,0)));
            Vector3 move_direction = right * input_direction.X + forward * input_direction.Y;
            position += move_direction * moveSpeed;

            ServerSend.PlayerPosition(this);
            ServerSend.PlayerRotation(this);
        }
    }
}
