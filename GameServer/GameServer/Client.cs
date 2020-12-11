using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Numerics;

namespace GameServer
{
    class Client
    {
        public int id;                              //client's id
        public TCP tcp;                             //reference to client's tcp
        public UDP udp;                             //reference to client's udp
        public Player player;
        public static int dataBufferSize = 4096;    //bytes
        public Client(int client_id) {              //constructor of outter class Client
            id = client_id;
            tcp = new TCP(id);                      //initialize Client's TCP instance
            udp = new UDP(id);
        }

        public class TCP {                          //inner class

            public TcpClient socket;                //will store the instance returned in server's accept connection callback method
            private readonly int client_id;
            private NetworkStream stream;           //for communication with the remote host (send and receive data)
            private byte[] receivedBuffer;          //location of memory to store data read from NetworkStream
            private Packet receivedData;

            public TCP(int id) {                    //constructor of inner class for initialization
                client_id = id;
            }

            public void Connect(TcpClient c_socket) {
                socket = c_socket;                                  //initialize TCP's socket variable with the instance from Server's AcceptTcpClientCallback method
                socket.ReceiveBufferSize = dataBufferSize;          //set size of the receive buffer (bytes)
                socket.SendBufferSize = dataBufferSize;             //set the size of the send buffer (bytes)

                stream = socket.GetStream();                        //returns the NetworkStream used to send and receive data from TcpClient
                receivedData = new Packet();
                receivedBuffer = new byte[dataBufferSize];

                stream.BeginRead(receivedBuffer, 0, dataBufferSize, ReceivedCallback, null); //begin to read from NetworkStream
                
                ServerSend.Welcome(client_id, "Brief welcome to the server!!");
            }

            public void SendData(Packet packet) {

                try
                {
                    if (socket != null) {

                        stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
                    }
                }
                catch (Exception e) {
                    Console.WriteLine($"Error sending packet data to {client_id} via TCP {e}...");
                }
            }

            private void ReceivedCallback(IAsyncResult asyncResult) {    //called when BeginRead() completes
                try
                {
                    int byte_length = stream.EndRead(asyncResult);      //returns number of bytes read from the NetworkStream
                    if (byte_length <= 0) {
                        return;                                         //get out of the method
                    }
                    byte[] data = new byte[byte_length];                //if data has been received, create new buffer for the data
                    Array.Copy(receivedBuffer, data, byte_length);      //copy from one array to another
                    receivedData.Reset(HandleData(data));
                    stream.BeginRead(receivedBuffer, 0, dataBufferSize, ReceivedCallback, null);    //continue reading data from the NetworkStream
                }
                catch (Exception e) {
                    Console.WriteLine($"Error occured during the process: {e}");
                }
            }

            private bool HandleData(byte[] data)
            {
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
                            Server.packetHandlers[packet_id](client_id,packet);              //invoke passing packet instance

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

                if (packet_length <= 1)
                {
                    return true;
                }
                return false;                                               //partial packet exists, so don't reset
            }
        }

        public class UDP {
            public IPEndPoint iPEndPoint;
            private int client_id;                  //will store client's id of udp connection

            public UDP(int id) {
                client_id = id;
            }

            public void Connect(IPEndPoint endPoint)
            {
                iPEndPoint = endPoint;
            }

            public void SendData(Packet packet) {
                Server.SendUdpData(iPEndPoint, packet);
            }

            public void HandleData(Packet packet_data) {
                int packet_length = packet_data.ReadInt();
                byte[] packet_bytes = packet_data.ReadBytes(packet_length);

                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packet_bytes)) {
                        int packet_id = packet.ReadInt();
                        Server.packetHandlers[packet_id](client_id, packet);
                    }

                });
            }
        }

        public void SendInfoGame(string player_name) {
            player = new Player(id, player_name, new Vector3(0, 0, 0));
            foreach (Client client in Server.clients.Values) {
                if (client.player != null) {
                    if (client.id != id) {
                        ServerSend.SpawnPlayer(id, client.player);
                    }
                }
            }
            foreach (Client client in Server.clients.Values) {
                if (client.player != null) {
                    ServerSend.SpawnPlayer(client.id, player);
                }
            }
            
        }
    }
}
