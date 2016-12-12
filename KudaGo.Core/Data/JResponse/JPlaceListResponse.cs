using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data.JData;

namespace KudaGo.Core.Data.JResponse
{
    class JPlaceListResponse
    {
        public JPlaceListResponse()
        {
            Results = new JPlaceListResult[0];
        }

        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<JPlaceListResult> Results { get; set; }
    }

    class JPlaceListResult : JPlace
    {
        public bool Has_Parking_Lot { get; set; }
    }
}
