using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
public class Client : MonoBehaviour
{
    public static Client client;                //client instance
    public static int dataBufferSize = 4096;    //bytes

    public string ip = "127.0.0.1";             //server ip for localhost
    public int port = 26950;                    //port number
    public int local_client_id = 0;             //local client's id
    public TCP tcp;                             //reference to client's TCP class

    private void Awake()
    {
        if (client == null)
        {
            client = this;                      //set it equal to the instance of Client class
        }
        else if (client != this)
        {
            Debug.Log("Incorrect instance needs to be destroyed...");
            Destroy(this);                      //only one instance of Client class must exist
        }
    }
    //Start is called before the first frame update
    private void Start()
    {
        tcp = new TCP();
    }

    public void ConnectToServer()
    {
        tcp.ConnectedPlayer();
    }

    public class TCP
    {
        public TcpClient socket;
        private NetworkStream stream;
        private byte[] receivedBuffer;

        public void ConnectedPlayer()
        {
            socket = new TcpClient { ReceiveBufferSize = dataBufferSize, SendBufferSize = dataBufferSize };
            receivedBuffer = new byte[dataBufferSize];
            socket.BeginConnect(client.ip, client.port, ConnectionCallback, socket);
        }

        private void ConnectionCallback(IAsyncResult asyncResult)
        {
            socket.EndConnect(asyncResult);
            if (!socket.Connected)
            {
                return;
            }
            stream = socket.GetStream();
            stream.BeginRead(receivedBuffer, 0, dataBufferSize, ReceivedCallback, null);
        }

        private void ReceivedCallback(IAsyncResult asyncResult)
        {
            try
            {
                int byte_length = stream.EndRead(asyncResult);      //returns number of bytes read from the NetworkStream
                if (byte_length <= 0)
                {
                    return;                                         //get out of the method
                }
                byte[] data = new byte[byte_length];                //if data has been received, create new buffer for the data

                Array.Copy(receivedBuffer, data, byte_length);      //copy from one array to another
                stream.BeginRead(receivedBuffer, 0, dataBufferSize, ReceivedCallback, null);    //continue reading data from the NetworkStream
            }
            catch { }
        }

    }

}
