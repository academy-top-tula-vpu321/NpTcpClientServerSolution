// Socket Client

using System.Net;
using System.Net.Sockets;
using System.Text;

using (Socket clientSocket = new Socket(AddressFamily.InterNetwork,
                                 SocketType.Stream,
                                 ProtocolType.Tcp))
{
    try
    {
        await clientSocket.ConnectAsync(IPAddress.Loopback, 5000);
        
        byte[] bufferSize = new byte[4];
        await clientSocket.ReceiveAsync(bufferSize);
        byte[] buffer = new byte[BitConverter.ToInt32(bufferSize, 0)];
        int receiveSize = await clientSocket.ReceiveAsync(buffer);

        Console.WriteLine($"Client receiver message: {Encoding.UTF8.GetString(buffer)}");

        Thread.Sleep(2000);

        string message = "Good by world. " + DateTime.Now.ToString();
        buffer = Encoding.UTF8.GetBytes(message);

        await clientSocket.SendAsync(BitConverter.GetBytes(buffer.Length));
        await clientSocket.SendAsync(buffer);
        Console.WriteLine($"Client send message: {message}");
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}