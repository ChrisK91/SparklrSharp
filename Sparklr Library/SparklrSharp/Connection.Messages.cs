using SparklrSharp.Sparklr;
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
        /// Contains the current Inbox. Needs to be refreshed manually
        /// </summary>
        public ICollection<Message> Inbox { get; internal set; }

        /// <summary>
        /// Retreives all messages
        /// </summary>
        /// <returns></returns>
        internal async Task<Sparklr.Message[]> GetInboxAsync()
        {
            var response = await webClient.GetJSONResponseAsync<JSONRepresentations.Message[]>("inbox");

            Sparklr.Message[] messages = new Sparklr.Message[response.Response.Length];

            for (int i = 0; i < response.Response.Length; i++)
            {
                messages[i] = await Message.CreateMessageAsync(
                        response.Response[i].message,
                        response.Response[i].time,
                        response.Response[i].from,
                        this
                    );
            }

            return messages;
        }
    }
}
