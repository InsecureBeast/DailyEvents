using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Comments;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Movies
{
    public class MovieCommentsRequest : BaseRequest<ICommentsResponse>
    {
        public long MovieId { get; set; }
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
            return "/movies/";
        }

        protected override string Build()
        {
            if (MovieId <= 0)
                throw new Exception("MovieId must be set");

            _builder.Append(MovieId + "/comments/?");

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
