using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    public static partial class GlobalExtensions
    {
        /// <summary>
        /// Updates the Inbox via the given connection
        /// </summary>
        /// <param name="conn">The connection on which to update the Messages</param>
        /// <returns></returns>
        public static async Task RefreshInboxAsync(this Connection conn)
        {
            SparklrSharp.Sparklr.Message.Inbox = await conn.GetInbox();
        }
    }
}

namespace SparklrSharp.Sparklr
{
    /// <summary>
    /// Provides a representation of messages on the Sparklr service
    /// </summary>
    public class Message
    {
        public string Content { get; private set; }
        public long Timestamp { get; private set; }
        public User ConversationPartner { get; private set; }

        /// <summary>
        /// Contains the current Inbox. Needs to be refreshed manually
        /// </summary>
        public static ICollection<Message> Inbox { get; internal set; }

        internal Message(string content, long timestamp, int userid)
        {
            Content = content;
            Timestamp = timestamp;

            //TODO: support users
        }
    }
}
