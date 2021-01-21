using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientHandle : MonoBehaviour                           //[client-side] handle received data that has been sent from the server
{
    public static void Welcome(Packet packet)                       //read welcome packet that has been sent from the server
    {
        string message = packet.ReadString();                       //read with the same order the data has been set to packet by the server (ServerSend)
        int player_id = packet.ReadInt();                           //get client's id (current player's id)

        Debug.Log($"Message from the server: {message}");           //display message from server

        Client.client.local_client_id = player_id;                  //set current player's id (client's id) with the available one that has been given from server
        ClientSend.Welcome_Received();

    }

    public static void Generate(Packet packet)                      //handle packet, extract info (then generate player in game field)
    {
        int id = packet.ReadInt();                                  //player's id
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.game.Generate(id, username, position, rotation);
    }
}
