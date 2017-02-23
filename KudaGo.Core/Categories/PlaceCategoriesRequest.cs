using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Core.Categories
{
    public class PlaceCategoriesRequest : EventCategoriesRequest
    {
        protected override string GetRelativePath()
        {
            return "/place-categories/?";
        }
    }
}
