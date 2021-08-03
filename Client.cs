using System;
using System.Net.Sockets;
using System.Text;
using witskyNet;

public class Client
{
    UdpClient client;
    public string Message { get; set; }
    public string RemoteAdress { get; set; }
    public int RemotePort { get; set; }
    public Client(string message, string remoteAddress, int remotePort)
    {
        Message = message;
        RemoteAdress = remoteAddress;
        RemotePort = remotePort;

        SendMessage(Message, RemoteAdress, RemotePort);
    }

    public void SendMessage(string message, string remoteAddress, int remotePort)
    {
        client = new UdpClient();
        try
        {
            while(true)
            {
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data, data.Length, remoteAddress, remotePort); // отправка
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            client.Close();
        }
    }
}