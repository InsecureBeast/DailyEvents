using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KudaGo.Client.Events.Data;

namespace KudaGo.Client.Events
{
    public interface IEventsOfTheDayResponse : IResponse
    {
        IEnumerable<IEventsOfTheDayResult> Results { get; } 
    }

    public interface IEventsOfTheDayResult
    {
        DateTime Date { get; }
        string Location { get; }
        IEvent EventDetails { get; }
    }

    internal class EventsOfTheDayResponse : IEventsOfTheDayResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }

        public IEnumerable<EventsOfTheDayResult> results { get; set; }
        public IEnumerable<IEventsOfTheDayResult> Results { get { return results; } }
    }

    internal class EventsOfTheDayResult : IEventsOfTheDayResult
    {
        public string date { get; set; }
        public DateTime Date
        {
            get
            {
                if (string.IsNullOrEmpty(date))
                    return DateTime.MinValue;

                DateTime datetime;
                return !DateTime.TryParse(date, out datetime) ? DateTime.MinValue : datetime;
            }
        }

        public string Location { get; set; }
        public Event Event { get; set; }
        public IEvent EventDetails { get { return Event; } }
    }
}
