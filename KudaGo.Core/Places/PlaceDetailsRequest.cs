using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Places
{
    public class PlaceDetailsRequest : BaseRequest<IPlaceDetailsResponse>
    {
        public long PlaceId { get; set; }
        public string Fields { get; set; }
        public string Expand { get; set; }

        public override async Task<IPlaceDetailsResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JPlaceDetailsResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new PlaceDetailsResponse(result);
        }

        protected override string Build()
        {
            if (PlaceId <= 0)
                throw new Exception("PlaceId must be set");

            _builder.Append(PlaceId + "/?");

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            if (Expand != null)
                _builder.Append("&expand=" + Expand);

            return base.Build();
        }

        protected override string GetRelativePath()
        {
            return "/places/";
        }
    }
}
