using SparklrSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrTests
{
    internal static class Credentials
    {
        //Insert valid credentials here
        internal const string ValidUsername = "test";
        internal const string ValidPassword = "test";

        //Insert invalid credentials here
        internal const string InvalidPassword = "invalid";

        internal static async Task<Connection> CreateSession()
        {
            Connection conn = new Connection();
            bool result = await conn.SigninAsync(ValidUsername, ValidPassword);

            if (result)
                return conn;
            else
                throw new Exception("Failed to authenticate");
        }
    }
}
