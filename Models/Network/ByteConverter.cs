using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class ByteConverter
{
    public static byte[] ToByteArray(this IP2PBase clientInfo)
    {
        if(clientInfo == null)
            return null;


        BinaryFormatter bf = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }


        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream Stream = new MemoryStream();

        formatter.Serialize(Stream, clientInfo);
        return Stream.ToArray();
    }

    public static IP2PBase ToP2PBase(this byte[] bytes)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream stream = new MemoryStream();

        stream.Write(bytes, 0, bytes.Length);
        stream.Seek(0, SeekOrigin.Begin);

        IP2PBase clientInfo = (IP2PBase)formatter.Deserialize(stream);

        return clientInfo;
    }
}

