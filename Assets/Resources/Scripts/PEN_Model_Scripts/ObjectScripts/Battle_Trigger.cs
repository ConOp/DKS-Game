using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("Manager").GetComponent<Manager>().startCombat(other);
            gameObject.SetActive(false);
        }
    }
}
