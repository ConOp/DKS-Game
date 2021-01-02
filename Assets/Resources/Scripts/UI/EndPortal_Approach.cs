using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal_Approach : MonoBehaviour
{

    public GameObject canvas;
    PopUpButton button;
    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        button = new PopUpButton(canvas, "Teleport to Next Room", null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canvas.SetActive(true);
            button.ShowButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            canvas.SetActive(false);
            button.HideButton();
        }
    }
}
