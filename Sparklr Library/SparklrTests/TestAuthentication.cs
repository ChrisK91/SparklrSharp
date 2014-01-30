using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparklrSharp;
using System.Threading.Tasks;

namespace SparklrTests
{
    [TestClass]
    public class TestAuthentication
    {
        [TestMethod]
        public async Task TestInvalidAuthentication()
        {
            Connection conn = new Connection();
            bool result = await conn.SigninAsync(Credentials.ValidUsername, Credentials.InvalidPassword);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task TestValidAuthentication()
        {
            Connection conn = new Connection();
            bool result = await conn.SigninAsync(Credentials.ValidUsername, Credentials.ValidPassword);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task TestLogout()
        {
            Connection conn = new Connection();
            bool result = await conn.SigninAsync(Credentials.ValidUsername, Credentials.ValidPassword);
            Assert.IsTrue(result);
            result = await conn.SignoffAsync();
            Assert.IsTrue(result);
        }
    }
}
