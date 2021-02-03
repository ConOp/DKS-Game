using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
public class Client : MonoBehaviour
{
    public static Client client;                //client instance
    public static int dataBufferSize = 4096;    //bytes

    public string ip = "127.0.0.1";             //server ip for localhost
    public int port = 26950;                    //port number
    public int local_client_id = 0;             //local client's id
    public TCP tcp;                             //reference to client's TCP class
    public UDP udp;                             //reference to client's UDP class
    private bool connected = false;

    private delegate void PacketHandler(Packet packet);             //type that represents references to methods
    private static Dictionary<int, PacketHandler> packetHandlers;   //packet's id, corresponding packet handler


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

    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        private byte[] received_buffer;
        private Packet received_packet;            //packet (sent by server) and received from client

        public void ConnectedPlayer()               //try to connect local player to server using tcp
        {
            socket = new TcpClient { ReceiveBufferSize = dataBufferSize, SendBufferSize = dataBufferSize };
            received_buffer = new byte[dataBufferSize];
            socket.BeginConnect(client.ip, client.port, ConnectionCallback, socket);
        }

        private void ConnectionCallback(IAsyncResult asyncResult)       //gets called after client's successful connection to server via tcp and initializes necessary components for the communication
        {
            socket.EndConnect(asyncResult);                             //end asynchronous pending connection request
            if (!socket.Connected)
            {
                return;
            }
            stream = socket.GetStream();                                //get NetworkStream that used to send and receive data
            received_packet = new Packet();                             //initialize Packet instance
            stream.BeginRead(received_buffer, 0, dataBufferSize, ReceivedCallback, null);   //begin asynchronous reading from NetworkStream
        }

        private void ReceivedCallback(IAsyncResult asyncResult)     //read received data-packet from the NetworkStream
        {
            try
            {
                int bytes_length = stream.EndRead(asyncResult);     //returns number of bytes read from the NetworkStream
                if (bytes_length <= 0)
                {
                    client.Disconnect();                            //will disconnect both tcp and udp connections
                    return;                                         //get out of the method
                }
                byte[] data = new byte[bytes_length];               //if data has been received, create new buffer for the data

                Array.Copy(received_buffer, data, bytes_length);    //copy from one array to another
                received_packet.Reset(HandleData(data));            //reset Packet instance so it can be reused, but first get (handle) data from the packet
                stream.BeginRead(received_buffer, 0, dataBufferSize, ReceivedCallback, null);    //continue reading data from the NetworkStream
            }
            catch {
                Disconnect();                                       //will disconnect both tcp and udp connections
            }
        }

