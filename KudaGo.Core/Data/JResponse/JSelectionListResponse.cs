using DailyEvents.Core.Data.JData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Core.Data.JResponse
{
    class JSelectionListResponse
    {
        public JSelectionListResponse()
        {
            Results = new JSelectionListResult[0];
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<JSelectionListResult> Results { get; set; }
    }

    class JSelectionListResult
    {
        public long Id { get; set; }
        public long Publication_Date { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Site_Url { get; set; }
        public IEnumerable<JImage> Images { get; set; }
    }
}
