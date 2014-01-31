using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparklrSharp;
using SparklrSharp.Sparklr;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;

namespace SparklrTests
{
    [TestClass]
    public class TestConversations
    {
        /// <summary>
        /// Contains a user that has send a specified number of messages to the user used for unit-testing
        /// </summary>
        private const int TestMessagesFromId = 1864;

        /// <summary>
        /// The number of messages in the conversation.
        /// </summary>
        private const int TestMessageCount = 2;

        [TestMethod]
        [ExpectedException(typeof(SparklrSharp.Exceptions.NotAuthorizedException))]
        public async Task TestConversationsNotAuthorized()
        {
            Connection conn = await Credentials.CreateSession();
            await conn.RefreshInboxAsync();
            await conn.SignoffAsync();

            Conversation convo = conn.GetConversationWith(conn.Inbox[0].ConversationPartner);
            await convo.LoadMore();
        }

        [TestMethod]
        public async Task TestConversationsLoadMessagesWithCompleteUser()
        {
            Connection conn = await Credentials.CreateSession();
            await conn.RefreshInboxAsync();

            User u = (from Message m in conn.Inbox where m.ConversationPartner.UserId == TestMessagesFromId select m.ConversationPartner).First();

            Conversation convo = conn.GetConversationWith(u);

            Assert.IsTrue(convo.NeedsRefresh);
            await convo.LoadMore();

            Assert.AreEqual(convo.Messages.Count, TestMessageCount);

            Assert.IsTrue(convo.NeedsRefresh);
            await convo.LoadMore();

            Assert.IsFalse(convo.NeedsRefresh);

            Assert.AreSame(convo.ConversationPartner, u);
        }

        [TestMethod]
        public async Task TestConversationsLoadMessagesWithUserid()
        {
            Connection conn = await Credentials.CreateSession();
            await conn.RefreshInboxAsync();

            User u = (from Message m in conn.Inbox where m.ConversationPartner.UserId == TestMessagesFromId select m.ConversationPartner).First();

            Conversation convo = await conn.GetConversationWithUseridAsync(TestMessagesFromId);

            Assert.IsTrue(convo.NeedsRefresh);
            await convo.LoadMore();

            Assert.AreEqual(convo.Messages.Count, TestMessageCount);

            Assert.IsTrue(convo.NeedsRefresh);
            await convo.LoadMore();

            Assert.IsFalse(convo.NeedsRefresh);

            Assert.AreSame(convo.ConversationPartner, u);
        }

        [TestMethod]
        [ExpectedException(typeof(SparklrSharp.Exceptions.NoDataFoundException))]
        public async Task TestConversationsLoadMessagesWithInvalidUserid()
        {
            Connection conn = await Credentials.CreateSession();
            await conn.RefreshInboxAsync();

            User u = (from Message m in conn.Inbox where m.ConversationPartner.UserId == TestMessagesFromId select m.ConversationPartner).First();

            Conversation convo = await conn.GetConversationWithUseridAsync(int.MaxValue);
        }

        [TestMethod]
        public async Task TestIEnumerableConversation()
        {
            Connection conn = await Credentials.CreateSession();
            await conn.RefreshInboxAsync();

            IEnumerable<Message> messages = conn.ConversationWith((from Message m in conn.Inbox where m.ConversationPartner.UserId == TestMessagesFromId select m.ConversationPartner).First());

            int number = 0;

            foreach (Message m in messages)
                number++;

            Assert.AreEqual(number, TestMessageCount);
        }
    }
}
