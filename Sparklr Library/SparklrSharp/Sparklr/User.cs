using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SparklrSharp.Sparklr;
using SparklrSharp.Extensions;

namespace SparklrSharp.Sparklr
{
    /// <summary>
    /// Represents a User on the Sparklr service
    /// </summary>
    public class User
    {
        /// <summary>
        /// Represents a user that has been deleted from the service
        /// </summary>
        public static readonly User DeletedUser = new User(-1, "deleted", "deleted", -1, false, "deleted");

        /// <summary>
        /// The ID
        /// </summary>
        public int UserId { get; private set; }

        /// <summary>
        /// The name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The handle (without @)
        /// </summary>
        public string Handle { get; private set; }

        /// <summary>
        /// The avatar-id. -1 if no avatar is selected.
        /// </summary>
        public long AvatarId { get; private set; }

        /// <summary>
        /// True if the logged in user is following this user.
        /// </summary>
        private bool following { get; set; }

        /// <summary>
        /// Checks if the authenticated user is following the given user, response is cached
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public async Task<bool> GetFollowingAsync(Connection conn)
        {
            if(!infoComplete)
            {
                await getFullUser(conn);
                infoComplete = true;
            }

            return following;
        }

        private async Task getFullUser(Connection conn)
        {
            JSONRepresentations.Get.User u = await conn.GetRawUser(this.UserId);

            following = u.following;
            rawTimeline = u.timeline;
            bio = u.bio;
        }

        /// <summary>
        /// The biography of the user.
        /// </summary>
        private string bio { get; set; }

        /// <summary>
        /// Retreives the biography of the user. Response is cached.
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public async Task<string> GetBioAsync(Connection conn)
        {
            if (!infoComplete)
            {
                await getFullUser(conn);
                infoComplete = true;
            }

            return bio;
        }

        private bool infoComplete = false;

        /// <summary>
        /// Retreives the Timeline for the user asynchronously. The timeline will be retreived from memory, if the posts already have been loaded.
        /// </summary>
        /// <param name="conn">The connection on which to run the query</param>
        /// <returns>A read only collection</returns>
        public async Task<ReadOnlyCollection<Post>> GetTimelineAsync(Connection conn)
        {
            if(!infoComplete)
            {
                await getFullUser(conn);
                infoComplete = false;
            }

            // TODO: Check if timeline has new posts
            if(timeline == null)
            {
                timeline = new List<Post>();

                foreach(JSONRepresentations.Get.Post p in rawTimeline)
                {
                    timeline.Add(await(Post.InstanciatePostAsync(p, conn)));
                }
            }
            return new ReadOnlyCollection<Post>(timeline);
        }

        internal List<Post> timeline { get; private set; }
        internal List<JSONRepresentations.Get.Post> rawTimeline { get; private set; }

        private static Dictionary<int, User> userCache = new Dictionary<int, User>();
        private static HashSet<int> deletedUsers = new HashSet<int>();

        /// <summary>
        /// Handles the creation of instances. Only one instance of a user can exist at a time.
        /// </summary>
        /// <param name="userid">The userid of the specified user</param>
        /// <param name="conn">The connection on which to run the identification</param>
        /// <returns></returns>
        public static async Task<User> InstanciateUserAsync(int userid, Connection conn)
        {
            if(deletedUsers.Contains(userid))
            {
                return User.DeletedUser;
            }
            else if (!userCache.ContainsKey(userid))
            {
                try
                {
                    User u = await conn.GetUserAsync(userid);
                    //This will always add the User to the cache, do not add user to cache here!
                }
                catch(Exceptions.NoDataFoundException)
                {
                    deletedUsers.Add(userid);
                    return User.DeletedUser;
                }
            }

            return userCache[userid];
        }

        internal static void AddDeletedUser(int id)
        {
            if (!deletedUsers.Contains(id))
                deletedUsers.Add(id);
        }

        internal static User InstanciateUser(int userid, string name, string handle, long avatarid, bool following, string bio)
        {
            if (!userCache.ContainsKey(userid))
            {
                User u = new User(userid, name, handle, avatarid, following, bio);
                userCache.Add(userid, u);
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debug.WriteLine("Added user #{0} to cache", u.UserId);
#endif
            }

            return userCache[userid];
        }

        internal static User InstanciateUser(int userid, string name, string handle, long avatarid)
        {
            if(!userCache.ContainsKey(userid))
            {
                User u = new User(userid, name, handle, avatarid);
                userCache.Add(userid, u);
#if DEBUG
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debug.WriteLine("Added user #{0} to cache", u.UserId);
#endif
            }

            return userCache[userid];
        }

        /// <summary>
        /// You most likely don't want to call this. Use InstanciateUser instead.
        /// </summary>
        /// <param name="userid">the id</param>
        /// <param name="name">the name</param>
        /// <param name="handle">the handle</param>
        /// <param name="avatarid">the avatarid</param>
        /// <param name="following">true if following</param>
        /// <param name="bio">the biography</param>
        private User(int userid, string name, string handle, long avatarid, bool following, string bio)
        {
            this.UserId = userid;
            this.Name = name;
            this.Handle = handle;
            this.AvatarId = avatarid;
            this.following = following;
            this.bio = bio;

            infoComplete = true;

            this.rawTimeline = new List<JSONRepresentations.Get.Post>();
        }

        private User(int userid, string name, string handle, long avatarid)
        {
            this.UserId = userid;
            this.Name = name;
            this.Handle = handle;
            this.AvatarId = avatarid;

            infoComplete = false;

            this.rawTimeline = new List<JSONRepresentations.Get.Post>();
        }
    }
}
