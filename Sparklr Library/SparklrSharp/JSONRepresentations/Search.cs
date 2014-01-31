using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp.JSONRepresentations
{
    class Search
    {
        public class User
        {
            public string username { get; set; }
            public int id { get; set; }
        }

        public class Post
        {
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
        }

        public class RootObject
        {
            public List<User> users { get; set; }
            public List<Post> posts { get; set; }
        }
    }
}
