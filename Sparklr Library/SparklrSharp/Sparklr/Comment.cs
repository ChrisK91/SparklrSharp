using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp.Sparklr
{
    /// <summary>
    /// Represents the type of comment
    /// </summary>
    public enum CommentType
    {
        /// <summary>
        /// The Comment is a comment
        /// </summary>
        Comment,
        /// <summary>
        /// The Comment is a like
        /// </summary>
        Like
    }

    /// <summary>
    /// Represents a comment on the sparklr service
    /// </summary>
    public class Comment : IComparable<Comment>
    {
        /// <summary>
        /// The id of the comment
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// The post that this comment it related to
        /// </summary>
        public Post Parent { get; private set; }

        /// <summary>
        /// The author of the comment
        /// </summary>
        public User Author { get; private set; }

        /// <summary>
        /// The message of the comment. If it's a comment, this will contain "👆".
        /// Use .ToString() to get either the Comment or "{User} likes this" instead
        /// </summary>
        public String Message { get; private set; }

        /// <summary>
        /// Returns either the comment text or "{User} likes this"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if(this.Type == CommentType.Comment)
            {
                return this.Message;
            }
            else
            {
                return String.Format("{0} likes this", Author.Name);
            }
        }

        private const string LIKE_CHARACTER = "☝";

        /// <summary>
        /// Returns the type of this comment
        /// </summary>
        public CommentType Type
        {
            get
            {
                return Message == LIKE_CHARACTER ? CommentType.Like : CommentType.Comment;
            }
        }

        /// <summary>
        /// The timestamp of the comment
        /// </summary>
        public long Timestamp { get; private set; }

        private static Dictionary<int, Comment> commentCache = new Dictionary<int, Comment>();

        internal static Comment InstanciateComment(int id, Post parent, User author, string message, long timestamp)
        {
            if (!commentCache.ContainsKey(id))
                commentCache.Add(id, new Comment(
                        id,
                        parent,
                        author,
                        message,
                        timestamp
                    ));

            return commentCache[id];
        }

        private Comment(int id, Post parent, User author, string message, long timestamp)
        {
            this.Id = id;
            this.Parent = parent;
            this.Author = author;
            this.Message = message;
            this.Timestamp = timestamp;
        }

        /// <summary>
        /// Compares the items
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int CompareTo(Comment item)
        {
            return this.Timestamp.CompareTo(item.Timestamp);
        }
    }
}
