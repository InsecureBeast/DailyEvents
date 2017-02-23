using System;
using System.Threading.Tasks;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Events
{
    public class EventsOfTheDayRequest : BaseRequest<IEventsOfTheDayResponse>
    {
        public string Fields { get; set; }
        public TextFormatEnum? TextFormat { get; set; }
        public DateTime? Date { get; set; }
        
        public override async Task<IEventsOfTheDayResponse> ExecuteAsync()
        {
            var request = new ClientServiceRequest<JEventsOfTheDayResponse>();
            var url = Build();
            var result = await request.ExecuteAsync(url);
            return new EventsOfTheDayResponse(result);
        }

        protected override string Build()
        {
            //next search request
            if (!string.IsNullOrEmpty(Next))
                return Next;

            if (Fields != null)
                _builder.Append("fields=" + Fields);

            _builder.Append("&expand=event");

            if (TextFormat != null)
                _builder.Append("&text_format=" + TextFormat.Value.ToString().ToLowerInvariant());

            if (Date != null)
                _builder.Append("&date=" + DateTimeHelper.ToUnixTimestamp(Date.Value));

            return base.Build();
        }

        protected override string GetRelativePath()
        {
            return "/events-of-the-day/?";
        }

        public class FieldsNames
        {
            public const string EVENT = "event";
            public const string DATE = "date";
            public const string LOCATION = "location";
        }
    }
}
