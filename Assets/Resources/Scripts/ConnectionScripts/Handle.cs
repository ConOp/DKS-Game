using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Handle : MonoBehaviour                                 //[client-side] handle received data that has been sent from the server
{
    public static void Welcome(Packet packet)                       //read welcome packet that has been sent from the server
    {
        string message = packet.ReadString();                       //read with the same order the data has been set to packet by the server (ServerSend)
        int player_id = packet.ReadInt();                           //get client's id (current player's id)

        Debug.Log($"Message from the server: {message}");           //display message from server

        Client.client.local_client_id = player_id;                  //set current player's id (client's id) with the available one that has been given from server
        Send.Welcome_Received();

        Client.client.udp.ConnectedPlayer(((IPEndPoint)Client.client.tcp.socket.Client.LocalEndPoint).Port);   //pass the local port that TCP connection is using (after tcp handshake start udp connection between client-server)
        
    }

    public static void Generate(Packet packet)                      //handle packet, extract info (then generate player in game field)
    {
        int id = packet.ReadInt();                                  //player's id
        string username = packet.ReadString();
        Vector3 position = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();

        GameManager.game.Generate(id, username, position, rotation);
    }
    public static Vector3 position;
    public static void PlayerPosition(Packet packet)
    {
        int id = packet.ReadInt();                                  //read client's id (local player's) that is moving
        position = packet.ReadVector3();
        GameManager.players[id].transform.Find("PlayerCharacter").transform.position = position;
        //GameManager.players[id].transform.Find("PlayerCharacter").transform.position = Vector3.MoveTowards(GameManager.players[id].transform.Find("PlayerCharacter").transform.position,position,5);      //set player to the new position
    }
    public static void PlayerRotation(Packet packet)
    {
        int id = packet.ReadInt();                                  //read client's id (local player's) that is rotating
        Quaternion rotation = packet.ReadQuaternion();
        GameManager.players[id].transform.Find("PlayerCharacter").transform.rotation = rotation;      //rotate player (local)
    }

    public static void DisconnectedPlayer(Packet packet) 
    {
        int disconnected_id = packet.ReadInt();                     //read player's id that was disconnected
        Destroy(GameManager.players[disconnected_id].gameObject);   //remove player that was disconnected (in order to prevent local player to keep seeing him after his disconnection)
        GameManager.players.Remove(disconnected_id);                //remove disconnected player from the dictionary
    }

    public static void PlayerHealth(Packet packet) 
    {
        int player_id = packet.ReadInt();                           //extract packet's info, sent from server
        float current_health = packet.ReadFloat();
        GameManager.players[player_id].setHealth(current_health);
    }

    public static void Regenerate(Packet packet) {
        int player_id = packet.ReadInt();
        GameManager.players[player_id].Regenerate();
    }

    public static void GenerateTile(Packet packet) 
    {
        string tile_name = packet.ReadString();
        Vector3 tile_position = packet.ReadVector3();
        Quaternion tile_rotation = packet.ReadQuaternion();
        ClientConstructDungeon.GetInstance().ReadNConstructTile(tile_name, tile_position, tile_rotation);
    }

    public static void GenerateRoom(Packet packet) {
        string room_name = packet.ReadString();
        Vector3 room_position = packet.ReadVector3();
        int tilesX = packet.ReadInt();
        int tilesZ = packet.ReadInt();
        string category = packet.ReadString();
        string type = packet.ReadString();
        ClientConstructDungeon.GetInstance().FinalizeRoom();
        if (ClientConstructDungeon.GetInstance().isInitialized())
        {
            ClientConstructDungeon.GetInstance().ReadNInitializeRoom(room_name, room_position, tilesX, tilesZ, category, type);
        }
        else
        {
            ClientConstructDungeon.GetInstance().InitializeDungeon();
            ClientConstructDungeon.GetInstance().ReadNInitializeRoom(room_name, room_position, tilesX, tilesZ, category, type);
        }
    }
    public static void RemoteDoors(Packet packet)
    {
        ClientConstructDungeon.GetInstance().DoorRemoteControl(packet.ReadVector3(), packet.ReadBool());
    }
    public static void AskPen(Packet packet)
    {
        GameObject player = GameObject.Find("CharacterSet").transform.Find("PlayerCharacter").gameObject;
        Send.PenValues(player.GetComponent<CharacterBehaviour>().pen.GetNeurotism(), player.GetComponent<CharacterBehaviour>().pen.GetExtraversion(), player.GetComponent<CharacterBehaviour>().pen.GetCertainty());
    }

    public static void WeaponLocation(Packet packet)
    {
        SpawnWeapon.Spawn(packet.ReadString(), packet.ReadVector3());
    }
}
