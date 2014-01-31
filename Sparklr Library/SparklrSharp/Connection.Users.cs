using SparklrSharp.Communications;
using SparklrSharp.Extensions;
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
        /// Retreives the user from the sparklr service. Throws a NoDataFoundException if the specified user is invalid.
        /// Use User.CreateUserAsync to use user caching.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        internal async Task<Sparklr.User> GetUserAsync(int id)
        {
            try
            {
                SparklrResponse<JSONRepresentations.Get.User> result = await webClient.GetJSONResponseAsync<JSONRepresentations.Get.User>("user", id);

                if (result.Code == System.Net.HttpStatusCode.OK)
                {
                    User u = User.InstanciateUser(
                        result.Response.user,
                        result.Response.name,
                        result.Response.handle,
                        result.Response.avatarid ?? -1,
                        result.Response.following,
                        result.Response.bio);

                    foreach (JSONRepresentations.Get.TimelinePost p in result.Response.timeline)
                    {
                        u.timeline.Add(
                                Post.InstanciatePost(
                                    p.id,
                                    await User.InstanciateUserAsync(p.from, this),
                                    p.network,
                                    p.type,
                                    p.meta,
                                    p.time,
                                    p.@public ?? false,
                                    p.message,
                                    p.origid ?? -1,
                                    p.via != null ? await User.InstanciateUserAsync((int)p.via, this) : null,
                                    p.commentcount ?? 0,
                                    p.modified ?? -1
                                )
                            );
                    }

                    return u;
                }
                else
                {
                    throw new Exceptions.NoDataFoundException();
                }
            }
            catch (Exceptions.InvalidResponseException ex)
            {
                if (ex.Response.IsOkAndFalse() || ex.Response.Code == System.Net.HttpStatusCode.NotFound)
                {
                    throw new Exceptions.NoDataFoundException();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
