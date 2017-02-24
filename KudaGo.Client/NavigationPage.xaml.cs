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

            //Loaded += NavigationControl_Loaded;
            RootFrame.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        public FrameControl AppFrame
        {
            get { return RootFrame; }
        }

        private void NavigationControl_Loaded(object sender, RoutedEventArgs e)
        {
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
                //SearchButton.Command.Execute(null);
            }
        }

        private void SettingsButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationHelper.NavigateToSettings();
            splitView.IsPaneOpen = false;
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

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            BackRequested(ref handled);
            e.Handled = handled;
        }

        private void BackRequested(ref bool handled)
        {
            // Get a hold of the current frame so that we can inspect the app back stack.
            if (AppFrame == null)
                return;

            // Check to see if this is the top-most page on the app back stack.
            if (AppFrame.CanGoBack && !handled)
            {
                // If not, set the event to handled and go back to the previous page in the app.
                handled = true;
                AppFrame.GoBack();
            }
        }
    }
}
