using System.Collections.Generic;

namespace DailyEvents.Core.Data.JResponse
{
    class JEventListResponse
    {
        public JEventListResponse()
        {
            Results = new JEventListResult[0];
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<JEventListResult> Results { get; set; }
    }
}
