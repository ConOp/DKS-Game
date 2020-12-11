using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    class Server
    {
        public static int MaxPlayers{ get; private set; }                           //to store max number of players
        public static int Port { get; private set; }                                //store port for connection
        private static TcpListener tcpListener;                                     //listens for connections from TCP network clients
        private static UdpClient udpListener;                                       //will manage all udp communication

        public static Dictionary<int, Client> clients = new Dictionary<int, Client>(); //(key = client's id, value = instance of Cslient)
        public delegate void PacketHandler(int fromClient, Packet packet);
        public static Dictionary<int, PacketHandler> packetHandlers;

        public static void Start(int maxPlayers, int port) {                        //necessary setup for server
            MaxPlayers = maxPlayers;
            Port = port;

            Console.WriteLine("Starting server...");
            Console.WriteLine("Waiting for a connection...");
            InitializedServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);                     //initialize new instance of the TcpListener, listens for incoming connection attempts on the specified local IP address and port number
            tcpListener.Start();                                                    //start listener for incoming connection requests
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), null); //creates accepted socket

            udpListener = new UdpClient(Port);
            udpListener.BeginReceive(UdpReceivedCallback, null);
            
            Console.WriteLine($"Server started successfully on {Port}!!!");

        }

        private static void AcceptTcpClientCallback(IAsyncResult asyncResult) {

            TcpClient client = tcpListener.EndAcceptTcpClient(asyncResult);         //accepts an incoming connection attempt, creates and returns a new TcpClient to handle remote host communication
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), null); //continue listening for connections (once a client connects)
            
            Console.WriteLine($"Incoming connection from ... {client.Client.RemoteEndPoint}");
            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null) {    
                    clients[i].tcp.Connect(client);           //assign newly connected tcp client instance to an id
                    return;
                }
            }
            Console.WriteLine($"{client.Client.RemoteEndPoint} failed remote client to connect --> full server");
        }

        private static void UdpReceivedCallback(IAsyncResult asyncResult) {
            try
            {
                IPEndPoint iPEnd_client = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = udpListener.EndReceive(asyncResult, ref iPEnd_client);
                udpListener.BeginReceive(UdpReceivedCallback, null);

                if (data.Length < 4) {
                    return;
                }

                using (Packet packet = new Packet(data)) {
                    int client_id = packet.ReadInt();
                    if (client_id == 0) {
                        return;
                    }
                    if (clients[client_id].udp.iPEndPoint == null)                          //new connection
                    {
                        clients[client_id].udp.Connect(iPEnd_client);
                        return;
                    }
                    if (clients[client_id].udp.iPEndPoint.ToString() == iPEnd_client.ToString()) {
                        clients[client_id].udp.HandleData(packet);
                    }
                }
            }
            catch (Exception e) { Console.WriteLine($"Error occurred while receiving UDP data from server: {e}"); }
        }

        public static void SendUdpData(IPEndPoint iPEnd_client, Packet packet) {
            try
            {
                if (iPEnd_client != null) {
                    udpListener.BeginSend(packet.ToArray(), packet.Length(), iPEnd_client, null, null);
                }
            }
            catch (Exception e){ Console.WriteLine($"Error occurred while sending data to {iPEnd_client} via UDP: {e}"); }
        }

        private static void InitializedServerData() {   //initialize dictionary of clients
            for (int i = 1; i <= MaxPlayers; i++) {
                clients.Add(i, new Client(i));          //(key,value)
            }
            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int) ClientPackets.welcomeReceived, ServerHandle.Welcome_Received},
                 { (int) ClientPackets.playerMovement, ServerHandle.PlayerMovement}
            };
            Console.WriteLine($"Initiliazation of packets have been completed");
        }
    }
}
