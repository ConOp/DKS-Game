using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager1 : MonoBehaviour
{
    public static UIManager1 manager;
    public InputField username;

    static GameObject Player1Text;
    static GameObject Player2Text;
    static GameObject Player3Text;
    static GameObject Player4Text;
    static List<GameObject> playerTexts;

    private void Awake()                        //gets called before application starts
    {
        Player1Text = GameObject.Find("Player1Text");
        Player2Text = GameObject.Find("Player2Text");
        Player3Text = GameObject.Find("Player3Text");
        Player4Text = GameObject.Find("Player4Text");
        playerTexts = new List<GameObject> { Player1Text, Player2Text, Player3Text, Player4Text };

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

    //onClick joinLobby button
    public void ConnectToServer()               //gets called when player presses connect button (tries to connect client (local player) to the server)
    {  
        username.interactable = false;          //property to enable or disable the ability to select a selectable UI element 
        Client.client.ConnectToServer();        //connect client to server
    }

    public static void ShowPlayerNames(int id, string username)
    {
        playerTexts[id].GetComponent<Text>().text = username;        
    }
}
