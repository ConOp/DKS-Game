using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentOnGround : MonoBehaviour
{
    Button canvas;
    ColorBlock colorVar;
    [Tooltip("The rotation speed of the object.")]
    [Range(0, 100)]
    public int speed = 50;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>())
        {
            anim = GetComponent<Animator>();
            anim.enabled = false;
        }
        canvas = GameObject.Find("JoystickCanvas/HitUI/Interact").GetComponent<Button>();
        colorVar = canvas.colors;
    }

    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }

    /// <summary>
    /// state true when picked up, false when dropped down.
    /// </summary>
    /// <param name="state"></param>
    public void PickedUp(bool state)
    {
        if (anim)
        {
            anim.enabled = state;
        }
        ResetColors();
        gameObject.GetComponent<Collider>().enabled = !state;
        gameObject.GetComponent<EquipmentOnGround>().enabled = !state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().overEquipment = true;
            other.gameObject.GetComponent<Player>().interactObject = gameObject;
            colorVar.normalColor = new Color32(207, 128, 65, 255);
            colorVar.highlightedColor = new Color32(207, 128, 65, 255);
            colorVar.selectedColor = new Color32(207, 128, 65, 255);
            canvas.colors = colorVar;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().overEquipment = false;
            other.gameObject.GetComponent<Player>().interactObject = null;
            ResetColors();
        }
    }

    void ResetColors()
    {
        colorVar.normalColor = new Color32(255, 255, 255, 255);
        colorVar.highlightedColor = new Color32(255, 255, 255, 255);
        colorVar.selectedColor = new Color32(255, 255, 255, 255);
        canvas.colors = colorVar;
    }
}
