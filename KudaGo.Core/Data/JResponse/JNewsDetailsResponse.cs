using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Core.Data.JResponse
{
    class JNewsDetailsResponse : JNewsListResult
    {
        public string Body_Text { get; set; }
        public string Site_Url { get; set; }
        public long Favorites_Count { get; set; }
        public long Comments_count { get; set; }
    }
}
