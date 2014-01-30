using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp.Sparklr
{
    /// <summary>
    /// Represents a User on the Sparklr service
    /// </summary>
    public class User
    {
        public int UserId { get; private set; }
        public string Name { get; private set; }
        public string Handle { get; private set; }
        public long AvatarId { get; private set; }
        public bool Following { get; private set; }
        public string Bio { get; private set; }

        private static Dictionary<int, User> userCache = new Dictionary<int, User>();

        /// <summary>
        /// Handles the creation of instances. Only one instance of a user can exist at a time.
        /// </summary>
        /// <param name="userid">The userid of the specified user</param>
        /// <param name="conn">The connection on which to run the identification</param>
        /// <returns></returns>
        internal static async Task<User> CreateUserAsync(int userid, Connection conn)
        {
            if (userCache.ContainsKey(userid))
            {
                return userCache[userid];
            }
            else
            {
                User u = await conn.GetUserAsync(userid);
                return u;
            }
        }

        /// <summary>
        /// You most likely don't want to call this. Use CreateUser instead.
        /// </summary>
        /// <param name="userid">the id</param>
        /// <param name="name">the name</param>
        /// <param name="handle">the handle</param>
        /// <param name="avatarid">the avatarid</param>
        /// <param name="following">true if following</param>
        /// <param name="bio">the biography</param>
        internal User(int userid, string name, string handle, long avatarid, bool following, string bio)
        {
            this.UserId = userid;
            this.Name = name;
            this.Handle = handle;
            this.AvatarId = avatarid;
            this.Following = following;
            this.Bio = bio;
        }
    }
}
