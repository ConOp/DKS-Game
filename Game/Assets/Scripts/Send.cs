﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Send                                      //logic for sending packets from client to server
{
    public static void Welcome_Received()                               //creates an client's first packet, after receiving welcome packet from server (like handshake)
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(ClientManager.GetInstance().local_client_id);
            packet.Write(ConnectionManager.GetInstance().username.text);
            SendTcpData(packet);                                        //send the created packet to the server
        }
    }

    private static void SendTcpData(Packet packet)
    {
        packet.WriteLength();
        ClientManager.GetInstance().tcp.SendData(packet);                           //send given packet from client to server (using tcp)
    }

    private static void SendUdpData(Packet packet)
    {
        packet.WriteLength();
        ClientManager.GetInstance().udp.SendData(packet);                         //send given packet from client to server (using udp)
    }

    public static void PlayerMovement(bool[] inputs)                //send player's inputs (about movement) to the server 
    {
        using (Packet packet = new Packet((int)ClientPackets.player_movement))
        {
            packet.Write(inputs.Length);
            foreach (bool input in inputs)
            {
                packet.Write(input);
            }
            packet.Write(GameManager.players[ClientManager.GetInstance().local_client_id].transform.rotation);

            SendUdpData(packet);                                    //movement packet will be sent over and over again (can't afford losing some of them)
        }
    }

    public static void Shoot(Vector3 facing_direction)              //specified direction that local player is shooting
    {
        using (Packet packet = new Packet((int) ClientPackets.shoot)) 
        {
            packet.Write(facing_direction);
            SendTcpData(packet);                                    //send packet to server via tcp
        }
    }
}