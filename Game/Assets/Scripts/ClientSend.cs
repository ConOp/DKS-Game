﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTcpData(Packet packet)
    {
        packet.WriteLength();
        Client.client.tcp.SendData(packet);                           //send given packet from client to server 
    }

    public static void Welcome_Received()                               //creates an client's first packet, after receiving welcome packet from server
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(Client.client.local_client_id);
            packet.Write(UIManager.manager.username.text);
            SendTcpData(packet);                                        //send the created packet to the server
        }
    }

    public static void PlayerMovement(bool[] inputs)
    {
        using (Packet packet = new Packet((int)ClientPackets.player_movement))
        {
            packet.Write(inputs.Length);
            foreach (bool input in inputs)
            {
                packet.Write(input);
            }
            packet.Write(GameManager.players[Client.client.local_client_id].transform.rotation);

            SendTcpData(packet);
        }
    }

}
