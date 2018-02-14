using System.Net.Sockets;

namespace EasySocket
{
    public sealed class EasyServerOptions
    {
        int _sendBufferSize = 8192;
        int _receiveBufferSize = 8192;
        int _backlog = (int)SocketOptionName.MaxConnections;
        bool _keepAlive = false;
        ulong _keepAliveTimeout = 180000;
        ulong _keepAliveInterval = 30000;

        internal EasyServerOptions() { }

        internal bool Locked { get; set; }

        int SendBufferSize
        {
            get => _sendBufferSize;
            set => SetOption(ref _sendBufferSize, value);
        }

        int ReceiveBufferSize
        {
            get => _receiveBufferSize;
            set => SetOption(ref _receiveBufferSize, value); 
        }

        int Backlog
        {
            get => _backlog;
            set => SetOption(ref _backlog, value); 
        }

        bool KeepAlive
        {
            get => _keepAlive;
            set => SetOption(ref _keepAlive, value); 
        }

        ulong KeepAliveTimeout
        {
            get => _keepAliveTimeout;
            set => SetOption(ref _keepAliveTimeout, value); 
        }

        ulong KeepAliveInterval
        {
            get => _keepAliveInterval;
            set => SetOption(ref _keepAliveInterval, value); 
        }

        private void SetOption <T>(ref T option, T value)
        {
            if (Locked)
                throw new System.InvalidOperationException("Options change locked");
            option = value;
        }
    }
}