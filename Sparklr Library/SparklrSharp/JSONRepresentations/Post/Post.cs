using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.JSONRepresentations.Post
{
    /// <summary>
    /// Represents a Post that is sent to the Sparklr*-Service
    /// </summary>
    public class Post
    {
        /// <summary>
        /// The message, cannot exceed 500 characters
        /// </summary>
        public string body { get; set; }

        /// <summary>
        /// The network to post to
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string network { get; set; }

        /// <summary>
        /// Indicates if the post has a image attached
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? img { get; set; }
    }
}
