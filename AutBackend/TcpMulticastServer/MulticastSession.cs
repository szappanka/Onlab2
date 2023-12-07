﻿using NetCoreServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpMulticastServer
{
    public class MulticastSession : TcpSession
    {
        public MulticastSession(TcpServer server) : base(server) { }

        protected override void OnConnected()
        {
            Console.WriteLine($"Session with Id {Id} connected!");
        }

        protected override void OnDisconnected()
        {
            Console.WriteLine($"Session with Id {Id} disconnected!");
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            Console.WriteLine("Incoming: " + message);

            // Multicast message to all connected sessions
            Server.Multicast(message);

            // If the buffer starts with '!' the disconnect the current session
            if (message == "!")
                Disconnect();
        }

        protected override void OnError(SocketError error)
        {
            Console.WriteLine($"Session caught an error with code {error}");
        }
    }
}
