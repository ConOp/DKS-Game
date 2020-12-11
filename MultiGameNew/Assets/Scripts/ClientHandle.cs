using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet packet) {

        string message = packet.ReadString();           //read with the same order the data has been set to packet
        int player_id = packet.ReadInt();               //remote client's id (current player)

        Debug.Log($"Message from the server: {message}");
        Client.client.local_client_id = player_id;      //set client's id with the one has been given from server
        ClientSend.Welcome_Received();

        Client.client.udp.ConnectedPlayer(((IPEndPoint)Client.client.tcp.socket.Client.LocalEndPoint).Port);   //pass the local port that TCP connection is using
    }

    public static void SpawnPlayer(Packet packet) {
        int id = packet.ReadInt();
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.game.SpawnPlayer(id, username, position, rotation);
    }

    public static void PlayerPosition(Packet packet) {
        int id = packet.ReadInt();
        Vector3 position = packet.ReadVector3();
        GameManager.players[id].transform.position = position;
    }
    public static void PlayerRotation(Packet packet) {
        int id = packet.ReadInt();
        Quaternion rotation = packet.ReadQuaternion();
        GameManager.players[id].transform.rotation = rotation;
    }

}
