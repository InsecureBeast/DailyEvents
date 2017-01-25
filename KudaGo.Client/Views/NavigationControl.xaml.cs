using KudaGo.Client.Helpers;
using KudaGo.Client.ViewModels;
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

namespace KudaGo.Client.Views
{
    public sealed partial class NavigationControl : UserControl
    {
        public static readonly DependencyProperty ViewContentProperty = 
            DependencyProperty.Register("ViewContent", typeof(object), typeof(NavigationControl), new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NavigationControl), new PropertyMetadata(null, TitleChanged));

        public NavigationControl()
        {
            InitializeComponent();
            var device = DeviceTypeHelper.GetDeviceFormFactorType();
            if (device == DeviceFormFactorType.Phone || device == DeviceFormFactorType.IoT || device == DeviceFormFactorType.Other)
                splitView.CompactPaneLength = 0;

            Loaded += NavigationControl_Loaded;
        }

        private void NavigationControl_Loaded(object sender, RoutedEventArgs e)
        {
            contentPresenter.SetBinding(ContentPresenter.ContentProperty, new Binding() { Path = new PropertyPath("ViewContent"), Source = this });

            if (Title != null)
                title.Text = Title;
        }

        public object ViewContent
        {
            get { return GetValue(ViewContentProperty); }
            set { SetValue(ViewContentProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private static void TitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as NavigationControl;
            if (control == null)
                return;

            control.title.Text = e.NewValue.ToString();
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchBoxLayout.Visibility = Visibility.Collapsed;
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SearchBox.Focus(FocusState.Programmatic);
        }

        private void CommandBar_Opened(object sender, object e)
        {

        }

        private void SearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SearchButton.Command.Execute(null);
            }
                
        }
    }
}
