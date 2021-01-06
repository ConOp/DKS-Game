using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal_Approach : MonoBehaviour
{
    GameObject button;
    private List<GameObject> players = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (!players.Contains(other.gameObject) && PlayerManager.GetInstance().GetActivePlayers().Contains(other.gameObject))
        {
            players.Add(other.gameObject);
            button = Instantiate(UIManager.GetInstance().PopUpButtonObject("Teleport to Next Floor",ReadyToLeave), other.gameObject.transform.Find("PopUpCanvas").transform);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (players.Contains(other.gameObject))
        {
            players.Remove(other.gameObject);
            Destroy(button);
        }
    }

    public void ReadyToLeave()
    {
        if (players.Count.Equals(PlayerManager.GetInstance().GetActivePlayers().Count))
        {
            GameObject.FindGameObjectWithTag("DungeonMaster").GetComponent<TestLoad>().ProceedNextStage();
        }
    }
}
