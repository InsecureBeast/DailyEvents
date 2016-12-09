using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client
{
    class DateTimeHelper
    {
        public static DateTime? GetDateTimeFromUnixTime(long unixDateTime)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var date = start.AddSeconds(unixDateTime).ToLocalTime();
            return date;
        }
    }
}
