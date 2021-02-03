using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUB_UI_Manager : MonoBehaviour
{
    [HideInInspector]
    public GameObject comp_canvas;
    [HideInInspector]
    public GameObject mark;

    GameObject controls;
    GameObject lobbyScreen;
    GameObject lobbyButton;

    // Start is called before the first frame update
    void Start()
    {
        comp_canvas = GameObject.Find("ComputerCanvas");
        mark = GameObject.Find("Exclamation");
        controls = GameObject.Find("JoystickCanvas");
        lobbyScreen = GameObject.Find("YesLobby");
        lobbyButton = GameObject.Find("JoinLobbyButton");
        mark.SetActive(false);
        comp_canvas.SetActive(false);
        lobbyScreen.SetActive(false);
    }

    public void RemoveControl()
    {
        controls.SetActive(false);
    }

    public void GiveControl()
    {
        controls.SetActive(true);
    }

    public void ShowSelected(string selector)
    {
        RemoveControl();
        if (selector == "Computer")
        {
            comp_canvas.SetActive(true);
        }
    }
    bool inlobby = false;
    public void LobbyState()
    {
        inlobby = !inlobby;
        lobbyScreen.SetActive(inlobby);
        if (inlobby)
        {
            lobbyButton.GetComponentInChildren<Text>().text = "Exit Lobby";
        }
        else
        {
            lobbyButton.GetComponentInChildren<Text>().text = "Join Lobby";
        }
    }

    public void CloseComputer()
    {
        comp_canvas.SetActive(false);
    }

    public void StartGame()
    {
        controls.SetActive(true);
        Send.StartGame();
        SceneManager.LoadScene("DungeonScene");
    }
}
