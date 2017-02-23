using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace DailyEvents.Client.Common
{
    public class TappedEventProvider
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(TappedEventProvider), new PropertyMetadata(null, OnCommandChange));

        public static void SetCommand(DependencyObject element, ICommand value)
        {
            element.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject element)
        {
            return (ICommand)element.GetValue(CommandProperty);
        }

        private static void OnCommandChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as FrameworkElement;
            if (element == null)
                return;

            element.Tapped += Element_Tapped;
        }

        private static void Element_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            var command = GetCommand(sender as FrameworkElement);
            if (command == null)
                return;

            //TODO command parametr
            command.Execute(null);
        }
    }
}
