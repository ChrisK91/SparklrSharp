using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.Sparklr
{
    /// <summary>
    /// Represents a post on the Sparklr service
    /// </summary>
    public class Post
    {
        private static Dictionary<int, Post> postCache = new Dictionary<int,Post>();

        /// <summary>
        /// The Post-ID
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// The author of the post
        /// </summary>
        public User Author { get; private set; }

        /// <summary>
        /// The network this post was posted on
        /// </summary>
        public string Network { get; private set; }

        /// <summary>
        /// The message type
        /// </summary>
        public int Type { get; private set; }

        /// <summary>
        /// Meta-Information. TODO: What is it? Maybe an image?
        /// </summary>
        public string Meta { get; set; }

        /// <summary>
        /// Original timestamp
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// Indicates if the post is visible.
        /// </summary>
        public bool IsPublic { get; private set; }

        /// <summary>
        /// Content of the post
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// The original id of a reposted post.
        /// </summary>
        [Obsolete("OriginalId will be replaced with an actual Post member")]
        public int OriginalId { get; private set; }

        /// <summary>
        /// The original author of a reposted post
        /// </summary>
        public User ViaUser { get; private set; }

        /// <summary>
        /// The number of comments
        /// </summary>
        public int CommentCount { get; private set; }

        /// <summary>
        /// Indicates when the post was last modified
        /// </summary>
        public long ModifiedTimestamp { get; private set; }

        internal static Post InstanciatePost(int id, User author, string network, int type, string meta, long timestamp, bool isPublic, string content, int originalId, User viaUser, int commentCount, long modifiedTimestamp)
        {
            if (!postCache.ContainsKey(id))
                postCache.Add(id, new Post(id, author, network, type, meta, timestamp, isPublic, content, originalId, viaUser, commentCount, modifiedTimestamp));
            
            return postCache[id];
        }

        private Post(int id, User author, string network, int type, string meta, long timestamp, bool isPublic, string content, int originalId, User viaUser, int commentCount, long modifiedTimestamp)
        {
            this.Id = id;
            this.Author = author;
            this.Network = network;
            this.Type = type;
            this.Meta = meta;
            this.Timestamp = timestamp;
            this.IsPublic = IsPublic;
            this.Content = content;
            this.OriginalId = originalId;
            this.ViaUser = viaUser;
            this.CommentCount = commentCount;
            this.ModifiedTimestamp = modifiedTimestamp;
        }

        /// <summary>
        /// Likes/Unlikes the given post. TODO: implement
        /// </summary>
        /// <returns></returns>
        public bool ToggleLike()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a comment on this post. TODO: implement
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool Comment(string comment)
        {
            throw new NotImplementedException();
        }
    }
}
