using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    #region Singleton
    private static PlayerManager instance = null;
    public static PlayerManager GetInstance()
    {
        if (instance == null)
        {
            return new PlayerManager();
        }
        return instance;
    }
    private PlayerManager()
    {
        instance = this;
        activePlayers = new List<GameObject>();
    }
    #endregion
    List<GameObject> activePlayers;
    public List<GameObject> GetActivePlayers()
    {
        return activePlayers;
    }
    public void AddPlayer(GameObject player)
    {
        if (!activePlayers.Contains(player))activePlayers.Add(player);
    }
    public void RemovePlayer(GameObject player)
    {
        if (activePlayers.Contains(player)) activePlayers.Remove(player);
    }
    public void SpawnPlayers()
    {
        foreach(GameObject player in activePlayers)
        {
            player.transform.position = new Vector3(5, 1, -3);
        }
    }
    public void DespawnPlayers()
    {
        foreach (GameObject player in activePlayers)
        {
            player.transform.position = player.transform.position - new Vector3(0, -3, 0);
           // Object.Destroy(player);
        }
    }


}
