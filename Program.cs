using System;

using Trifolium;

namespace Trifolium
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine(Config.GetConfig().gameserver.port);
            // Create game server.
            Server server = new Server(5555);
        }
    }
}
