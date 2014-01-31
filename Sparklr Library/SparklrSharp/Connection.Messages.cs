using SparklrSharp.Communications;
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
        public IList<Message> Inbox { get; internal set; }

        /// <summary>
        /// Retreives all messages
        /// </summary>
        /// <returns></returns>
        internal async Task<Sparklr.Message[]> GetInboxAsync()
        {
            SparklrResponse<JSONRepresentations.Message[]> response = await webClient.GetJSONResponseAsync<JSONRepresentations.Message[]>("inbox");

            Sparklr.Message[] messages = await parseJSONMessages(response);

            return messages;
        }

        /// <summary>
        /// Retreives a conversation asynchronously
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="starttime"></param>
        /// <returns></returns>
        internal async Task<Message[]> GetConversationAsync(int userId, long? starttime = null)
        {
            SparklrResponse<JSONRepresentations.Message[]> response;

            if (starttime == null)
                response = await webClient.GetJSONResponseAsync<JSONRepresentations.Message[]>("chat", userId);
            else
                response = await webClient.GetJSONResponseAsync<JSONRepresentations.Message[]>("chat", userId + "?starttime=" + starttime);

            Sparklr.Message[] messages = await parseJSONMessages(response);

            return messages;
        }

        /// <summary>
        /// Extracts message information from a JSON object and turns them into a Sparklr.Message
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<Message[]> parseJSONMessages(SparklrResponse<JSONRepresentations.Message[]> response)
        {
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
