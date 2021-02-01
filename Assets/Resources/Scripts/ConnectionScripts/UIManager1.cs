using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager1 : MonoBehaviour
{
    public static UIManager1 manager;
    public GameObject start_menu;
    public InputField username;

    private void Awake()                        //gets called before application starts
    {
        if (manager == null)
        {
            manager = this;                      //set it equal to the instance of Client class
        }
        else if (manager != this)
        {
            Debug.Log("Incorrect instance needs to be destroyed...");
            Destroy(this);                      //only one instance of UIManager must exist
        }
    }

    public void ConnectToServer()               //gets called when player presses connect button (tries to connect client (local player) to the server)
    {
        start_menu.SetActive(false);            //hide start menu once the player connects and starts playing
        username.interactable = false;          //property to enable or disable the ability to select a selectable UI element 
        Client.client.ConnectToServer();        //connect client to server
        Send.JoinLobby();
    }
}
