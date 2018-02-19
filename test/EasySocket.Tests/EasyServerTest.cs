using System;
using System.Net;
using EasySocket;
using Xunit;

namespace EasySocket.Tests
{
    public class EasyServerTest
    {
        private IPEndPoint GetIpEP()
        {
            return new IPEndPoint(IPAddress.Any, 8001);
        }

        [Fact]
        public void ServerCreatesSuccessfully()
        {
            var ipEndPoint = GetIpEP();
            var port = ipEndPoint.Port;
            var ipAddr = ipEndPoint.Address;

            new EasyServer(port);
            new EasyServer(ipEndPoint);
            new EasyServer(ipAddr, port);
        }
    }
}
