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

        private static void SendDataToAll(Packet packet)                    //send data to all connected-remote clients
        {
            packet.WriteLength();
            for (int i = 1; i <= Server.maximum_players; i++)
            {
                Server.clients[i].tcp.SendData(packet);                     //send packet to each client
            }
        }

        private static void SendDataToAll(int excepted_client, Packet packet) //send data to all remote clients except one specified client
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
    }
}
