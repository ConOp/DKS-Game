using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager
{
    #region Singleton
    public static ConnectionManager instance;
    public static ConnectionManager GetInstance()
    {
        if(instance == null)
        {
            return new ConnectionManager();                     //set it equal to the instance of Client class
        }
        return instance;
    }
    ConnectionManager()
    {
        instance = this;
    }
    #endregion

   // public GameObject start_menu;
    public InputField username;



    public void ConnectToServer()               //gets called when player presses connect button (tries to connect client (local player) to the server)
    {
        // start_menu.SetActive(false);            //hide start menu once the player connects and starts playing
        username.interactable = false;          //property to enable or disable the ability to select a selectable UI element 
        ClientManager.GetInstance().ConnectToServer();        //connect client to server
    }
}
