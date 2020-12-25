using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal_Approach : MonoBehaviour
{

    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canvas.SetActive(false);
        }
    }
}
