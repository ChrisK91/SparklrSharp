using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.JSONRepresentations.Get
{
    public class Notification
    {
        public int id { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public int type { get; set; }
        public long time { get; set; }
        public string body { get; set; }
        public string action { get; set; }
    }
}
