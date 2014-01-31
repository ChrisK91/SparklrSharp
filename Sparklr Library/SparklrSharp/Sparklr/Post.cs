using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SparklrSharp.Sparklr
{
    public class Post
    {
        private static Dictionary<int, Post> postCache = new Dictionary<int,Post>();

        public int Id { get; private set; }
        public User Author { get; private set; }
        public string Network { get; private set; }
        public int Type { get; private set; }
        public string Meta { get; set; }
        public long Timestamp { get; set; }
        public bool IsPublic { get; private set; }
        public string Content { get; private set; }

        [Obsolete("OriginalId will be replaced with an actual Post member")]

        public int OriginalId { get; private set; }
        public User ViaUser { get; private set; }
        public int CommentCount { get; private set; }
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

        public bool ToggleLike()
        {
            throw new NotImplementedException();
        }

        public bool Comment(string comment)
        {
            throw new NotImplementedException();
        }
    }
}
