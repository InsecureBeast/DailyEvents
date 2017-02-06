using System;
using System.Threading.Tasks;
using KudaGo.Core.Data.JResponse;

namespace KudaGo.Core.Events
{
    public interface IEventDetailsResponse : IEventListResult
    {
        bool DisableComments { get; }
    }

    public class EventDetailsRequest : BaseRequest<IEventDetailsResponse>
    {
        public long EventId { get; set; }
        public string Fields { get; set; }
        public string Expand { get; set; }

        public override async Task<IEventDetailsResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JEventDetailsResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new EventDetailsResponse(result);
        }

        protected override string GetRelativePath()
        {
            return "/events/";
        }

        protected override string Build()
        {
            if (EventId <= 0)
                throw new Exception("EventId must be set");

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
        public EventDetailsResponse(JEventDetailsResponse jResult) : base(jResult)
        {
            if (jResult == null)
                return;

            DisableComments = jResult.Disable_Comments;
        }

        public bool DisableComments { get; private set; }
    }
}
