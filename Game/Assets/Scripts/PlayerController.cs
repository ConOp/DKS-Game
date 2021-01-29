﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour                           
{
    public Transform view_direction;

    private void FixedUpdate()      //has the frequency of the physics system, it is called every fixed frame-rate frame (50 calls per sec)
    {
        SendInputToServer();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))   //mouse right click for shooting
        {
            Send.Shoot(view_direction.forward); //which direction player is facing (for shooting)
        
        }
    }

    private void SendInputToServer() //send local player's input (about movement) to the server, then server calculates the player's new position and sends it to all other remote clients)
    {
        bool[] inputs = new bool[] {
            Input.GetKey(KeyCode.W),        //physical keys (keyboard) for moving a player
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
        };
        Send.PlayerMovement(inputs);
    }

}
