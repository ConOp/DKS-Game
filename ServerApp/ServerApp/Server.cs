using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerApp
{
    class Server
    {
        public static int maximum_players { get; private set; }             //automatic property
        public static int port { get; private set; }

        private static TcpListener server = null;
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>(); //(key = client's id, value = instance of Client)

        public static void StartServer(int m, int p)
        {
            maximum_players = m; port = p;

            Console.WriteLine("Starting server...\nWaiting for connections...");
            server = new TcpListener(IPAddress.Any, port);
            server.Start();                                                                   //start listening for client requests
            server.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), null);    //begin an asynchronous operation to accept an incoming connection attempt
            Console.WriteLine($"Server started successfully on {port}...");
        }

        private static void AcceptTcpClientCallback(IAsyncResult asyncResult)
        {
            TcpClient client = server.EndAcceptTcpClient(asyncResult);
            server.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), null); //continue listening for connections (once a client connects)
            
            Console.WriteLine($"Incoming connection from ... {client.Client.RemoteEndPoint}");
            for (int i = 1; i <= maximum_players; i++)
            {
                if (clients[i].tcp.socket == null)
                {
                    clients[i].tcp.Connect(client);           //assign newly connected tcp client instance to an id
                    return;
                }
            }
            Console.WriteLine($"{client.Client.RemoteEndPoint} failed remote client to connect --> full server");
        }

        private static void InitializedServerData()                                       //initialize dictionary of clients
        {
            for (int i = 1; i <= maximum_players; i++)
            {
                clients.Add(i, new Client(i));          //(key,value)
            }
        }
    }
}
