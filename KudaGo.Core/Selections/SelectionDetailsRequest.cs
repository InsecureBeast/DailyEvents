using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Selections
{
    public class SelectionDetailsRequest : BaseRequest<ISelectionDetailsResponse>
    {
        public long SelectionId { get; set; }
        public string Fields { get; set; }
        public string Expand { get; set; }

        public override async Task<ISelectionDetailsResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JSelectionDetailsResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new SelectionDetailsResponse(result);
        }

        protected override string GetRelativePath()
        {
            return "/lists/";
        }

        protected override string Build()
        {
            if (SelectionId <= 0)
                throw new Exception("SelectionId must be set");

            _builder.Append(SelectionId + "/?");

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            if (Expand != null)
                _builder.Append("&expand=" + Expand);

            return base.Build();
        }
    }
}
