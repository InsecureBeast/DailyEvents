using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Comments;
using KudaGo.Core.Data;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Selections
{
    public class SelectionCommentsRequest : BaseRequest<ICommentsResponse>
    {
        public long SelectionId { get; set; }
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
            return "/lists/";
        }

        protected override string Build()
        {
            if (SelectionId <= 0)
                throw new Exception("SelectionId must be set");

            _builder.Append(SelectionId + "/comments/?");

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
