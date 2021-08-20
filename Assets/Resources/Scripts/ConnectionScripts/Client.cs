using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public static Client client = null;         //client instance
    public static int dataBufferSize = 4096;    //bytes

    public string ip = "127.0.0.1"; //server ip (for localhost "127.0.0.1"); 
    public string Ip => ip;

    private readonly int port = 25565;          //port number   26950; 
    public int Port => port;

    public int local_client_id = 0;             //local client's id
    public TCP tcp;                             //reference to client's TCP class
    public UDP udp;                             //reference to client's UDP class
    private bool connected = false;

    public delegate void PacketHandler(Packet packet);             //type that represents references to methods
    private static Dictionary<int, PacketHandler> packetHandlers;   //packet's id, corresponding packet handler
    public Dictionary<int, PacketHandler> Handlers { get => packetHandlers; }    //property for accessing private field


    private void Awake()
    {
        if (client == null)
        {
            client = this;                      //set it equal to the instance of Client class
        }
        else if (client != this)
        {
            Debug.Log("Incorrect instance needs to be destroyed...");
            Destroy(this);                      //only one instance of Client class must exist (meaning only one local player)
        }
    }
    //Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnApplicationQuit()                                //handle case unity doesn't properly close open connections in play mode
    {
        Disconnect();
    }

    public void ConnectToServer()                                   //client attempts to connect to the server
    {
        tcp = new TCP();
        udp = new UDP();
        InitializedClientData();
        connected = true;
        tcp.ConnectedPlayer();                                      //connect client(local player) via tcp, udp connection starts after successful tcp connection client-server
    }

    private void InitializedClientData()                                //intialize dictionary of data - packets
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int) ServerPackets.welcome, Handle.Welcome},
            { (int) ServerPackets.generated_player, Handle.Generate},
            { (int) ServerPackets.player_position, Handle.PlayerPosition},
            { (int) ServerPackets.player_rotation, Handle.PlayerRotation},
            { (int) ServerPackets.disconnected_player, Handle.DisconnectedPlayer},
            { (int) ServerPackets.load_scene, Handle.LoadScene},
            { (int) ServerPackets.generate_IRoom, Handle.GenerateRoom},
            { (int) ServerPackets.generate_Tile, Handle.GenerateTile},
            { (int) ServerPackets.askPen, Handle.AskPen},
            { (int) ServerPackets.remoteDoor, Handle.RemoteDoors},
            { (int) ServerPackets.weaponLocation, Handle.WeaponLocation},
            { (int) ServerPackets.remotePlayerWeapon, Handle.RemotePlayerWeapon},
            { (int) ServerPackets.spawnEnemy, Handle.SpawnEnemy},
            { (int) ServerPackets.spawnMod, Handle.SpawnMod},
            { (int) ServerPackets.moveEnemy, Handle.MoveEnemy},
            { (int) ServerPackets.inCombat, Handle.InCombat},
            { (int) ServerPackets.returnEnemiesInCombat, Handle.ReturnCombatEnemies}
        };

        Debug.Log("Initialization for packets done");
    }

    public void Disconnect() {                                    //disconnect from server and stop all traffic in the network
        if (connected) {
            connected = false;
            tcp.Socket.Close();
            udp.socket.Close();
            Debug.Log("Client disconnected from server!");
        }
    }
}
