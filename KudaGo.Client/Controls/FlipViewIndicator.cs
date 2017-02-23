using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace DailyEvents.Client.Controls
{
    public sealed class FlipViewIndicator : ListBox
    {
        public FlipViewIndicator()
        {
            this.DefaultStyleKey = typeof(FlipViewIndicator);
        }

        public FlipView Flipview
        {
            get { return (FlipView)GetValue(FlipviewProperty); }
            set { SetValue(FlipviewProperty, value); }
        }

        public static readonly DependencyProperty FlipviewProperty =
            DependencyProperty.Register("Flipview", typeof(FlipView), typeof(FlipViewIndicator), new PropertyMetadata(null, FlipView_Changed));

        private static void FlipView_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FlipViewIndicator that = d as FlipViewIndicator;
            FlipView flip = (e.NewValue as FlipView);
            that.ItemsSource = flip.ItemsSource;
            Binding binding = new Binding();
            binding.Mode = BindingMode.TwoWay;
            binding.Source = flip;
            binding.Path = new PropertyPath("SelectedItem");
            that.SetBinding(FlipViewIndicator.SelectedItemProperty, binding);
        }
    }
}
