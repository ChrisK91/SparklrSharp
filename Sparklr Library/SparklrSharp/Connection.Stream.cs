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

        internal async Task<Post[]> GetStreamSinceAsync(string name, long timestamp)
        {
            SparklrResponse<JSONRepresentations.Get.Post[]> response = await webClient.GetJSONResponseAsync<JSONRepresentations.Get.Post[]>("stream", name + "?since=" + timestamp);
            return await extractPostsAsync(response);
        }

        internal async Task<Post[]> GetStreamAsync(string name, long starttime)
        {
            SparklrResponse<JSONRepresentations.Get.Post[]> response = await webClient.GetJSONResponseAsync<JSONRepresentations.Get.Post[]>("stream", name + "?starttime=" + starttime);
            return await extractPostsAsync(response);
        }

        private async Task<Post[]> extractPostsAsync(SparklrResponse<JSONRepresentations.Get.Post[]> response)
        {
            List<Post> posts = new List<Post>();

            List<int> userInfoToRetreive = new List<int>();

            foreach(JSONRepresentations.Get.Post p in response.Response)
            {
                userInfoToRetreive.Add(p.from);

                if(p.via != null)
                    userInfoToRetreive.Add((int)p.via);
            }

            if(userInfoToRetreive.Count > 0)
                await this.IdentifyMultipleUsersAsync(userInfoToRetreive.ToArray());

            foreach (JSONRepresentations.Get.Post p in response.Response)
            {
                Post po = await Post.InstanciatePostAsync(p, this);

                if (p != null)
                    posts.Add(po);
            }

            return posts.ToArray();
        }
    }
}
