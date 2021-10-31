using System;
using System.Net;
using System.Net.Sockets;

using Trifolium;

namespace Trifolium {
    public class Server {
        public Server(int port) {
            Logger logger = new Logger();

            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName()); // * Get localhost entry.
            IPAddress ipAddress = ipHost.AddressList[0]; // * Select first entry.
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port); // * Get IP endpoint.

            // * Now, we can create socket.
            Socket server = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            logger.Log("Server", $"Socket initialized on host: {server.RemoteEndPoint.ToString()}:{port}");
        }
    };
}