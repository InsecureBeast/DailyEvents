using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Core
{
    public class DailyEventsNotFoundException : Exception
    {
        public DailyEventsNotFoundException(string message) : base(message)
        {
        }
    }
}
