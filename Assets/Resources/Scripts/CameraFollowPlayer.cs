using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.1f;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x,transform.position.y,target.position.z), smoothSpeed);
    }
}
