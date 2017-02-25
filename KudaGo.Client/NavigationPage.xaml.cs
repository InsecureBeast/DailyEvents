using DailyEvents.Client.Helpers;
using DailyEvents.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DailyEvents.Client.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DailyEvents.Client.Views
{
    public partial class NavigationPage : Page
    {
        public NavigationPage()
        {
            InitializeComponent();
            var device = DeviceTypeHelper.GetDeviceFormFactorType();
            if (device == DeviceFormFactorType.Phone || device == DeviceFormFactorType.IoT || device == DeviceFormFactorType.Other)
                splitView.CompactPaneLength = 0;

        }

        public FrameControl AppFrame
        {
            get { return RootFrame; }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchBoxLayout.Visibility = Visibility.Collapsed;
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SearchBoxLayout.Visibility = Visibility.Visible;
            var vm = DataContext as NavigationViewModel;
            vm.SearchString = string.Empty;
            SearchBox.Focus(FocusState.Programmatic);
        }

        private void SearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                SearchButton.Focus(FocusState.Programmatic);
                var vm = DataContext as NavigationViewModel;
                vm.SearchCommand.Execute(null);
            }
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = false;
            NavigationHelper.NavigateToSettings();
        }

        private void HomeButton_OnClick(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = false;
            NavigationHelper.GoHome();
        }

        private async void FeedbackButton_OnClick(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = false;
            var emailMessage = new EmailMessage();
            emailMessage.To.Add(new EmailRecipient("pe.dmitriev@gmail.com"));
            emailMessage.Body = "";
            emailMessage.Subject = "Daily Events feedback";
            await EmailManager.ShowComposeNewEmailAsync(emailMessage);
        }

        private void PlacesButton_OnClick(object sender, RoutedEventArgs e)
        {
            splitView.IsPaneOpen = false;
        }
    }
}
