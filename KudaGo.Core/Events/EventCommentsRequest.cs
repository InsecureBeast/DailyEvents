using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DailyEvents.Core.Comments;
using DailyEvents.Core.Data;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Events
{
    public class EventCommentsRequest : BaseRequest<ICommentsResponse>
    {
        public long EventId { get; set; }
        public string Fields { get; set; }
        public string Expand { get; set; }
        public CommentOrderBy? OrderBy { get; set; }
        public string Ids { get; set; }

        public override async Task<ICommentsResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JCommentsResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new CommentsResponse(result);
        }

        protected override string GetRelativePath()
        {
            return "/events/";
        }

        protected override string Build()
        {
            if (EventId <= 0)
                throw new Exception("EventId must be set");

            _builder.Append(EventId + "/comments/?");

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            if (Expand != null)
                _builder.Append("&expand=" + Expand);

            if (Ids != null)
                _builder.Append("&ids=" + Ids);

            if (OrderBy != null)
                _builder.Append("&order_by=" + OrderBy.Value.ToString().ToLowerInvariant());

            return base.Build();
        }
    }
}
