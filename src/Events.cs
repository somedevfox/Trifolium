using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;
using Trifolium;

namespace Trifolium {
	class Events {
        public static void test(Socket socket, Dictionary<string, string> dic) {
            Logger.Log("Client // Test Event", "This connection was made by client to test server's availability.");
        }
        public static void init(Socket socket, Dictionary<string, string> dic) {
            if(dic["libver"] != Version.libVersion) {
                Logger.LogError("Client // Init Event", "Someone's Trifolium version doesn't match with server!");
                socket.Send(Encoding.UTF8.GetBytes("{\"event\": \"init\", \"d\": { \"error\": \"invalid_lib_ver\" } }"));
                return;
            }
        }
        public static void login(Socket socket, Dictionary<string, string> dic) {
            Logger.Log("Client // Login Event", "ballsack encryption");
        }
    }
}