using System;
using System.Net;
using System.Net.Sockets;

namespace Trifolium {
    public class Logger {
        public void Log(string prefix, string message) {
            Console.WriteLine("[{0}] {1}", prefix, message);
        }
        public void LogSuccess(string prefix, string message) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[{0}] {1}", prefix, message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void LogError(string prefix, string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[{0}] {1}", prefix, message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    };
}