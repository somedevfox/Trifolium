using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;

using Trifolium;

namespace Trifolium {
    class Info {
        static public string libver = "1";
        public static string gamever = "1.0";
        public static string[] extensions = new string[] {};

        public static string GetExtensions(string extensionstring) {
            if(extensionstring == "[]") 
                return "System.String[]";
            else return extensionstring;
        }
    }
}