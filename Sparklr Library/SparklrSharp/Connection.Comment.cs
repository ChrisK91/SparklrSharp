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
        /// Retreives the comments for the given post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task<Comment[]> GetCommentsForPostAsync(int id)
        {
            SparklrResponse<JSONRepresentations.Get.Comment[]> response = await webClient.GetJSONResponseAsync<JSONRepresentations.Get.Comment[]>("comments", id);

            Comment[] comments = new Comment[response.Response.Length];

            int i = 0;
            foreach (JSONRepresentations.Get.Comment c in response.Response)
            {
                comments[i] = Comment.InstanciateComment(
                                            c.id,
                                            await Post.GetPostByIdAsync(c.postid, this),
                                            await User.InstanciateUserAsync(c.from, this),
                                            c.message,
                                            c.time
                                        );
                i++;
            }

            return comments;
        }
    }
}
