using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp.Sparklr
{
    /// <summary>
    /// Provides a representation of messages on the Sparklr service.
    /// </summary>
    public class Message
    {
        public string Content { get; private set; }
        public long Timestamp { get; private set; }
        public User ConversationPartner { get; private set; }

        private Message(string content, long timestamp, User conversationPartner)
        {
            Content = content;
            Timestamp = timestamp;
            ConversationPartner = conversationPartner;
        }

        /// <summary>
        /// Creates a message and fills the user details
        /// </summary>
        /// <param name="content"></param>
        /// <param name="timestamp"></param>
        /// <param name="userid"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        internal static async Task<Message> CreateMessageAsync(string content, long timestamp, int userid, Connection conn)
        {
            User conversationPartner = await User.CreateUserAsync(userid, conn);
            return new Message(content, timestamp, conversationPartner);
        }
    }
}
