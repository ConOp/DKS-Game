using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour                       //player's ability to look around
{
    public PlayerManager1 playerManager;                             //player's instance
    public float sensitivity = 100f;
    public float angle = 85f;

    private float vertical_rotation;
    private float horizontal_rotation;

    //Start is called before the first frame update
    private void Start()
    {
        vertical_rotation = transform.localEulerAngles.x;
        horizontal_rotation = playerManager.transform.eulerAngles.y;
    }

    //Update is called every frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            toggleCursorMode();
        }
        if (Cursor.lockState == CursorLockMode.Locked) 
        {
            Look();
        }
        Debug.DrawRay(transform.position, transform.forward * 2, Color.blue);               //NEEDS TO BE REMOVED (useful only for testing player's sight of view)
    }

    private void Look()                                                                     //player's sight of view (using camera)
    {
        float mouse_vertical = -Input.GetAxis("Mouse Y");
        float mouse_horizontal = Input.GetAxis("Mouse X");

        vertical_rotation += mouse_vertical * sensitivity * Time.deltaTime;
        horizontal_rotation += mouse_horizontal * sensitivity * Time.deltaTime;

        vertical_rotation = Mathf.Clamp(vertical_rotation, -angle, angle);
        transform.localRotation = Quaternion.Euler(vertical_rotation, 0f, 0f);
        playerManager.transform.rotation = Quaternion.Euler(0f, horizontal_rotation, 0f);
    }

    private void toggleCursorMode() {
        Cursor.visible = !(Cursor.visible);
        if (Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
