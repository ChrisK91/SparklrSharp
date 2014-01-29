using SparklrSharp.Communications;
using SparklrSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SparklrTests")]

namespace SparklrSharp
{
    public partial class Connection
    {
        /// <summary>
        /// Checks if sparklr is running
        /// </summary>
        /// <returns>true if it is, otherwise false</returns>
        public async Task<bool> GetAwake()
        {
            SparklrResponse<string> response = await webClient.GetRawResponseAsync("areyouawake");

            return response.IsOkAndTrue();
        }
    }
}
