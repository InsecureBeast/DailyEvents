using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DailyEvents.Client.Controls
{
    public interface ITitleProvider
    {
        string Title { get; }
    }

    public sealed partial class FrameControl : Frame, ITitleProvider
    {

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(FrameControl), new PropertyMetadata(null, change));

        private static void change(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public FrameControl()
        {
            this.InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            var oldPage = oldContent as Page;
            if (oldPage != null)
                oldPage.DataContextChanged -= Page_DataContextChanged;

            var page = newContent as Page;
            page.DataContextChanged += Page_DataContextChanged;

            base.OnContentChanged(oldContent, newContent);
        }

        private void Page_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (sender.DataContext is ViewModels.NavigationViewModel)
                return;

            var provider = sender.DataContext as ITitleProvider;
            if (provider == null)
                return;

            Binding myBinding = new Binding();
            myBinding.Source = provider.Title;
            SetBinding(TitleProperty, myBinding);
            //Title = provider.Title;
        }
    }
}
