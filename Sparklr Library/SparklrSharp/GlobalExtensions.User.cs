using SparklrSharp.Sparklr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    public partial class GlobalExtensions
    {
        /// <summary>
        /// Retreives the data for the given user from Sparklr
        /// </summary>
        /// <param name="conn">The connection on wich to run the query</param>
        /// <param name="userid">The useris</param>
        /// <returns></returns>
        public static Task<User> GetUserAsync(this Connection conn, int userid)
        {
            return User.InstanciateUserAsync(userid, conn);
        }
    }
}
