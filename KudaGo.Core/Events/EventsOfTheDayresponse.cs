using System;
using System.Collections.Generic;
using System.Linq;
using DailyEvents.Core.Data;
using DailyEvents.Core.Data.JData;
using DailyEvents.Core.Data.JResponse;

namespace DailyEvents.Core.Events
{
    public interface IEventsOfTheDayResponse : IResponse
    {
        IEnumerable<IEventsOfTheDayResult> Results { get; } 
    }

    public interface IEventsOfTheDayResult
    {
        DateTime Date { get; }
        string Location { get; }
        IEvent Event { get; }
    }

    internal class EventsOfTheDayResponse : IEventsOfTheDayResponse
    {
        public EventsOfTheDayResponse(JEventsOfTheDayResponse jResponse)
        {
            if (jResponse == null)
            {
                Results = new IEventsOfTheDayResult[0];
                return;
            }

            Count = jResponse.Count;
            Next = jResponse.Next;
            Previous = jResponse.Previous;
            Results = jResponse.Results.Select(r => new EventsOfTheDayResult(r));
        }

        public int Count { get; private set; }
        public string Next { get; private set; }
        public string Previous { get; private set; }
        public IEnumerable<IEventsOfTheDayResult> Results { get; private set; }
    }

    internal class EventsOfTheDayResult : IEventsOfTheDayResult
    {
        public EventsOfTheDayResult(JEventsOfTheDayResult jResult)
        {
            if (jResult == null)
            {
                Event = new Event(new JEvent());
                return;
            }

            Date = GetDateTime(jResult.Date);
            Location = jResult.Location;
            Event = new Event(jResult.Event);
        }

        public DateTime Date { get; private set; }
        public string Location { get; private set; }
        public IEvent Event { get; private set; }

        private DateTime GetDateTime(string date)
        {
            if (string.IsNullOrEmpty(date))
                return DateTime.MinValue;

            DateTime datetime;
            return !DateTime.TryParse(date, out datetime) ? DateTime.MinValue : datetime;
        }
    }
}
