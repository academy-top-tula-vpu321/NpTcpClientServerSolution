// TcpListener

using System.Net;
using System.Net.Sockets;

TcpListener server = new TcpListener(IPAddress.Loopback, 5000);

server.Start();
Console.WriteLine("Server starting");

while(true)
{
    TcpClient client = await server.AcceptTcpClientAsync();
    Console.WriteLine($"Server accept client: {client.Client.RemoteEndPoint}");
}


