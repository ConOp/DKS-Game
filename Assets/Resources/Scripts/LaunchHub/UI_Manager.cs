using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [HideInInspector]
    public GameObject comp_canvas;
    [HideInInspector]
    public GameObject mark;

    GameObject controls;

    // Start is called before the first frame update
    void Start()
    {
        comp_canvas = GameObject.Find("ComputerCanvas");
        mark = GameObject.Find("Exclamation");
        controls = GameObject.Find("JoystickCanvas");
        mark.SetActive(false);
        comp_canvas.SetActive(false);
    }

    public void RemoveControl()
    {
        controls.SetActive(false);
    }

    public void GiveControl()
    {
        controls.SetActive(true);
    }

    public void ShowSelected(string selector)
    {
        RemoveControl();
        if (selector == "Computer")
        {
            comp_canvas.SetActive(true);
        }
    }

    public void CloseComputer()
    {
        comp_canvas.SetActive(false);
    }
}
