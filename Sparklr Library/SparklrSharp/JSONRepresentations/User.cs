using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.JSONRepresentations
{
    public class User
    {
        //TODO: add timeline support
        public int user { get; set; }
        public string handle { get; set; }
        public long? avatarid { get; set; }
        public bool following { get; set; }
        public string name { get; set; }
        public string bio { get; set; }
    }
}
