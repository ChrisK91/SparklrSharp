using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    public partial class Connection
    {
        /// <summary>
        /// Retreives all messages
        /// </summary>
        /// <returns></returns>
        internal async Task<Sparklr.Message[]> GetInbox()
        {
            var response = await webClient.GetJSONResponseAsync<JSONRepresentations.Message[]>("inbox");

            Sparklr.Message[] messages = new Sparklr.Message[response.Response.Length];

            for (int i = 0; i < response.Response.Length; i++)
            {
                messages[i] = new Sparklr.Message(
                        response.Response[i].message,
                        response.Response[i].time,
                        response.Response[i].from
                    );
            }

            return messages;
        }
    }
}
