using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EquipmentOnGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [Tooltip("The rotation speed of the object.")]
    [Range(0, 100)]
    public int speed = 50;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
}
