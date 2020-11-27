using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                GameObject.Find("Manager").GetComponent<Manager>().enemies.Add(e);
            }
            GameObject.Find("Manager").GetComponent<Manager>().startCombat(other);
            gameObject.SetActive(false);
        }
    }
}
