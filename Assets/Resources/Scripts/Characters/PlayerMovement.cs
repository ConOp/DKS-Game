using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 4f;

    [HideInInspector]
    public Vector3 forward, right;

    public Joystick joystick;

    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; //sets right position to 90 degrees to the right of forward position.

    }

    // Update is called once per frame
    void Update()
    {
        if(Math.Abs(joystick.Horizontal)>0.2f || Math.Abs(joystick.Vertical) > 0.2f)//sets sensitivity for movement.
        {
            Move();
        }
    }
    
    void Move()
    {
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * joystick.Horizontal;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * joystick.Vertical;

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.forward = heading; //in order to move according to camera rotation and NOT global position.
        transform.position += rightMovement;
        transform.position += upMovement;
    }
}
