using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Events.Data;

namespace KudaGo.Client.Events
{
    public interface IEventDetailsResponse : IEventListResult
    {
        bool Disable_Comments { get; }
    }

    public class EventDetailsRequest : BaseRequest<IEventDetailsResponse>
    {
        public string EventId { get; set; }
        public string Fields { get; set; }
        public string Expand { get; set; }
        
        public override async Task<IEventDetailsResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<EventDetailsResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return result;
        }

        protected override string GetRelativePath()
        {
            return "/events/";
        }

        protected override string Build()
        {
            if (string.IsNullOrEmpty(EventId))
                throw new Exception("EventId must be set");

            if (EventId != null)
                _builder.Append(EventId + "/?");

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            if (Expand != null)
                _builder.Append("&expand=" + Expand);

            return base.Build();
        }
    }

    internal class EventDetailsResponse : EventListResult, IEventDetailsResponse
    {
        public bool Disable_Comments { get; set; }
    }
}
