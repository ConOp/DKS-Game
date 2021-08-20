using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;


public class UDP
{
    public UdpClient socket;
    public IPEndPoint iPEndPoint;                                   //represents a network endpoint as an IP address and a port number

    public UDP()                                                    //constructor of UDP class
    {
        iPEndPoint = new IPEndPoint(IPAddress.Parse(Client.client.Ip), Client.client.Port);
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
                Client.client.Disconnect();
                return;
            }
            HandleData(data);                                                   //packet exists
        }
        catch
        {
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
                Client.client.Handlers[packet_id](packet);                       //invoke passing packet instance
            }
        });
    }

    public void SendData(Packet packet)                             //send packet to server using udp
    {
        try
        {
            packet.InsertInt(Client.client.local_client_id);        //insert client's id into packet (in order for server to understand who send it)
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

    private void Disconnect()
    {                                     //disconnect client from server and clean necessary UDP instances
        Client.client.Disconnect();
        iPEndPoint = null;
        socket = null;
    }
}
