using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Events.Data;

namespace KudaGo.Client.Events
{
    public interface IEventCommentsResponse : IResponse
    {
        IEnumerable<IEventCommentResult> Results { get; }
    }

    public interface IEventCommentResult
    {
        string Id { get; }
        DateTime? DatePosted { get; }
        string Text { get; }
        IUser User { get; }
        bool Is_Deleted { get; }
        long Replies_Count { get; }
        string Thread { get; }
        string Reply_To { get; }
    }

    public class EventCommentsRequest : BaseRequest<IEventCommentsResponse>
    {
        public string EventId { get; set; }
        public string Fields { get; set; }
        public string Expand { get; set; }
        public OrderByEnum? OrederBy { get; set; }
        public string Ids { get; set; }

        public override async Task<IEventCommentsResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<EventCommentsResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return result;
        }

        protected override string GetRelativePath()
        {
            return "/events/";
        }

        protected override string Build()
        {
            if (string.IsNullOrEmpty(EventId))
                throw new Exception("EventId must be set");

            if (EventId != null)
                _builder.Append(EventId + "/comments/?");

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            if (Expand != null)
                _builder.Append("&expand=" + Expand);

            if (Ids != null)
                _builder.Append("&ids=" + Ids);

            return base.Build();
        }

        public enum OrderByEnum
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

    internal class EventCommentsResponse : IEventCommentsResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }

        public IEnumerable<EventCommentResult> results { get; set; }
        public IEnumerable<IEventCommentResult> Results
        {
            get { return results; }
        }
    }

    internal class EventCommentResult : IEventCommentResult
    {
        public EventCommentResult()
        {
            user = new User();
        }

        public string Id { get; set; }

        public long date_posted { get; set; }
        public DateTime? DatePosted
        {
            get 
            {
                return DateTimeHelper.GetDateTimeFromUnixTime(date_posted);
            }
        }

        public string Text { get; set; }
        public bool Is_Deleted { get; set; }
        public long Replies_Count { get; set; }
        public string Thread { get; set; }
        public string Reply_To { get; set; }

        public User user { get; set; }
        public IUser User { get { return user; } }
    }
}
