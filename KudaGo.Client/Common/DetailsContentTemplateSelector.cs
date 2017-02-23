using DailyEvents.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DailyEvents.Client.Common
{
    class DetailsContentTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EventTemplate { get; set; }
        public DataTemplate NewsTemplate { get; set; }
        public DataTemplate SelectionTemplate { get; set; }
        public DataTemplate MovieTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (item is EventNodeViewModel)
                return EventTemplate;

            return base.SelectTemplateCore(item);
        }
    }
}
