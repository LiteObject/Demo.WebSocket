using System.Net;
using System.Net.Sockets;

namespace Demo.WebSocket.Server
{
    /// <summary>
    /// Original:
    /// https://developer.mozilla.org/en-US/docs/Web/API/WebSockets_API/Writing_WebSocket_server
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
                Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8080));
                serverSocket.Listen(128);
                serverSocket.BeginAccept(null, 0, OnAccept, null);
             */

            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);

            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection…", Environment.NewLine);

            TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("A client connected.");

            NetworkStream stream = client.GetStream();

            // an infinite cycle to be able to handle every change in stream
            while (true)
            {
                while (!stream.DataAvailable)
                {
                    Byte[] bytes = new Byte[client.Available];
                    stream.Read(bytes, 0, bytes.Length);
                }
            }
        }

        /*private void OnAccept(IAsyncResult result)
        {
            try
            {
                Socket client = null;
                if (serverSocket != null && serverSocket.IsBound)
                {
                    client = serverSocket.EndAccept(result);
                }
                if (client != null)
                {
                    // Handshaking and managing ClientSocket
                }
            }
            catch (SocketException exception)
            {

            }
            finally
            {
                if (serverSocket != null && serverSocket.IsBound)
                {
                    serverSocket.BeginAccept(null, 0, OnAccept, null);
                }
            }
        }*/
    }
}