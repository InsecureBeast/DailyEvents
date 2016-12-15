using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.News
{
    public class NewsDetailsRequest : BaseRequest<INewsDetailsResponse>
    {
        public long NewsId { get; set; }
        public string Fields { get; set; }
        public string Expand { get; set; }

        public override async Task<INewsDetailsResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JNewsDetailsResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new NewsDetailsResponse(result);
        }

        protected override string GetRelativePath()
        {
            return "/news/";
        }

        protected override string Build()
        {
            if (NewsId <= 0)
                throw new Exception("NewsId must be set");

            _builder.Append(NewsId + "/?");

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            if (Expand != null)
                _builder.Append("&expand=" + Expand);

            return base.Build();
        }
    }
}
