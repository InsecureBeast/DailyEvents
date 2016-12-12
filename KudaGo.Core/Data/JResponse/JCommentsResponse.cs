using System.Collections.Generic;
using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data.JResponse
{
    internal class JCommentsResponse
    {
        public JCommentsResponse()
        {
            Results = new JCommentResult[0];
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<JCommentResult> Results { get; set; }
    }

    internal class JCommentResult
    {
        public JCommentResult()
        {
            User = new JUser();
        }

        public string Id { get; set; }
        public long Date_Posted { get; set; }
        public string Text { get; set; }
        public bool Is_Deleted { get; set; }
        public long Replies_Count { get; set; }
        public string Thread { get; set; }
        public string Reply_To { get; set; }
        public JUser User { get; set; }
    }
}
