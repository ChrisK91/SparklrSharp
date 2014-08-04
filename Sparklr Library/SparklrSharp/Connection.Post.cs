using SparklrSharp.Communications;
using SparklrSharp.Sparklr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparklrSharp.Extensions;

namespace SparklrSharp
{
    public partial class Connection
    {
        /// <summary>
        /// retreives the post by the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task<Post> GetPostByIdAsync(int id)
        {
            SparklrResponse<JSONRepresentations.Get.Post> response = await webClient.GetJSONResponseAsync<JSONRepresentations.Get.Post>("post", id);

            Post p = await Post.InstanciatePostAsync(response.Response, this);

            return p;
        }

        /// <summary>
        /// Submits a post to the sparklr service
        /// </summary>
        /// <param name="message">The body of the message</param>
        /// <param name="network">The name of the network</param>
        /// <returns>True if succesful, otherwise false.</returns>
        internal async Task<bool> SendPostWithoutImageAsync(string message, string network = null)
        {
            SparklrResponse<string> response = await webClient.PostJsonAsyncRawResponse<JSONRepresentations.Post.Post>("post", new JSONRepresentations.Post.Post(){body = message, network = network});

            return response.IsOkAndTrue();
        }
    }
}
