using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
    public static void PlayerRotation(Packet packet)
    {
        int id = packet.ReadInt();                                  //read client's id (local player's) that is rotating
        Vector3 forward = packet.ReadVector3();
        GameManager.players[id].transform.Find("PlayerCharacter").transform.forward = forward;
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
        SpawnWeapon.Spawn(packet.ReadString(), packet.ReadVector3(), packet.ReadFloat());
    }

    public static void RemotePlayerWeapon(Packet packet)
    {
        GameManager.game.PlayerHoldWeapon(packet.ReadInt(), packet.ReadFloat());
    }
    public static void SpawnEnemy(Packet packet)
    {
        string name = packet.ReadString();
        if (name.Contains("Ranged"))
        {
            GameObject enemy = Instantiate(Enemy_Prefab_Manager.GetInstance().GetRangedEnemies()[0], packet.ReadVector3(), new Quaternion());
           ConnectionEnemyHandler.GetInstance().allExistingEnemies.Add(enemy);
        }
        else
        {
            GameObject enemy = Instantiate(Enemy_Prefab_Manager.GetInstance().GetMeleeEnemies()[0], packet.ReadVector3(), new Quaternion());
            ConnectionEnemyHandler.GetInstance().allExistingEnemies.Add(enemy);
        }
    }
    public static void SpawnMod(Packet packet)
    {
        string mod_name = packet.ReadString();
        int enemyid = packet.ReadInt();
        ConnectionEnemyHandler.GetInstance().allExistingEnemies[enemyid].GetComponent<Basic_Enemy>().Add_Modification(mod_name);
    }
    public static void MoveEnemy(Packet packet)
    {

        Vector3 location = packet.ReadVector3();
        Quaternion rotation = packet.ReadQuaternion();
        int enemyid = packet.ReadInt();
        ConnectionEnemyHandler.GetInstance().allExistingEnemies[enemyid].transform.position = location;
        ConnectionEnemyHandler.GetInstance().allExistingEnemies[enemyid].transform.rotation = rotation;
    }
    public static void InCombat(Packet packet)
    {
        if (packet.ReadBool())
        {
            GameManager.players[Client.client.local_client_id].transform.Find("PlayerCharacter").GetComponent<Player>().enterCombat();
        }
        else
        {
            GameManager.players[Client.client.local_client_id].transform.Find("PlayerCharacter").GetComponent<Player>().exitCombat();
        }
    }

    public static void ReturnCombatEnemies(Packet packet)
    {
        int[] indexes = new int[packet.ReadInt()];
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = packet.ReadInt();
        }
        GameManager.players[Client.client.local_client_id].transform.Find("PlayerCharacter").GetComponent<Player>().SearchEnemies(indexes);

    }
    public static void LoadScene(Packet packet) {
        SceneManager.LoadScene(packet.ReadString());

    }
}
