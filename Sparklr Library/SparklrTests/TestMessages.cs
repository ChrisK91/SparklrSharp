﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SparklrSharp;
using SparklrSharp.Sparklr;

namespace SparklrTests
{
    [TestClass]
    public class TestMessages
    {
        [TestMethod]
        [ExpectedException(typeof(SparklrSharp.Exceptions.NotAuthorizedException))]
        public async Task TestNotAuthorized()
        {
            Connection conn = new Connection();
            var response = await conn.GetInbox();
        }

        [TestMethod]
        public async Task TestInbox()
        {
            Connection conn = await Credentials.CreateSession();
            var result = await conn.GetInbox();

            Assert.IsTrue(result.Length >= 1);
        }

        [TestMethod]
        public async Task TestInboxRefresh()
        {
            Connection conn = await Credentials.CreateSession();

            Assert.IsNull(SparklrSharp.Sparklr.Message.Inbox);
            await conn.RefreshInboxAsync();
            Assert.IsTrue(SparklrSharp.Sparklr.Message.Inbox.Count >= 1);
        }
    }
}
