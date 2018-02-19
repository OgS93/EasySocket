using System;
using System.Net;
using System.Net.Sockets;

namespace EasySocket
{
    public class EasyServer
    {
        EasyServerOptions _serverOptions;
        TcpListener _listener;

        private EasyServer(TcpListener listener)
        {
            _listener = listener;
            _serverOptions = new EasyServerOptions();
            _serverOptions.Locked = true;

            var socket = _listener.Server;
            socket.SendBufferSize    = _serverOptions.SendBufferSize;
            socket.ReceiveBufferSize = _serverOptions.ReceiveBufferSize;
            if (_serverOptions.KeepAlive)
            {
                socket.SetKeepAlive(_serverOptions.KeepAliveTime, _serverOptions.KeepAliveInterval);
            }
        }

        // Initializes a new instance of the EasyServer class that listens on both IPv4 and IPv6 on the given port.
        public EasyServer(int port) : this(TcpListener.Create(port)) { }

        // Initializes a new instance of the EasyServer class with the specified local end point.
        public EasyServer(IPEndPoint localEP) : this(new TcpListener(localEP)) { }

        // Initializes a new instance of the TcpListener class that listens to the specified IP address
        // and port.
        public EasyServer(IPAddress localaddr, int port) : this(new TcpListener(localaddr, port)) { }
    }
}
