using System.Net.Sockets;
using System.Collections.Generic;
using Trifolium;

namespace Trifolium {
    class Events {
        public static void test(Socket socket, Dictionary<string, string> dic) {
            Logger.Log("Test Event", "This connection was made by client to test server's availability.");
        }
        public static void init(Socket socket, Dictionary<string, string> dic) {
            Logger.Log("Init Event", "ballsack encryption");
        }
    }
}