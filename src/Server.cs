using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class RedisServer
{
  public static void Main(string[] args)
  {
    // Start listening on port 6379
    TcpListener server = new TcpListener(IPAddress.Any, 6379);
    server.Start();
    Console.WriteLine("Redis server is running...");

    while (true)
    {
      // Accept incoming client connection
      using (Socket clientSocket = server.AcceptSocket())
      {
        byte[] buffer = new byte[256];
        int bytesRead = clientSocket.Receive(buffer);

        // Convert received bytes to string
        string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"Received request: {request}");

        // Send response for PING command
        if (request.Contains("PING"))
        {
          byte[] response = Encoding.UTF8.GetBytes("+PONG\r\n");
          clientSocket.Send(response, SocketFlags.None);
        }
      }
    }
  }
}
