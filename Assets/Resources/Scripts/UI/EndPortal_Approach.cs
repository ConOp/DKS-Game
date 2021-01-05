using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal_Approach : MonoBehaviour
{
    GameObject buttonPref;
    GameObject button;
    private void Start()
    {
        buttonPref = UIManager.GetInstance().PopUpButtonObject("Teleport to Next Floor", null);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            button = Instantiate(buttonPref, other.gameObject.transform.Find("PopUpCanvas").transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(button);
        }
    }
}
