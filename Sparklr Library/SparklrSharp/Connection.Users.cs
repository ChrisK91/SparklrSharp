using SparklrSharp.Communications;
using SparklrSharp.Extensions;
using SparklrSharp.Sparklr;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp
{
    public partial class Connection
    {
        /// <summary>
        /// Retreives the user from the sparklr service. Throws a NoDataFoundException if the specified user is invalid.
        /// Retreived Users are cached.
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

                    foreach (JSONRepresentations.Get.Post p in result.Response.timeline)
                    {
                        u.rawTimeline.Add(p);
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

        internal async Task<JSONRepresentations.Get.User> GetRawUser(int id)
        {
            //TODO: Check for deleted users
            return (await webClient.GetJSONResponseAsync<JSONRepresentations.Get.User>("user", id)).Response;
        }

        /// <summary>
        /// Identifies multiple users. Only retreives id, name, avatar and handle
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        internal async Task<Dictionary<int, User>> IdentifyMultipleUsersAsync(int[] ids)
        {
            SparklrResponse<JSONRepresentations.Get.UserMinimal[]> result = await webClient.GetJSONResponseAsync<JSONRepresentations.Get.UserMinimal[]>("username", String.Join(",", ids));

            Dictionary<int, User> users = new Dictionary<int, User>();

            foreach(SparklrSharp.JSONRepresentations.Get.UserMinimal u in result.Response)
            {
                User user = User.InstanciateUser(u.id, u.username, u.displayname, u.avatarid);

                users.Add(u.id, user);
            }

            foreach(int missing in ids.Except(users.Keys))
            {
                User.AddDeletedUser(missing);
                users.Add(missing, User.DeletedUser);
            }

            return users;
        }
    }
}
