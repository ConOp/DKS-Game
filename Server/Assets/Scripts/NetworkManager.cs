using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager network_manager;                //NetworkManager instance
    public GameObject player_prefab;

    private void Awake()
    {
        if (network_manager == null)
        {
            network_manager = this;                             //set it equal to the instance of NetworkManager class
        }
        else if (network_manager != this)
        {
            Debug.Log("Incorrect instance needs to be destroyed...");
            Destroy(this);                                      //only one instance of NetworkManager class must exist
        }
    }

    //Start is called before the first frame update
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        Server.StartServer(4, 26950);
    }

    public Player InstatiatePlayer() {
        return Instantiate(player_prefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();        //return attached player component that has been generated
    }
}
