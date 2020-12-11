using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
        public static void Welcome_Received(int fromClient, Packet packet) {
            int client_id_received = packet.ReadInt();
            string username_received = packet.ReadString();
            Console.WriteLine($"{Server.clients[client_id_received].tcp.socket.Client.RemoteEndPoint} connected with username {username_received} and id {client_id_received}");
            if (fromClient != client_id_received) {
                Console.WriteLine("Wrong client id...");
            }
            Server.clients[fromClient].SendInfoGame(username_received);
        }
        public static void PlayerMovement(int fromClient, Packet packet) {
            bool[] inputs = new bool[packet.ReadInt()];
            for (int i = 0; i < inputs.Length; i++) {
                inputs[i] = packet.ReadBool();
            }
            Quaternion rotation = packet.ReadQuaternion();
            Server.clients[fromClient].player.SetInput(inputs, rotation);
        }
    }
}
