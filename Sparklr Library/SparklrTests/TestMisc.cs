using System;
using SparklrSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace SparklrTests
{
    [TestClass]
    public class TestMisc
    {
        [TestMethod]
        public async Task TestAwake()
        {
            Connection conn = new Connection();

            bool result = await conn.GetAwakeAsync();

            Assert.IsTrue(result);
        }
    }
}
