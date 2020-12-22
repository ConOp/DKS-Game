using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerApp
{
    class Client
    {
        public int client_id;
        public TCP tcp;
        public static int dataBufferSize = 4096;    //bytes

        public Client(int ci)                 //constructor of outter class Client
        {
            client_id = ci;
            tcp = new TCP(client_id);
        }

        public class TCP 
        {
            public TcpClient socket;
            private readonly int id;
            private NetworkStream stream;
            private byte[] receivedBuffer;

            public TCP(int i)                                       //constructor of inner class TCP
            {                    
                id = i;
            }

            public void Connect(TcpClient client_socket)
            {
                socket = client_socket;
                socket.ReceiveBufferSize = dataBufferSize;
                socket.SendBufferSize = dataBufferSize;

                stream = socket.GetStream();                        //get the NetworkStream used to send and receive data from TcpClient
                receivedBuffer = new byte[dataBufferSize];
                stream.BeginRead(receivedBuffer, 0, receivedBuffer.Length, ReceivedCallback, null); //begin to read from NetworkStream
            }

            private void ReceivedCallback(IAsyncResult asyncResult)
            {
                try
                {
                    int byte_length = stream.EndRead(asyncResult);
                    if (byte_length <= 0)
                    {
                        return;
                    }
                    byte[] data = new byte[byte_length];                //if data has been received, create new buffer for the data
                    Array.Copy(receivedBuffer, data, byte_length);      //copy from one array to another
                    
                    stream.BeginRead(receivedBuffer, 0, dataBufferSize, ReceivedCallback, null);    //continue reading data from the NetworkStream
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error occured while receiving TCP data: {e}");
                }
            }
         }
    }
}
