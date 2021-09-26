using UnityEngine;
using System.Net.Sockets;
using System;

public class TCP
{
    private TcpClient socket;
    public TcpClient Socket => socket;
    private NetworkStream stream;
    private byte[] read_buffer;
    private Packet received_packet;            //packet (sent by server) and received from client

    public void ConnectedPlayer()               //try to connect local player to server using tcp
    {
        socket = new TcpClient { ReceiveBufferSize = Client.dataBufferSize, SendBufferSize = Client.dataBufferSize };
        read_buffer = new byte[Client.dataBufferSize];
        socket.BeginConnect(Client.client.Ip, Client.client.PortNum, TCPConnectionCallback, socket);
    }

    private void TCPConnectionCallback(IAsyncResult asyncResult) //gets called after client's successful connection to server via tcp and initializes necessary components for the communication
    {
        socket.EndConnect(asyncResult);                          //end asynchronous pending connection request
        if (!socket.Connected)
        {
            return;
        }
        stream = socket.GetStream();                            //get NetworkStream that used to send and receive data
        received_packet = new Packet();                         //initialize Packet instance
        stream.BeginRead(read_buffer, 0, Client.dataBufferSize, TcpNetworkStreamCallback, null);   //begin asynchronous reading from NetworkStream
    }

    private void TcpNetworkStreamCallback(IAsyncResult asyncResult)     //read received data-packet from the NetworkStream
    {
        try
        {
            int bytes_length = stream.EndRead(asyncResult);     //returns number of bytes read from the NetworkStream
            if (bytes_length <= 0)
            {
                Client.client.Disconnect();                     //will disconnect both tcp and udp connections
                return;                                         //get out of the method
            }
            byte[] data = new byte[bytes_length];               //if data has been received, create new buffer for the data

            Array.Copy(read_buffer, data, bytes_length);        //copy from one array to another
            received_packet.Clear(HandleData(data));            //clear Packet instance so it can be reused, but first get (handle) data from the packet
            stream.BeginRead(read_buffer, 0, Client.dataBufferSize, TcpNetworkStreamCallback, null);    //continue reading data from the NetworkStream
        }
        catch
        {
            Disconnect();                                       //will disconnect both tcp and udp connections
        }
    }

    public void SendData(Packet packet)                         //send given packet to server using tcp
    {
        try
        {
            if (socket != null)
            {
                stream.BeginWrite(packet.ToArray(), 0, packet.ContentLength(), null, null);    //begin asynchronous writing to the NetworkStream
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
                    Client.client.Handlers[packet_id](packet);              //invoke passing packet instance (call appropriate method to handle Packet)
                }
            });

            packet_length = 0;                                   //reset packet's length
            if (received_packet.UnreadLength() >= 4)             //this is the start of packet (first value placed in the packet is its content's length, which is an integer [int consists of 4 bytes])
            {
                packet_length = received_packet.ReadInt();      //get packet's length of data that was sent from server and received from client (meaning local player)
                if (packet_length <= 0)                         //no data stored inside the packet
                {
                    return true;                                //true --> reset packet in order to receive new data
                }
            }
        }

        if (packet_length <= 1)
        {
            return true;
        }
        return false;                                               //partial packet exists, so don't reset }
    }

    private void Disconnect()
    {                                 //disconnect client from server and clean necessary TCP instances
        Client.client.Disconnect();
        stream = null;
        received_packet = null;
        read_buffer = null;
        socket = null;
    }
}
