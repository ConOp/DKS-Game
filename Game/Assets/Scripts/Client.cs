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
        tcp = new TCP();
    }

    public void ConnectToServer()
    {
        InitializedClientData();
        tcp.ConnectedPlayer();
    }

    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        private byte[] received_buffer;
        private Packet received_packet;            //packet (sent by server) and received from client

        public void ConnectedPlayer()
        {
            socket = new TcpClient { ReceiveBufferSize = dataBufferSize, SendBufferSize = dataBufferSize };
            received_buffer = new byte[dataBufferSize];
            socket.BeginConnect(client.ip, client.port, ConnectionCallback, socket);
        }

        private void ConnectionCallback(IAsyncResult asyncResult)
        {
            socket.EndConnect(asyncResult);
            if (!socket.Connected)
            {
                return;
            }
            stream = socket.GetStream();
            received_packet = new Packet();                         //initialize Packet instance
            stream.BeginRead(received_buffer, 0, dataBufferSize, ReceivedCallback, null);
        }

        private void ReceivedCallback(IAsyncResult asyncResult)
        {
            try
            {
                int bytes_length = stream.EndRead(asyncResult);     //returns number of bytes read from the NetworkStream
                if (bytes_length <= 0)
                {
                    return;                                         //get out of the method
                }
                byte[] data = new byte[bytes_length];               //if data has been received, create new buffer for the data

                Array.Copy(received_buffer, data, bytes_length);    //copy from one array to another
                received_packet.Reset(HandleData(data));            //reset Packet instance so it can be reused, but first get data from the packet
                stream.BeginRead(received_buffer, 0, dataBufferSize, ReceivedCallback, null);    //continue reading data from the NetworkStream
            }
            catch { }
        }

        public void SendData(Packet packet)                         //send packet to server
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
                Debug.Log($"Error sending data from client to server via TCP: {_ex}");
            }
        }

        private bool HandleData(byte[] data)                        //returns a boolean, telling Packet's Reset() whether the instance should be cleared
        {
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
            {
                byte[] packet_bytes = received_packet.ReadBytes(packet_length);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packet_bytes))
                    {
                        int packet_id = packet.ReadInt();
                        packetHandlers[packet_id](packet);              //invoke passing packet instance
                    }
                });

                packet_length = 0;                                      //reset packet's length
                if (received_packet.UnreadLength() >= 4)                //this is the start of packet (first value placed in the packet is its content's length, which is an integer [int consists of 4 bytes])
                {
                    packet_length = received_packet.ReadInt();          //get packet's length of data that was sent from server and received from client (meaning local player)
                    if (packet_length <= 0)                             //no data stored inside
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
    }

    private void InitializedClientData()                                //intialize dictionary of packet data
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            { (int) ServerPackets.welcome, ClientHandle.Welcome},
            { (int) ServerPackets.generated_player, ClientHandle.Generate},
        };

        Debug.Log("Initialization for packets done");
    }
}
