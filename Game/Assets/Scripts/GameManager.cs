using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    #region Singleton
    public static GameManager instance;
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            return new GameManager();                     //set it equal to the instance of Client class
        }
        return instance;
    }
    GameManager()
    {
        instance = this;
    }
    #endregion
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();    //[client-side] store player's info (game field logic)
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
        player.GetComponent<PlayerManager>().Initialize(id, username);                          //initialize player's attributes after generating in game field
        players.Add(id, player.GetComponent<PlayerManager>());                                  //add corresponding player manager of the player that has been just generated to the dictionary [key = id, value = player manager]
        Debug.Log("Player has been instatiated successfully...");
    }
}
