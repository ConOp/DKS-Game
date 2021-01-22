using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour                           
{
    private void FixedUpdate()      //has the frequency of the physics system, it is called every fixed frame-rate frame (50 calls per sec)
    {
        SendInputToServer();
    }

    private void SendInputToServer() //player's movement (local player (client) sends input to the server, then server calculates the player's new position and sends it to all other remote clients)
    {
        bool[] inputs = new bool[] {
            Input.GetKey(KeyCode.W),        //physical keys (keyboard) for moving a player
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D)
        };
        ClientSend.PlayerMovement(inputs);
    }

}
