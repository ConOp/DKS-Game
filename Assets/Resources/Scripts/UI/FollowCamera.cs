using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Transform mainCamera;
    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera);
        transform.forward = -transform.forward;
    }
}
