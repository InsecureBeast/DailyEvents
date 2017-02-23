using System.Collections.Generic;
using DailyEvents.Core.Data.JData;

namespace DailyEvents.Core.Data.JResponse
{
    internal class JEventsOfTheDayResponse
    {
        public JEventsOfTheDayResponse()
        {
            Results = new JEventsOfTheDayResult[0];
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<JEventsOfTheDayResult> Results { get; set; }
    }

    internal class JEventsOfTheDayResult
    {
        public string Date { get; set; }
        public string Location { get; set; }
        public JEvent Event { get; set; }
    }
}
