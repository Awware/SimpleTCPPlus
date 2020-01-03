﻿using SimpleTCPPlus.Client;
using SimpleTCPPlus.Common;
using System;
using System.Threading;

namespace ReallyClient
{
    class Client
    {
        static void Main(string[] args)
        {
            SimpleTcpClient client = new SimpleTcpClient(System.Reflection.Assembly.GetExecutingAssembly());
            client.DataReceived += (s, pack) =>
            {
                Console.WriteLine($"PACKET:\n{pack.Packet.PacketType}");
                client.PacketHandler(pack);
            };
            client.ConnectedToServer += (x, clientx) =>
            {
                Console.WriteLine("Connected to the server!");
            };
            while (!client.TcpClient.Connected)
            {
                try
                {
                    client = client.Connect("127.0.0.1", 6124);
                }
                catch { Console.WriteLine("Reconnecting..."); }
            }
            client.WritePacket(new SimpleTCPPlus.Common.Packet("JSON", "INIT"));
            PacketWrapper wrap = client.WritePacketAndReceive(new Packet("dasda", "sasok"), "TEST_REPLY_PACKET");
            System.Console.WriteLine($"WRAP: {wrap.Packet.PacketType}");
            new Thread(() => { while (true) { } }).Start();
        }
    }
}
