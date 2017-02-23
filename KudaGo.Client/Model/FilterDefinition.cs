using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.Model
{
    public class FilterDefinition
    {
        public DateTime? ActualSince { get; set; }
        public DateTime? ActualUntil { get; set; }
        public bool? IsFree { get; set; }
        public string Categories { get; set; }
    }
}
