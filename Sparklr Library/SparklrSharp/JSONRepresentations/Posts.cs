using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp.JSONRepresentations
{
    class Posts
    {
        public class Comment
        {
            public int id { get; set; }
            public int postid { get; set; }
            public int from { get; set; }
            public string message { get; set; }
            public int time { get; set; }
        }

        public class RootObject
        {
            //ALWAYS USE ROOTOBJECT WHEN DECODING 
            public int from { get; set; }
            public int id { get; set; }
            public object to { get; set; }
            public int type { get; set; }
            public object flags { get; set; }
            public string meta { get; set; }
            public int time { get; set; }
            public int @public { get; set; }
            public string message { get; set; }
            public object via { get; set; }
            public object origid { get; set; }
            public int commentcount { get; set; }
            public int modified { get; set; }
            public string network { get; set; }
            public List<Comment> comments { get; set; }
        }
    }
}
