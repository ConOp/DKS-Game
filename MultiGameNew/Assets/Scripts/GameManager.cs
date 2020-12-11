using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager game;
    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public GameObject local_player_prefab;
    public GameObject player_prefab;
    
    private void Awake()                        //gets called before application starts
    {
        if (game == null)
        {
            game = this;                      //set it equal to the instance of GameManager class
        }
        else if (game != this)
        {
            Debug.Log("Incorrect instance needs to be destroyed...");
            Destroy(this);                      //only one instance of GameManager class must exist
        }
    }

    public void SpawnPlayer(int id, string username, Vector3 position, Quaternion rotation) {
        GameObject player;
        if (id == Client.client.local_client_id)
        {
            player = Instantiate(local_player_prefab, position, rotation);
        }
        else {
            player = Instantiate(player_prefab, position, rotation);
        }
        player.GetComponent<PlayerManager>().id = id;
        player.GetComponent<PlayerManager>().username = username;
        players.Add(id, player.GetComponent<PlayerManager>());
    }
}
