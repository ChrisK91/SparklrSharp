using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparklrSharp.Sparklr
{
    public enum NotificationType
    {
        CommentOrLike = 1,
        Mention = 2,
        Message = 3
    }

    /// <summary>
    /// Represents a notification on the sparklr service
    /// </summary>
    public class Notification
    {
        public int Id { get; private set; }
        public User From { get; private set; }
        public User To { get; private set; }
        public NotificationType Type { get; private set; }
        public long TimeStamp { get; private set; }
        public string Body { get; private set; }
        public string Action { get; private set; }

        public string NotificationText
        {
            get
            {
                switch (Type)
                {
                    case NotificationType.CommentOrLike:
                        if (Body == "☝")
                        {
                            return String.Format("{0} likes your post.", From.Name);
                        }
                        else
                        {
                            return String.Format("{0} commented {1}.", From.Name, Body);
                        }
                    case NotificationType.Mention:
                        return String.Format("{0} mentioned you.", From.Name);
                    case NotificationType.Message:
                        return String.Format("{0} messaged you: {1}", From.Name, Body);
                    default:
                        throw new NotImplementedException("The given type is not supported");
                }
            }
        }

        internal Notification(int id, User from, User to, NotificationType type, long timestamp, string body, string action)
        {
            this.Id = id;
            this.From = from;
            this.To = to;
            this.Type = type;
            this.TimeStamp = timestamp;
            this.Body = body;
            this.Action = action;
        }

        public async Task<bool> Dismiss()
        {
            throw new NotImplementedException();
        }
    }
}
