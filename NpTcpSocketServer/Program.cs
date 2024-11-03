// Socket Server

using System.Net;
using System.Net.Sockets;
using System.Text;

IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 5000);

using(Socket serverSocket  = new Socket(AddressFamily.InterNetwork,
                                        SocketType.Stream,
                                        ProtocolType.Tcp))
{
    try
    {
        serverSocket.Bind(endPoint);
        serverSocket.Listen();
        Console.WriteLine("Server starting");

        while (true)
        {
            using (var client = await serverSocket.AcceptAsync())
            {
                // send
                string message = "Hello world. " + DateTime.Now.ToString();
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                byte[] bufferSize = BitConverter.GetBytes(buffer.Length);

                await client.SendAsync(bufferSize);
                await client.SendAsync(buffer);
                Console.WriteLine($"Server send message: {message}");

                // receive
                //List<byte> response = new List<byte>();
                //buffer = new byte[1024];
                //int chankSize;
                //do
                //{
                //    chankSize = await client.ReceiveAsync(buffer);
                //    response.AddRange(buffer.Take(chankSize));
                //} while (chankSize > 0);
                //Console.WriteLine($"Server receive message: {Encoding.UTF8.GetString(response.ToArray())}");

                // receive with data size
                await client.ReceiveAsync(bufferSize);
                buffer = new byte[BitConverter.ToInt32(bufferSize, 0)];

                int responseSize = await client.ReceiveAsync(buffer);
                Console.WriteLine($"Server receive message: {Encoding.UTF8.GetString(buffer)}");
            }
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
    
}