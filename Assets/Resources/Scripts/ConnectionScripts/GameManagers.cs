using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers
{
    #region Singleton
    public static GameManagers instance;
    public static GameManagers GetInstance()
    {
        if (instance == null)
        {
            return new GameManagers();                     //set it equal to the instance of Client class
        }
        return instance;
    }
    GameManagers()
    {
        instance = this;
    }
    #endregion
    public static Dictionary<int, PlayerStatsToBeFixed> players = new Dictionary<int, PlayerStatsToBeFixed>();    //[client-side] store player's info (game field logic)
    public GameObject local_player_prefab;
    public GameObject player_prefab;


    public void Generate(int id, string username, Vector3 position, Quaternion rotation)        //generate player
    {
        GameObject player;
        if (id == ClientManager.GetInstance().local_client_id)                                                //check if the generated player is the local player
        {
            player = Object.Instantiate(local_player_prefab, position, rotation);                      //instatiate local player in game field
        }
        else
        {
            player = Object.Instantiate(player_prefab, position, rotation);                            //instatiate another player (not local)
        }
        player.GetComponent<PlayerStatsToBeFixed>().Initialize(id, username);                          //initialize player's attributes after generating in game field
        players.Add(id, player.GetComponent<PlayerStatsToBeFixed>());                                  //add corresponding player manager of the player that has been just generated to the dictionary [key = id, value = player manager]
        Debug.Log("Player has been instatiated successfully...");
    }
}
