using SparklrSharp.Sparklr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    public static partial class GlobalExtensions
    {
        public static async Task<Notification[]> GetNotificationsAsync(this Connection conn)
        {
            return await conn.GetNotificationsAsync();
        }
    }
}
