using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using witskyNet;

public class Server
{
    public UdpClient server;

    public int LocalPort { get; set; }
    IPEndPoint remoteIp = null;

    public Server(int localPort)
    {
        LocalPort = localPort;
        ReceiveMessage( LocalPort);
    }

    public void ReceiveMessage(int localPort)
    {
        UdpClient server = new UdpClient(localPort); // UdpClient для получения данных
        try
        {
            while(true)
            {
                byte[] data = server.Receive(ref remoteIp); // получаем данные
                string message = Encoding.Unicode.GetString(data);
                Console.WriteLine("Собеседник: {0}", message);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            server.Close();
        }
    }
}