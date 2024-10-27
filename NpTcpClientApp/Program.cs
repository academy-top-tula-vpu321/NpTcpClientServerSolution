// TcpClient

using System.Net.Sockets;

using TcpClient client = new();

try
{
    await client.ConnectAsync(System.Net.IPAddress.Loopback, 5000);
    Console.WriteLine($"client {client.Client.LocalEndPoint} connected");
}
catch(Exception)
{
    Console.WriteLine("client not connected");
}