using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    public partial class Connection
    {
        public string EmailIDOutput;
        public async Task<string>RequestInvite(string EmailID)
        {

            var response = await webClient.GetRawResponseAsync("requestinvite/" + EmailID);
            EmailIDOutput = response.Response.ToString();
            return "";
        }
    }
}
