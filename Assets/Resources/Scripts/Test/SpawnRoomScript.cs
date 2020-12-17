using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerManager.GetInstance().AddPlayer(other.gameObject);
            Debug.Log("Player added to active players");
        }
    }
}
