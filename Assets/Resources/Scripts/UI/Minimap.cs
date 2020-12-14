using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject minimap;
    // Start is called before the first frame update
    void Start()
    {
        minimap.SetActive(false);
    }

    public void MapToggle()
    {
        minimap.SetActive(!minimap.activeSelf);
    }
}
