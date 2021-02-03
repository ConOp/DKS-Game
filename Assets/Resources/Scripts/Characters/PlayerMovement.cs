using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField]
    float moveSpeed = 10f;

    //[HideInInspector]
    public Vector3 forward, right;

    public Joystick joystick;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; //sets right position to 90 degrees to the right of forward position.

    }


    private void FixedUpdate()      //has the frequency of the physics system, it is called every fixed frame-rate frame (50 calls per sec)
    {
        if (Client.client.local_client_id != 0) 
        {
            SendInputToServer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Client.client.local_client_id == 0) 
        { 
            if (Math.Abs(joystick.Horizontal) > 0.2f || Math.Abs(joystick.Vertical) > 0.2f)//sets sensitivity for movement.
            {
                Move();
            }
        }
        else
        {
            //todo
        }
    }
    
    void Move()
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * joystick.Horizontal;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * joystick.Vertical;

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading; //in order to move according to camera rotation and NOT global position.
        transform.position += rightMovement;
        transform.position += upMovement;
    }

    private void SendInputToServer() //send local player's input (about movement) to the server, then server calculates the player's new position and sends it to all other remote clients)
    {
        float[] inputs = FixMovement();
        /*
        bool[] inputs = new bool[] {
            Input.GetKey(KeyCode.W),        //physical keys (keyboard) for moving a player
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
        };
        */
        Send.PlayerMovement(inputs);
    }

    float[] FixMovement()
    {
        float[] inputs = new float[]
        {
            joystick.Horizontal,
            joystick.Vertical
        };
        inputs[0] = Math.Abs(inputs[0]) < 0.2f ? 0 : inputs[0];
        inputs[1] = Math.Abs(inputs[1]) < 0.2f ? 0 : inputs[1];
        return inputs;
    }
}
