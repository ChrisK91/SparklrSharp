﻿using SparklrSharp.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp.Sparklr
{
    //TODO: Create base class for refreshable message lists (stream, mentions, etc.)

    /// <summary>
    /// Represents a stream of messages. The stream can be either a network or all posts by a specified user.
    /// </summary>
    public class Stream
    {
        private static Dictionary<string, Stream> streamCache = new Dictionary<string, Stream>();

        private SortedList<Post> posts = new SortedList<Post>();

        // older messages have smaller timestamps
        private long oldestTimestamp = long.MaxValue;

        // newer messages have greater timestamps
        private long newestTimestamp = long.MinValue;

        /// <summary>
        /// The name of the stream
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The current posts in this stream
        /// </summary>
        public ReadOnlyCollection<Post> Posts
        {
            get
            {
                return new ReadOnlyCollection<Post>(posts);
            }
        }

        /// <summary>
        /// Returns an instance of the given stream. Streams are cached
        /// </summary>
        /// <param name="name">The name of the stream</param>
        /// <param name="conn">The connection on which to retreive the stream</param>
        /// <returns></returns>
        public static async Task<Stream> InstanciateStreamAsync(string name, Connection conn)
        {
            if (!streamCache.ContainsKey(name))
            {
                Stream s = new Stream(name);

                await s.loadInitialPosts(conn);

                streamCache.Add(name, s);
            }

            return streamCache[name];
        }

        /// <summary>
        /// Loads the initial 30 posts
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        internal async Task loadInitialPosts(Connection conn)
        {
            Post[] initialPosts = await conn.GetStreamAsync(this.Name);

            appendPosts(initialPosts);
        }

        /// <summary>
        /// Adds the posts to the internal list
        /// </summary>
        /// <param name="posts"></param>
        private void appendPosts(Post[] posts)
        {
            foreach (Post p in posts)
            {
                this.posts.Add(p);

                if (p.ModifiedTimestamp < oldestTimestamp)
                    oldestTimestamp = p.Timestamp;

                if (p.ModifiedTimestamp > newestTimestamp)
                    newestTimestamp = p.Timestamp;
            }
        }

        /// <summary>
        /// Returns a stream of posts for the given user. Streams are cached.
        /// </summary>
        /// <param name="u">The user for which to retreive the posts</param>
        /// <param name="conn">The conenction on which to run the query</param>
        /// <returns></returns>
        public static Task<Stream> InstanciateStreamAsync(User u, Connection conn)
        {
            return InstanciateStreamAsync(u.UserId.ToString(), conn);
        }

        /// <summary>
        /// Attempts to load more posts.
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public async Task<bool> LoadOlderPosts(Connection conn)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debug.WriteLine("Loading older posts starting from {0} for {1}", oldestTimestamp, Name);
#endif
            Post[] morePosts = await conn.GetStreamAsync(Name, oldestTimestamp);

            if(morePosts.Length > 0)
            {
                appendPosts(morePosts);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Retreives newer posts for the network and inserts them into Posts
        /// </summary>
        /// <param name="conn">The connection on which to run the query</param>
        /// <returns>True if new posts were available, otherwise false</returns>
        public async Task<bool> LoadNewerPosts(Connection conn)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debug.WriteLine("Loading newer posts starting from {0} for {1}", oldestTimestamp, Name);
#endif
            Post[] morePosts = await conn.GetStreamSinceAsync(Name, newestTimestamp + 1);

            if(morePosts.Length > 0)
            {
                appendPosts(morePosts);
                return true;
            }
            return false;
        }

        private Stream(string name)
        {
            this.Name = name;
        }

        //TODO: Support for refreshing
    }
}
