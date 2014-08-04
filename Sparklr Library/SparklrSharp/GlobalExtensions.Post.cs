using SparklrSharp.Sparklr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    public static partial class GlobalExtensions
    {
        /// <summary>
        /// Retreives the post with the given ID from the sparklr service. Uses caching.
        /// </summary>
        /// <param name="conn">The connection on which to perform the request</param>
        /// <param name="id">The id of the post</param>
        /// <returns></returns>
        public static Task<Post> GetPostByIdAsync(this Connection conn, int id)
        {
            return Post.GetPostByIdAsync(id, conn);
        }

        /// <summary>
        /// Can be used to submit a post to the sparklr service
        /// </summary>
        /// <param name="conn">The connection on which to perform the request</param>
        /// <param name="message">The content of the post. Cannot exceed 500 characters.</param>
        /// <param name="network">The network to post to. Defaults to "following".</param>
        /// <returns>True if succesfull, otherwise false</returns>
        public static Task<bool> SubmitPostAsync(this Connection conn, string message, string network = null)
        {
            return Post.SubmitPostAsync(message, network, conn);
        }
    }
}
