using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;


public enum ConnectionTypes { Unknown, LAN, WAN }

[Serializable]
public class ClientInfo : IP2PBase
{
    public string Name { get; set; }
    public long ID { get; set; }
    public IPEndPoint ExternalEndpoint { get; set; }
    public IPEndPoint InternalEndpoint { get; set; }
    public ConnectionTypes ConnectionType { get; set; }
    public bool UPnPEnabled { get; set; }
    public List<IPAddress> InternalAddresses = new List<IPAddress>();        

    [NonSerialized] //server use only
    public TcpClient Client;

    [NonSerialized] //server use only
    public bool Initialized;

    public bool Update(ClientInfo ci)
    {
        if (ID == ci.ID)
        {
            foreach (PropertyInfo p in ci.GetType().GetProperties())
                if (p.GetValue(ci) != null)
                    p.SetValue(this, p.GetValue(ci));

            if (ci.InternalAddresses.Count > 0)
            {
                InternalAddresses.Clear();
                InternalAddresses.AddRange(ci.InternalAddresses);
            }
        }

        return (ID == ci.ID);
    }

    public override string ToString()
    {
        if (ExternalEndpoint != null)
            return Name + " (" + ExternalEndpoint.Address + ")";
        else
            return Name + " (UDP Endpoint Unknown)";
    }

    public ClientInfo Simplified()
    {
        return new ClientInfo()
        {
            Name = this.Name,
            ID = this.ID,
            InternalEndpoint = this.InternalEndpoint,
            ExternalEndpoint = this.ExternalEndpoint                                
        };
    }
}    
