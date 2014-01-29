using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.JSONRepresentations
{
    /// <summary>
    /// The JSON representation of a message. You most likely don't want to use it.
    /// </summary>
    public class Message
    {
        public string message { get; set; }
        public long time { get; set; }
        public int from { get; set; }
    }
}
