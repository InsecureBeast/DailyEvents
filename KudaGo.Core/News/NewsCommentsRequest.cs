using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Comments;
using DailyEvents.Core.Data;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.News
{
    public class NewsCommentsRequest : BaseRequest<ICommentsResponse>
    {
        public long NewsId { get; set; }
        public string Fields { get; set; }
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
            return "/news/";
        }

        protected override string Build()
        {
            if (NewsId <= 0)
                throw new Exception("NewsId must be set");

            _builder.Append(NewsId + "/comments/?");

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            if (Ids != null)
                _builder.Append("&ids=" + Ids);

            if (OrderBy != null)
                _builder.Append("&order_by=" + OrderBy.Value.ToString().ToLowerInvariant());

            return base.Build();
        }
    }
}
