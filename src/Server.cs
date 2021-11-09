//::::::::::::::::::::::::::::::::::::::::::::::::::::
// Server.cs
//====================================================
// This script is game server for Trifolium.
// It ONLY supports SSL, because of security concerns.
// 
// By default it binds to 0.0.0.0:5556 (Secure Trifolium port).
//::::::::::::::::::::::::::::::::::::::::::::::::::::


using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using Trifolium;

namespace Trifolium {
    public class Server {
        X509Certificate serverCert = null;
        public Server(int port) {
            // [ANCHOR] | Initialize socket.
            var server = new TcpListener(IPAddress.Any, 5555);
            server.Start();
            Logger.LogSuccess("Server", $"Socket initialized on host: {server.LocalEndpoint}");

            while(true) {
                TcpClient clientSock = server.AcceptTcpClient();
                // [ANCHOR] Initiate TLS stream.
                Logger.LogSuccess("Server", "A client has connected, creating thread...");
                Thread child = new Thread(() => new Client(clientSock));
                child.Start();
            }
        }
    };
}