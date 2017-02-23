using System;
using System.Collections.Generic;
using System.Linq;
using DailyEvents.Core.Data;
using DailyEvents.Core.Data.JData;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Comments
{
    public interface ICommentsResponse : IResponse
    {
        IEnumerable<ICommentResult> Results { get; }
    }

    public interface ICommentResult
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

    internal class CommentsResponse : ICommentsResponse
    {
        public CommentsResponse(JCommentsResponse jResponse)
        {
            if (jResponse == null)
            {
                Results = new ICommentResult[0];
                return;
            }

            Count = jResponse.Count;
            Next = jResponse.Next;
            Previous = jResponse.Previous;
            Results = jResponse.Results.Select(r => new CommentResult(r));
        }

        public int Count { get; private set; }
        public string Next { get; private set; }
        public string Previous { get; private set; }
        public IEnumerable<ICommentResult> Results { get; private set; }
    }

    internal class CommentResult : ICommentResult
    {
        public CommentResult(JCommentResult jCommentResult)
        {
            if (jCommentResult == null)
            {
                User = new User(new JUser());
                return;
            }

            Id = jCommentResult.Id;
            DatePosted = DateTimeHelper.GetDateTimeFromUnixTime(jCommentResult.Date_Posted);
            Text = jCommentResult.Text;
            IsDeleted = jCommentResult.Is_Deleted;
            RepliesCount = jCommentResult.Replies_Count;
            Thread = jCommentResult.Thread;
            ReplyTo = jCommentResult.Reply_To;
            User = new User(jCommentResult.User);
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
