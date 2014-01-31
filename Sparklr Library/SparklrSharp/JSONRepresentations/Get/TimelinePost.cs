using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.JSONRepresentations.Get
{
    public class TimelinePost
    {
        public int id { get; set; }
        public int from { get; set; }
        public string network { get; set; }
        public int type { get; set; }
        public string meta { get; set; }
        public long time { get; set; }
        public bool? @public { get; set; }
        public string message { get; set; }
        public int? origid { get; set; }
        public int? via { get; set; }
        public int? commentcount { get; set; }
        public long? modified { get; set; }


        public int? to { get; set; }
        public object flags { get; set; }
    }
}
