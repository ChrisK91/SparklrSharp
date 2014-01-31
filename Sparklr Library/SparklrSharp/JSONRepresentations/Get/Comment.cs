using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.JSONRepresentations.Get
{
    public class Comment
    {
        public int id { get; set; }
        public int postid { get; set; }
        public int from { get; set; }
        public string message { get; set; }
        public int time { get; set; }
    }
}
