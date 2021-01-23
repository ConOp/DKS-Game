using System;
using System.Collections.Generic;
using System.Text;

namespace ServerApp
{
    class ServerSend                                                        //create and prepare packets to be sent
    {
        public static void Welcome(int toClient, string message)            //which client to send the packet, message to be sent
        {
            using (Packet packet = new Packet((int)ServerPackets.welcome))  //create the welcome packet (using statement ensures the correct use of IDisposable objects)
            {
                packet.Write(message);                                      //fill with data (write a message and client's id)
                packet.Write(toClient);

                SendTcpData(toClient, packet);                              //sent packet to specified client
            }
        }
        //----------------------------------------------send TCP data--------------------------------------------
        private static void SendTcpData(int toClient, Packet packet)        //prepare packet to be sent
        {
            packet.WriteLength();                                           //take packet's content length and put it at the beginning of the packet (buffer)
            Server.clients[toClient].tcp.SendData(packet);                  //send data from server to the specified client
        }

        private static void SendTcpDataToAll(Packet packet)                    //send data to all connected-remote clients
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.maximum_players; i++)
            {
                Server.clients[i].tcp.SendData(packet);                     //send packet to each client
            }
        }

        private static void SendTcpDataToAll(int excepted_client, Packet packet) //send data to all remote clients except one specified client
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.maximum_players; i++)
            {
                if (i != excepted_client)                                     //check for the specified excepted client
                {
                    Server.clients[i].tcp.SendData(packet);
                }
            }
        }

        //----------------------------------------------send UDP data--------------------------------------------
        private static void SendUdpData(int to_client, Packet packet)       //prepare packet to be sent
        {
            packet.WriteLength();
            Server.clients[to_client].udp.SendData(packet);
        }

        private static void SendUdpDataToAll(Packet packet)                 //send data to all remote clients (connected)
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.maximum_players; i++)
            {
                Server.clients[i].udp.SendData(packet);                     //send packet to each client
            }
        }

        private static void SendUdpDataToAll(int exceptClient, Packet packet) //send data to all remote clients except specified one
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.maximum_players; i++)
            {
                if (i != exceptClient)                                     //check for the specific client
                {
                    Server.clients[i].udp.SendData(packet);
                }
            }
        }

        //---------------------------------------------player initialization and movement------------------------
        public static void Generate(int toClient, Player player)
        {
            using (Packet packet = new Packet((int)ServerPackets.generated_player))     //after generating player send him a test packet
            {
                packet.Write(player.player_id);
                packet.Write(player.username);
                packet.Write(player.position);
                packet.Write(player.rotation);
                SendTcpData(toClient, packet);
            }
        }

        public static void PlayerPosition(Player player)                        //inform all players about a player's position
        {
            using (Packet packet = new Packet((int)ServerPackets.player_position))
            {
                packet.Write(player.player_id);
                packet.Write(player.position);                                  //write to the packet player's new position
                SendUdpDataToAll(packet);
            }
        }

        public static void PlayerRotation(Player player)                        //inform all players about a player's rotation
        {
            using (Packet packet = new Packet((int)ServerPackets.player_rotation))
            {
                packet.Write(player.player_id);
                packet.Write(player.rotation);                                  //write to the packet player's new rotation
                SendUdpDataToAll(player.player_id, packet);                        //inform everyone except the player that has been rotating
            }
        }
    }
}
