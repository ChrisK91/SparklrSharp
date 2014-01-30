using SparklrSharp.Communications;
using SparklrSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SparklrSharp
{
    public partial class Connection
    {
        private Communications.WebClient webClient;

        /// <summary>
        /// Creates a new instance of Connection
        /// </summary>
        public Connection()
        {
            webClient = new Communications.WebClient();
        }

        /// <summary>
        /// Checks if sparklr is running
        /// </summary>
        /// <returns>true if it is, otherwise false</returns>
        public async Task<bool> GetAwakeAsync()
        {
            SparklrResponse<string> response = await webClient.GetRawResponseAsync("areyouawake");

            return response.IsOkAndTrue();
        }
    }
}
