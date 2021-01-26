using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour                                 //[server-side] handling player's related data & logic
{
    public int player_id;
    public string username;
    public CharacterController characterController;                 //player's reference to unity's character controller
    public float gravity = (-9.81f) * 2;                                  //gravity acceleration

    public float moving_speed = 5f;                                 //player's move speed (calculate like multiplying by unity's time.deltatime)
    public float jumping_speed = 9f;
    private float vertical_speed_y = 0;
    private bool[] inputs;                                          //store inputs about movement sent from client

    private void Start()
    {
        gravity *= Mathf.Pow(Time.fixedDeltaTime, 2);
        moving_speed *= Time.fixedDeltaTime;
        jumping_speed *= Time.fixedDeltaTime;
    }

    public void InitializePlayer(int id, string usern)              //necessary initializations
    {
        player_id = id;
        username = usern;

        inputs = new bool[5];                                       //initialize the array (boolean for every keyword that was pressed)
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
        move_direction *= moving_speed;
        
        if (characterController.isGrounded) {
            vertical_speed_y = 0f;
            if (inputs[4]) {                                        //check if jump key was pressed
                vertical_speed_y = jumping_speed;
            }
        }

        vertical_speed_y += gravity;
        move_direction.y = vertical_speed_y;
        characterController.Move(move_direction);

        ServerSend.PlayerPosition(this);
        ServerSend.PlayerRotation(this);
    }
}
