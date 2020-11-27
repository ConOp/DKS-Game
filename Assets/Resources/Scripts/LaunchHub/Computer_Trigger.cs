using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer_Trigger : MonoBehaviour
{
    HUB_UI_Manager uimng;
    public GameObject canvas;

    private void Start()
    {
        uimng = GameObject.FindGameObjectWithTag("Manager").GetComponent<HUB_UI_Manager>();
        canvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        uimng.mark.SetActive(true);
        canvas.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        uimng.mark.SetActive(false);
        canvas.SetActive(false);
    }
}