        public void SendData(Packet packet)                             //send given packet to server using tcp
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);    //begin asynchronous writing to the NetworkStream
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Error sending data from client to server via TCP: {ex}");
            }
        }

        private bool HandleData(byte[] data)                        //returns a boolean, telling Packet's Reset() whether the instance should be cleared
        {                                                           //also prepares received packet in order to get used by the suitable packet handler method 
            int packet_length = 0;
            received_packet.SetBytes(data);                         //set packet's bytes to the ones we read from the stream (data)
            if (received_packet.UnreadLength() >= 4)                //this is the start of packet (first value placed in the packet is its content's length, which is an integer [int consists of 4 bytes])
            {
                packet_length = received_packet.ReadInt();          //get packet's length of data that was sent from server and received from client (meaning local player)
                if (packet_length <= 0)                             //no data stored inside
                {
                    return true;                                    //true --> reset packet in order to receive new data
                }
            }

            while (packet_length > 0 && packet_length <= received_packet.UnreadLength())     //received packet contains another complete (whole) packet that needs to handled
            {                                                                                //and as long as packet's length doesn't exceed the length of the one we are reading
                byte[] packet_bytes = received_packet.ReadBytes(packet_length);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packet_bytes))
                    {
                        int packet_id = packet.ReadInt();
                        packetHandlers[packet_id](packet);              //invoke passing packet instance (call appropriate method to handle Packet)
                    }
                });

                packet_length = 0;                                      //reset packet's length
                if (received_packet.UnreadLength() >= 4)                //this is the start of packet (first value placed in the packet is its content's length, which is an integer [int consists of 4 bytes])
                {
                    packet_length = received_packet.ReadInt();          //get packet's length of data that was sent from server and received from client (meaning local player)
                    if (packet_length <= 0)                             //no data stored inside the packet
                    {
                        return true;                                    //true --> reset packet in order to receive new data
                    }
                }
            }

            if (packet_length <= 1)
            {
                return true;
            }
            return false;                                               //partial packet exists, so don't reset }
        }

        private void Disconnect() {                                 //disconnect client from server and clean necessary TCP instances
            client.Disconnect();
            stream = null;
            received_packet = null;
            received_buffer = null;
            socket = null;
        }
    }

    public class UDP {

        public UdpClient socket;
        public IPEndPoint iPEndPoint;                                   //represents a network endpoint as an IP address and a port number

        public UDP()                                                    //constructor of UDP class
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse(client.ip), client.port);
        }

        public void ConnectedPlayer(int local_port_number)              //try to connect local player to server using udp
        {
            socket = new UdpClient(local_port_number);                  //pass the port in which the client (local player) communicates with server [bind UdpClient to local port]
            socket.Connect(iPEndPoint);                                 //create default remote host using the specified network endpoint
            socket.BeginReceive(ReceivedCallback, null);                //start asynch receive
            using (Packet packet = new Packet())
            {                                                           //initiate UDP connection with the server by sending a packet (empty one)
                SendData(packet);
            }
        }

        private void ReceivedCallback(IAsyncResult asyncResult)         //read received udp data-packet
        {
            try
            {
                byte[] data = socket.EndReceive(asyncResult, ref iPEndPoint);       //end pending asynchronous receive and return bytes of data
                socket.BeginReceive(ReceivedCallback, null);            //start asynchronous receive (for new data)

                if (data.Length < 4)                                               //check for existing packet in order to handle it (every packet has id at top)
                {
                    client.Disconnect();
                    return;
                }
                HandleData(data);                                                   //packet exists
            }
            catch {
                Disconnect();
            }
        }

        private void HandleData(byte[] data)                            //prepares received packet in order to get used by the suitable packet handler method
        {
            using (Packet packet = new Packet(data))                                //new Packet with the given data
            {
                int packet_length = packet.ReadInt();                               //extract packet's content
                data = packet.ReadBytes(packet_length);
            }
            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet packet = new Packet(data))                            //new packet with shortened byte array (packet's length has been removed from the top)
                {
                    int packet_id = packet.ReadInt();
                    packetHandlers[packet_id](packet);                              //invoke passing packet instance
                }
            });
        }

        public void SendData(Packet packet)                             //send packet to server using udp
        {
            try
            {
                packet.InsertInt(client.local_client_id);               //insert client's id into packet (in order for server to understand who send it)
                if (socket != null)
                {
                    socket.BeginSend(packet.ToArray(), packet.Length(), null, null);    //start asynchronous send
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Error occurred while sending data from client to server via UDP: {e}...");
            }
        }

        private void Disconnect() {                                     //disconnect client from server and clean necessary UDP instances
            client.Disconnect();
            iPEndPoint = null;
            socket = null;
        }
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
            { (int) ServerPackets.player_health, Handle.PlayerHealth},
            { (int) ServerPackets.regenerated_player, Handle.Regenerate},
            { (int) ServerPackets.generate_IRoom, Handle.GenerateRoom},
            { (int) ServerPackets.generate_Tile, Handle.GenerateTile},
            { (int) ServerPackets.askPen, Handle.AskPen},
            { (int) ServerPackets.remoteDoor, Handle.RemoteDoors}
        };

        Debug.Log("Initialization for packets done");
    }

    private void Disconnect() {                                         //disconnect from server and stop all traffic in the network
        if (connected) {
            connected = false;
            tcp.socket.Close();
            udp.socket.Close();
            Debug.Log("Client disconnected from server!");
        }
    }
}
