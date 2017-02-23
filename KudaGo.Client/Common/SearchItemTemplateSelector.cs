using DailyEvents.Client.ViewModels.Nodes;
using DailyEvents.Client.ViewModels.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DailyEvents.Client.Common
{
    public class SearchItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EventTemplate { get; set; }
        public DataTemplate PlaceTemplate { get; set; }
        public DataTemplate NewsTemplate { get; set; }
        public DataTemplate SelectionTemplate { get; set; }
        public DataTemplate AdvTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            return base.SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is AdvNodeViewModel)
                return AdvTemplate;

            var vm = item as SearchNodeViewModel;
            if (vm == null)
                return base.SelectTemplateCore(item, container);

            switch (vm.Type)
            {
                case Core.Search.CType.News:
                    return NewsTemplate;
                case Core.Search.CType.Event:
                    return EventTemplate;
                case Core.Search.CType.Place:
                    return PlaceTemplate;
                case Core.Search.CType.List:
                    return SelectionTemplate;
                default:
                    break;
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}
