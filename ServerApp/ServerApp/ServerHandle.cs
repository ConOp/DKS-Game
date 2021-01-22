using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

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
            Server.clients[fromClient].SendToGame(username_received);
        }

        public static void PlayerMovement(int fromClient, Packet packet)        //extract info sent to server about player's movement
        {
            bool[] inputs = new bool[packet.ReadInt()];
            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i] = packet.ReadBool();
            }
            Quaternion rotation = packet.ReadQuaternion();
            Server.clients[fromClient].player.SetInput(inputs, rotation);
        }
    }
}
