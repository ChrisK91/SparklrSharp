using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using SparklrSharp;

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
            string result = await conn.GetInbox();
        }
    }
}
