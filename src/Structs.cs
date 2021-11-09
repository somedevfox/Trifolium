using System.Collections.Generic;

namespace Trifolium.Structs {
    public struct Event {
        public string e;
        public Dictionary<string, string> d;
    }
    public struct Status {
        public string e;
        public int sid;
    }
    public struct Error {
        public int id;
        public string message;
    }
}