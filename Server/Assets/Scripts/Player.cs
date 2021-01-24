using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour                                 //[server-side] handling player's related data & logic
{
    public int player_id;
    public string username;

    private float moving_speed = 5f / Constants.ticks_per_sec;      //player's move speed (calculate like multiplying by unity's time.deltatime)
    private bool[] inputs;                                          //store inputs about movement sent from client

    public void InitializePlayer(int id, string usern)              //necessary initializations
    {
        player_id = id;
        username = usern;

        inputs = new bool[4];                                       //initialize the array (boolean for every keyword that was pressed)
    }

    public void SetInput(bool[] local_inputs, Quaternion local_rotation)    //inputs according to pressed keywords, new rotation according to mouse input
    {
        inputs = local_inputs;
        transform.rotation = local_rotation;
    }

    public void FixedUpdate()                                       //update player's new position (movement) by making suitable calculations
    {
        Vector2 input_direction = Vector2.zero;
        if (inputs[0])
        {
            input_direction.y += 1;
        }
        if (inputs[1])
        {
            input_direction.y -= 1;
        }
        if (inputs[2])
        {
            input_direction.x -= 1;
        }
        if (inputs[3])
        {
            input_direction.x += 1;
        }
        Move(input_direction);
    }

    private void Move(Vector2 input_direction)                      //change player's movement and rotation
    {
        Vector3 move_direction = transform.right * input_direction.x + transform.forward * input_direction.y;   //direction to move
        transform.position += move_direction * moving_speed;

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }
}
