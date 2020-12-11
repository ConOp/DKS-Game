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
    
    private delegate void PacketHandler(Packet packet);
    private static Dictionary<int, PacketHandler> packetHandlers;   //key=packet's id, value=corresponding handler

    private void Awake()                        //gets called before application starts
    {
        if (client == null)
        {
            client = this;                      //set it equal to the instance of Client class
        }
        else if (client != this) {
            Debug.Log("Incorrect instance needs to be destroyed...");
            Destroy(this);                      //only one instance of Client class must exist
        }
    }

    private void Start()                        //initialize references
    {
        tcp = new TCP();
        udp = new UDP();

    }

    public void ConnectToServer() {
        InitializedClientData();
        tcp.ConnectedPlayer();
    }

    public class TCP {

        public TcpClient socket;
        private NetworkStream stream;
        private byte[] receivedBuffer;
        private Packet receivedData;

        public void ConnectedPlayer() {

            socket = new TcpClient{ ReceiveBufferSize = dataBufferSize, SendBufferSize = dataBufferSize };
            receivedBuffer = new byte[dataBufferSize];
            socket.BeginConnect(client.ip, client.port, ConnectionCallback, socket);
        }

        private void ConnectionCallback(IAsyncResult asyncResult) {

            socket.EndConnect(asyncResult);
            if (!socket.Connected) {
                return;
            }
            stream = socket.GetStream();
            receivedData = new Packet();
            stream.BeginRead(receivedBuffer, 0, dataBufferSize, ReceivedCallback, null);
        }

        public void SendData(Packet packet)
        {
            try
            {
                if (socket != null)
                {
                    stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log($"Error sending data to server via TCP: {_ex}");
            }
        }

        private void ReceivedCallback(IAsyncResult asyncResult) {
            try
            {
                int byte_length = stream.EndRead(asyncResult);      //returns number of bytes read from the NetworkStream
                if (byte_length <= 0)
                {
                    return;                                         //get out of the method
                }
                byte[] data = new byte[byte_length];                //if data has been received, create new buffer for the data
                
                Array.Copy(receivedBuffer, data, byte_length);      //copy from one array to another

                receivedData.Reset(HandleData(data));               //reset instance for reuse
                stream.BeginRead(receivedBuffer, 0, dataBufferSize, ReceivedCallback, null);    //continue reading data from the NetworkStream
            }
            catch{ }
        }

        private bool HandleData(byte[] data) {
            int packet_length = 0;
            receivedData.SetBytes(data);                            //set the bytes to the ones we read from the stream 
            if (receivedData.UnreadLength() >= 4)                   //start of packet (an integer consist of 4 bytes)
            {
                packet_length = receivedData.ReadInt();
                if (packet_length <= 0) 
                {
                    return true;                                    //true --> reset to receive new data
                }
            }
            while (packet_length > 0 && packet_length <= receivedData.UnreadLength())     //receivedData contains another complete packet that needs to handled
            {
                byte[] packetBytes = receivedData.ReadBytes(packet_length);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packetBytes))
                    {
                        int packet_id = packet.ReadInt();
                        packetHandlers[packet_id](packet);              //invoke passing packet instance

                    }
                });
                packet_length = 0;
                if (receivedData.UnreadLength() >= 4)                   //start of packet (an integer consist of 4 bytes)
                {
                    packet_length = receivedData.ReadInt();
                    if (packet_length <= 0)
                    {
                        return true;                                    //true --> reset to receive new data
                    }
                }
            }

            if (packet_length <= 1) {
                return true;
            }
            return false;                                               //partial packet exists, so don't reset
        }
    }

    public class UDP 
    {
        public UdpClient socket;
        public IPEndPoint iPEndPoint;

        public UDP() 
        {
            iPEndPoint = new IPEndPoint(IPAddress.Parse(client.ip), client.port);   //pass server's ip and port
        }

        public void ConnectedPlayer(int client_local_port) {

            socket = new UdpClient(client_local_port);
            socket.Connect(iPEndPoint);                             //create default remote host using the specified network endpoint
            socket.BeginReceive(ReceivedCallback, null);
            using (Packet packet = new Packet()) {                  //initialize connection by sending a packet
                SendData(packet);
            }
        }

        public void SendData(Packet packet) {
            try {
                packet.InsertInt(client.local_client_id); //insert client's id into packet (in order for server to understand who send it)
                if (socket != null) {
                    socket.BeginSend(packet.ToArray(), packet.Length(), null, null);
                }
            } catch (Exception e){
                Debug.Log($"Error occurred while sending data to the server via UDP: {e}...");
            }
        }

        private void ReceivedCallback(IAsyncResult asyncResult) {
            try
            {
                byte[] data = socket.EndReceive(asyncResult, ref iPEndPoint);       //ends a pending asynchronous receive and returns bytes of data
                socket.BeginReceive(ReceivedCallback, null);

                if (data.Length < 4)                                               //check if packet_byte exists
                {
                    return;
                }
                HandleData(data);
            }
            catch { }
        }

        private void HandleData(byte[] data) {
            using (Packet packet = new Packet(data)) {
                int packet_length = packet.ReadInt();
                data = packet.ReadBytes(packet_length);
            }
            ThreadManager.ExecuteOnMainThread(() =>
            {
                using (Packet packet = new Packet(data))            //new packet with shortened byte array
                {
                    int packet_id = packet.ReadInt();
                    packetHandlers[packet_id](packet);
                }
            });
        }
    }

    private void InitializedClientData()                            //intialize dictionary of packet data
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int) ServerPackets.welcome, ClientHandle.Welcome},
            { (int) ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer},
             { (int) ServerPackets.playerPosition, ClientHandle.PlayerPosition},
              { (int) ServerPackets.playerRotation, ClientHandle.PlayerRotation}
        };

        Debug.Log("Initialization for packets done");
    }
}
