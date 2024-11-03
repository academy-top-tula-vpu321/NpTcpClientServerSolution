// TcpListener

using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = new TcpListener(IPAddress.Loopback, 5000);

server.Start();
Console.WriteLine($"Server starting. Server end point: {server.LocalEndpoint}");

while(true)
{
    // send to client
    TcpClient client = await server.AcceptTcpClientAsync();
    Console.WriteLine($"Server accept client: {client.Client.RemoteEndPoint}");

    NetworkStream stream = client.GetStream();
    string message = "Hello client! " + DateTime.Now.ToString();
    byte[] buffer = Encoding.UTF8.GetBytes(message);

    await stream.WriteAsync(buffer);
    Console.WriteLine($"Server send message: {message}, to client {client.Client.RemoteEndPoint}");

    // receive
    StringBuilder responseString = new();
    int receiveSize;

    do
    {
        receiveSize = await stream.ReadAsync(buffer);
        responseString.Append(Encoding.UTF8.GetString(buffer, 0, receiveSize));

    } while (receiveSize > 0);

    Console.WriteLine($"Server receive message: {responseString.ToString()}");
}


