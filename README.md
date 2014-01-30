#SparklrSharp
SparklrSharp is an asynchronous implementation of the Sparklr* web-API. It tries to create further abstraction. This means, that web-API changes will only need to be implemented in this library and not in your code. You just update the library and everything (should) work fine.

SparklrSharp is a portable class library with support for .NET 4.5, Windows Phone 7.5, 7.8 and Windows Phone 8 as well as Windows Store applications. On some packages you might need to add the `Microsoft.Bcl.Async`-nuget.

#ToDo
To enable UnitTest, you need to create a `Credentials.cs`-file with the folllowing contents in the SparklrTests-project

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
    
#Usage
There is a small example included in this solution. In most cases, you'll only need to add the following namespaces and you're ready to go:
    
    using SparklrSharp;
    using SparklrSharp.Sparklr;
    
Then you can create a new connection

    Connection conn = new Connection();
    
On this connection you can sign in and run calls

    bool signedIn = await conn.SigninAsync(username, password);
    
    if(signedIn)
    {
        //Refresh inbox
        await conn.RefreshInboxAsync();
    }
    
    
