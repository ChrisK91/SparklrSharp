SparklrSharp
============

A small library to play around with the sparkl API

ToDo
====
create Credentials.cs with the following content, to enable unit tests

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
            internal const string ValidUsername = ;
            internal const string ValidPassword = ;
    
            //Insert invalid credentials here
            internal const string InvalidPassword = ;
    
            internal static async Task<Connection> CreateSession()
            {
                Connection conn = new Connection();
                bool result = await conn.Signin(ValidUsername, ValidPassword);
    
                if (result)
                    return conn;
                else
                    throw new Exception("Failed to authenticate");
            }
        }
    }
