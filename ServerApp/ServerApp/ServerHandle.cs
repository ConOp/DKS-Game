using System;
using System.Collections.Generic;
using System.Text;

namespace ServerApp
{
    class ServerHandle
    {
        public static void Welcome_Received(int fromClient, Packet packet)          //server receives welcome packet from client
        {
            int client_id_received = packet.ReadInt();
            string username_received = packet.ReadString();
            Console.WriteLine($"{Server.clients[client_id_received].tcp.socket.Client.RemoteEndPoint} connected with username {username_received} and id {client_id_received}");
            if (fromClient != client_id_received)
            {
                Console.WriteLine("Wrong client id...");
            }
            //Server.clients[fromClient].SendInfoGame(username_received);
        }
    }
}
