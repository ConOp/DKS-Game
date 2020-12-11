using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class ServerSend
    {
        public static void Welcome(int toClient, string message)            //which client to send the packet, message to be sent
        {
            using (Packet packet = new Packet((int)ServerPackets.welcome))  //create the packet
            {
                packet.Write(message);                                      //fill with data
                packet.Write(toClient);

                SendTcpData(toClient, packet);
            }
        }

        //-----------------TCP DATA---------------------------------

        private static void SendTcpData(int to_client, Packet packet )      //prepare packet to be sent
        {
            packet.WriteLength();                                           //take packet's content length and put it at the beginning of the buffer
            Server.clients[to_client].tcp.SendData(packet);                 //send data from server to the client
        }

        private static void SendDataToAll(Packet packet)                    //send data to all remote clients (connected)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(packet);                     //send packet to each client
            }
        }

        private static void SendDataToAll(int exceptClient, Packet packet) //send data to all remote clients except one
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != exceptClient)                                     //check for the specific client
                {
                    Server.clients[i].tcp.SendData(packet);
                }
            }
        }

        //-----------------------------UDP Data-------------------------

        private static void SendUdpData(int to_client, Packet packet) {
            packet.WriteLength();
            Server.clients[to_client].udp.SendData(packet);
        }

        private static void SendUdpDataToAll(Packet packet)                    //send data to all remote clients (connected)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(packet);                     //send packet to each client
            }
        }

        private static void SendUdpDataToAll(int exceptClient, Packet packet) //send data to all remote clients except one
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != exceptClient)                                     //check for the specific client
                {
                    Server.clients[i].udp.SendData(packet);
                }
            }
        }

        public static void SpawnPlayer(int toClient, Player player) {
            using (Packet packet = new Packet((int)ServerPackets.spawnPlayer)) {
                packet.Write(player.player_id);
                packet.Write(player.username);
                packet.Write(player.position);
                packet.Write(player.rotation);
                SendTcpData(toClient, packet);
            }
        }

        public static void PlayerPosition(Player player) {
            using (Packet packet = new Packet((int) ServerPackets.playerPosition)) {
                packet.Write(player.player_id);
                packet.Write(player.position);
                SendUdpDataToAll(packet);
            }
        }

        public static void PlayerRotation(Player player)
        {
            using (Packet packet = new Packet((int)ServerPackets.playerRotation))
            {
                packet.Write(player.player_id);
                packet.Write(player.rotation);
                SendUdpDataToAll(player.player_id,packet);
            }
        }
    }
}
