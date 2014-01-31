using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.JSONRepresentations
{
    public class Notification
    {
        public int id { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public int type { get; set; } //1: like/comment; 2: mention; 3: message
        public long time { get; set; }
        public string body { get; set; }
        public string action { get; set; }
    }
}
