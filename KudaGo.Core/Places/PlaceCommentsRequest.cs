using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Comments;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Places
{
    public class PlaceCommentsRequest : BaseRequest<ICommentsResponse>
    {
        public long PlaceId { get; set; }
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
            return "/places/";
        }

        protected override string Build()
        {
            if (PlaceId <= 0)
                throw new Exception("PlaceId must be set");

            _builder.Append(PlaceId + "/comments/?");

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
