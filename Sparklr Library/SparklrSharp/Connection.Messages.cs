using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    public partial class Connection
    {
        public async Task<string> GetInbox()
        {
            var response = await webClient.GetJSONResponseAsync<JSONRepresentations.Message[]>("inbox");
            return "";
        }
    }
}
