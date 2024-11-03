// TcpClient

using System.Net.Sockets;
using System.Text;

using TcpClient client = new();

try
{
    // receive
    await client.ConnectAsync(System.Net.IPAddress.Loopback, 5000);
    Console.WriteLine($"client {client.Client.LocalEndPoint} connected");
    Console.WriteLine($"server {client.Client.RemoteEndPoint}");

    byte[] buffer = new byte[1024];
    NetworkStream stream = client.GetStream();
    int bufferSize = await stream.ReadAsync(buffer);

    Console.WriteLine($"Client receive message: {Encoding.UTF8.GetString(buffer)}");

    Thread.Sleep(2000);

    // send
    string message = "Good by server. " + DateTime.Now.ToString();
    buffer = Encoding.UTF8.GetBytes(message);

    await stream.WriteAsync(buffer);
    Console.WriteLine($"Client send message: {message}");
}
catch(Exception)
{
    Console.WriteLine("client not connected");
}