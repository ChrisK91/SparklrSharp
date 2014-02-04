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
        internal async Task<Post[]> GetStreamAsync(string name)
        {
            SparklrResponse<JSONRepresentations.Get.Post[]> response = await webClient.GetJSONResponseAsync<JSONRepresentations.Get.Post[]>("stream", name);
            return await extractPostsAsync(response);
        }

        private async Task<Post[]> extractPostsAsync(SparklrResponse<JSONRepresentations.Get.Post[]> response)
        {
            Post[] posts = new Post[response.Response.Length];

            int i = 0;
            foreach (JSONRepresentations.Get.Post p in response.Response)
            {
                posts[i] = await Post.InstanciatePostAsync(p, this);
                i++;
            }

            return posts;
        }
    }
}
