using System;
using System.Net;
using System.Net.Sockets;

namespace Trifolium {
    public class Logger {
        public static void Log(string prefix, string message) {
            Console.WriteLine("[{0}] {1}", prefix, message);
        }
        public static void LogSuccess(string prefix, string message) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[{0}] {1}", prefix, message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogError(string prefix, string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[{0}] {1}", prefix, message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    };
}