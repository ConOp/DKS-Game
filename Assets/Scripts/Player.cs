using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

[RequireComponent(typeof(CharacterController))]
public class Player : NetworkBehaviour
{
    CharacterController character;                                          //to handle character controller

    [SerializeField]
    KeyCode restart;

    float y_velocity = 0;
    [Range(-5f, -25f)]
    public float gravity = -15f;
    [Range(5f, 15f)]
    public float movementSpeed = 10f;   //speed of player's movement (public in order to access value from inspector window)
    [Range(5f, 15f)]
    public float jumpSpeed = 10f;                                           //player's jump speed

    //camera variables
    Transform fpscamera;
    float step = 0f;
    [Range(1f, 90f)]
    public float maxStep = 85f;
    [Range(-1f, -90f)]
    public float minStep = -85f;
    [Range(0.5f, 5f)]                                                       //display range to inspector window
    public float mouse_sensitivity = 2f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();                    //return character controller component attached to the player
        fpscamera = GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            Turn_around();
            Move();
        }
        else
        {
            fpscamera.gameObject.SetActive(false);                          //disable camera
        }
        
        if (Input.GetKey(restart))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);     //R button for restart
        }
    }

    void Turn_around()
    {
        float x_input = Input.GetAxis("Mouse X") * mouse_sensitivity;       //rotate in x axis using mouse as a camera
        float y_input = Input.GetAxis("Mouse Y") * mouse_sensitivity;
        transform.Rotate(0, x_input, 0);                                    //turn player based on the x input
        step -= y_input;
        step = Mathf.Clamp(step, minStep, maxStep);
        Quaternion rot = Quaternion.Euler(step, 0, 0);                     //create the local rotation value for the camera and set it
        fpscamera.localRotation = rot;
    }

    void Move()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f); //movementSpeed
        Vector3 movement = transform.TransformVector(input) * movementSpeed;
        if (character.isGrounded)
        {
            y_velocity = 0;
            if (Input.GetButtonDown("Jump"))
            {
                y_velocity = jumpSpeed;
            }
        }

        y_velocity += gravity * Time.deltaTime;
        movement.y = y_velocity;
        character.Move(movement * Time.deltaTime);                          //move character around
    }
}
