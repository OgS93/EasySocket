using System.Net.Sockets;

namespace EasySocket
{
    public sealed class EasyServerOptions
    {
        int _sendBufferSize = 8192;
        int _receiveBufferSize = 8192;
        int _backlog = (int)SocketOptionName.MaxConnections;
        bool _keepAlive = false;
        ulong _keepAliveTime = 180000;
        ulong _keepAliveInterval = 30000;

        internal EasyServerOptions() { }

        internal bool Locked { get; set; }

        public int SendBufferSize
        {
            get => _sendBufferSize;
            set => SetOption(ref _sendBufferSize, value);
        }

        public int ReceiveBufferSize
        {
            get => _receiveBufferSize;
            set => SetOption(ref _receiveBufferSize, value); 
        }

        public int Backlog
        {
            get => _backlog;
            set => SetOption(ref _backlog, value); 
        }

        public bool KeepAlive
        {
            get => _keepAlive;
            set => SetOption(ref _keepAlive, value); 
        }

        public ulong KeepAliveTime
        {
            get => _keepAliveTime;
            set => SetOption(ref _keepAliveTime, value);
        }

        public ulong KeepAliveInterval
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