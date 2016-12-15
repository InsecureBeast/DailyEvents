using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data.JResponse
{
    class JNewsListResponse
    {
        public JNewsListResponse()
        {
            Results = new JNewsListResult[0];
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<JNewsListResult> Results { get; set; }
    }

    class JNewsListResult
    {
        public long Id { get; set; }
        public long Publication_Date { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public JPlace Place { get; set; }
        public IEnumerable<JImage> Images { get; set; }
    }
}
