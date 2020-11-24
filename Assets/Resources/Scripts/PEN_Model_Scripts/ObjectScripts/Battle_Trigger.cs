using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("Manager").GetComponent<Manager>().startCombat(other);
            gameObject.SetActive(false);
        }
    }
}
