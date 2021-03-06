﻿using DailyEvents.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DailyEvents.Client.Common
{
    public class EventItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EventTemplate { get; set; }
        public DataTemplate AdvTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            return base.SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is EventNodeViewModel)
                return EventTemplate;

            var uiElement = container as UIElement;
            if (item is AdvNodeViewModel)
            {
                //VariableSizedWrapGrid.SetColumnSpan(uiElement, 2);
                return AdvTemplate;
            }

            return base.SelectTemplateCore(item, container);
        }
    }
}
