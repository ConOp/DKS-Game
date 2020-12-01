using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWalksOver : MonoBehaviour
{
    Button canvas;
    ColorBlock colorVar;
    [HideInInspector]
    public bool standingOver;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("JoystickCanvas/HitUI/Interact").GetComponent<Button>();
        colorVar = canvas.colors;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().overEquipment = true;
            other.gameObject.GetComponent<Player>().interactObject = gameObject;
            colorVar.normalColor =  new Color32(207, 128, 65,255);
            colorVar.highlightedColor =  new Color32(207, 128, 65,255);
            colorVar.selectedColor =  new Color32(207, 128, 65,255);
            canvas.colors = colorVar;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().overEquipment = true;
            other.gameObject.GetComponent<Player>().interactObject = null;
            colorVar.normalColor = new Color32(255, 255, 255, 255);
            colorVar.highlightedColor = new Color32(255, 255, 255, 255);
            colorVar.selectedColor = new Color32(255, 255, 255, 255);
            canvas.colors = colorVar;
        }
    }
}
