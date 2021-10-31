using System;

using Trifolium;

namespace Trifolium
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            Server server = new Server(5555);
        }
    }
}
