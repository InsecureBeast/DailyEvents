﻿using System;

namespace DailyEvents.Core
{
    class DateTimeHelper
    {
        public static DateTime? GetDateTimeFromUnixTime(long unixDateTime)
        {
            if (unixDateTime == 0)
                return null;

            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var date = start.AddSeconds(unixDateTime).ToLocalTime();
            return date;
        }

        public static long ToUnixTimestamp(DateTime dateTime)
        {
            return (long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
