using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraController : MonoBehaviour
{
    public PlayerManager playerManager;
    public float sensitivity = 100;
    public float clampAngle = 85f;

    private float vertical_rotation;
    private float horizontal_rotation;

    private void Start()
    {
        vertical_rotation = transform.localEulerAngles.x;
        horizontal_rotation = playerManager.transform.eulerAngles.y;
    }
    private void Update()
    {
        Look();
        Debug.DrawRay(transform.position,transform.forward*2, Color.white);
    }

    private void Look() {
        float mouse_vertical = -Input.GetAxis("Mouse Y");
        float mouse_horizontal = Input.GetAxis("Mouse X");

        vertical_rotation += mouse_vertical * sensitivity * Time.deltaTime;
        horizontal_rotation += mouse_horizontal * sensitivity * Time.deltaTime;

        vertical_rotation = Mathf.Clamp(vertical_rotation, -clampAngle, clampAngle);
        transform.localRotation = Quaternion.Euler(vertical_rotation, 0f, 0f);
        playerManager.transform.rotation = Quaternion.Euler(0f, horizontal_rotation, 0f);
    }
}
