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
        /// retreives the post by the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task<Post> GetPostByIdAsync(int id)
        {
            SparklrResponse<JSONRepresentations.Get.Post> response = await webClient.GetJSONResponseAsync<JSONRepresentations.Get.Post>("post", id);

            Post p = Post.InstanciatePost(
                        response.Response.id,
                        await User.InstanciateUserAsync(response.Response.from, this),
                        response.Response.network,
                        response.Response.type,
                        response.Response.meta,
                        response.Response.time,
                        response.Response.@public != null ? response.Response.@public == 1 : false,
                        response.Response.message,
                        response.Response.origid != null ? (await Post.GetPostByIdAsync((int)response.Response.origid, this)) : null,
                        response.Response.via != null ? await User.InstanciateUserAsync((int)response.Response.via, this) : null,
                        response.Response.commentcount ?? 0,
                        response.Response.modified ?? -1
                    );

            return p;
        }
    }
}
