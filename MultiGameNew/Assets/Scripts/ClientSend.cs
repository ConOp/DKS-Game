using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTcpData(Packet packet) {
        packet.WriteLength();
        Client.client.tcp.SendData(packet);
    }

    private static void SendUdpData(Packet packet) {
        packet.WriteLength();
        Client.client.udp.SendData(packet);
    }

    public static void Welcome_Received() {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived)) {
            packet.Write(Client.client.local_client_id);
            packet.Write(UIManager.manager.username.text);
            SendTcpData(packet);
        }
    }

    public static void PlayerMovement(bool[] inputs) {
        using (Packet packet = new Packet((int)ClientPackets.playerMovement)) {
            packet.Write(inputs.Length);
            foreach (bool inp in inputs) {
                packet.Write(inp);
            }
            packet.Write(GameManager.players[Client.client.local_client_id].transform.rotation);

            SendUdpData(packet);
        }
    }

}
