using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager game;
    public static Dictionary<int, PlayerManager1> players = new Dictionary<int, PlayerManager1>();    //[client-side] store player's info (game field logic)
    public GameObject local_player_prefab;
    public GameObject player_prefab;

    private void Awake()                        //gets called before application starts
    {
        if (game == null)
        {
            game = this;                        //set it equal to the instance of GameManager class
        }
        else if (game != this)
        {
            Debug.Log("Incorrect instance needs to be destroyed...");
            Destroy(this);                      //only one instance of Game class must exist
        }
    }

    public void Generate(int id, string username, Vector3 position, Quaternion rotation)        //generate player
    {
        GameObject player;
        if (id == Client.client.local_client_id)                                                //check if the generated player is the local player
        {
            player = Instantiate(local_player_prefab, position, rotation);                      //instatiate local player in game field
        }
        else
        {
            player = Instantiate(player_prefab, position, rotation);                            //instatiate another player (not local)
        }
        player.GetComponent<PlayerManager1>().Initialize(id, username);                          //initialize player's attributes after generating in game field
        players.Add(id, player.GetComponent<PlayerManager1>());                                  //add corresponding player manager of the player that has been just generated to the dictionary [key = id, value = player manager]
        Debug.Log("Player has been instatiated successfully...");
    }
}
