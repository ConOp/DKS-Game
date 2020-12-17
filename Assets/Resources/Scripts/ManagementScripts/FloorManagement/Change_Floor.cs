using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Floor : MonoBehaviour
{
    private List<GameObject> players=new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (!players.Contains(other.gameObject)&&PlayerManager.GetInstance().GetActivePlayers().Contains(other.gameObject))
        {
            players.Add(other.gameObject);
            if (players.Count.Equals(PlayerManager.GetInstance().GetActivePlayers().Count))
            {
                GameObject.FindGameObjectWithTag("DungeonMaster").GetComponent<TestLoad>().ProceedNextStage();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (players.Contains(other.gameObject))
        {
            players.Remove(other.gameObject);
        }
    }

    
}
