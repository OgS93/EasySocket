using System;
using System.Net;
using System.Net.Sockets;

namespace EasySocket
{
    public class EasyServer
    {
        EasyServerOptions _serverOptions;
        TcpListener _listener;

        private EasyServer()
        {
            _serverOptions = new EasyServerOptions();
        }

        // Initializes a new instance of the EasyServer class that listens on both IPv4 and IPv6 on the given port.
        public EasyServer(int port) : this()
        {
            _listener = TcpListener.Create(port);
        }

        // Initializes a new instance of the EasyServer class with the specified local end point.
        public EasyServer(IPEndPoint localEP) : this()
        {
            _listener = new TcpListener(localEP);
        }

        // Initializes a new instance of the TcpListener class that listens to the specified IP address
        // and port.
        public EasyServer(IPAddress localaddr, int port) : this()
        {
            _listener = new TcpListener(localaddr, port);
        }
    }
}
