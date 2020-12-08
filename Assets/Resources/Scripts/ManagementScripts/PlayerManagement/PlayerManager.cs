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
    PlayerManager()
    {
        instance = this;
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

}
