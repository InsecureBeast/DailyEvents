using DailyEvents.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.Helpers
{
    class AdvHelper
    {
        public static bool IsAdvItem(object item)
        {
            return item is AdvNodeViewModel;
        }

        public static int GetAdvItemIndex()
        {
            return 4;
        }
    }
}
