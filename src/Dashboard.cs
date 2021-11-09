using System;
using Trifolium;
using System.Net;
using System.Runtime;
using System.Net.Http;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections.Generic;

namespace Trifolium {
    class Dashboard {
        public Dashboard(int port) {
            // [ANCHOR] Initialize dashboard.
            // * Check if HttpListener is supported (why the fuck would you run net 6.0 software on Windows XP?)
            if(!HttpListener.IsSupported) {
                Logger.LogError("Dashboard", "Windows XP Service Pack 2 or Server 2003 is required for HttpListener.");
                return;
            }

            
        }
    }
}