using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JData;
using KudaGo.Core.Data.JResponse;
using KudaGo.Core.Search;

namespace KudaGo.Core.Events
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
        bool IsDeleted { get; }
        long RepliesCount { get; }
        string Thread { get; }
        string ReplyTo { get; }
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
            var request = new ClientServiceRequest<JEventCommentsResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new EventCommentsResponse(result);
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
        public EventCommentsResponse(JEventCommentsResponse jResponse)
        {
            if (jResponse == null)
            {
                Results = new IEventCommentResult[0];
                return;
            }

            Count = jResponse.Count;
            Next = jResponse.Next;
            Previous = jResponse.Previous;
            Results = jResponse.Results.Select(r => new EventCommentResult(r));
        }

        public int Count { get; private set; }
        public string Next { get; private set; }
        public string Previous { get; private set; }
        public IEnumerable<IEventCommentResult> Results { get; private set; }
    }

    internal class EventCommentResult : IEventCommentResult
    {
        public EventCommentResult(JEventCommentResult jEventCommentResult)
        {
            if (jEventCommentResult == null)
            {
                User = new User(new JUser());
                return;
            }

            Id = jEventCommentResult.Id;
            DatePosted = DateTimeHelper.GetDateTimeFromUnixTime(jEventCommentResult.Date_Posted);
            Text = jEventCommentResult.Text;
            IsDeleted = jEventCommentResult.Is_Deleted;
            RepliesCount = jEventCommentResult.Replies_Count;
            Thread = jEventCommentResult.Thread;
            ReplyTo = jEventCommentResult.Reply_To;
            User = new User(jEventCommentResult.User);
        }

        public string Id { get; private set; }
        public DateTime? DatePosted { get; private set; }
        public string Text { get; private set; }
        public bool IsDeleted { get; private set; }
        public long RepliesCount { get; private set; }
        public string Thread { get; private set; }
        public string ReplyTo { get; private set; }
        public IUser User { get; private set; }
    }
}
