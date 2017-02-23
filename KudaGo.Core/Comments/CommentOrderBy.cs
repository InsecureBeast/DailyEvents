using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Core.Data
{
    public enum CommentOrderBy
    {
        id,
        date_posted,
        text,
        user,
        is_deleted,
        replies_count,
        thread,
        reply_to
    }

    public class CommentFields
    {
        public const string ID = "id";
        public const string DATE_POSTED = "date_posted";
        public const string TEXT = "text";
        public const string USER = "user";
        public const string IS_DELETED = "is_deleted";
        public const string REPLIES_COUNT = "replies_count";
        public const string THREAD = "thread";
        public const string REPLY_TO = "reply_to";
    }
}
