using DailyEvents.Client.ViewModels;
using DailyEvents.Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DailyEvents.Client.Helpers
{
    static class NavigationHelper
    {
        public static void NavigateTo(Type pageType, object viewModel)
        {
            NavigationPage navPage = Window.Current.Content as NavigationPage;
            if (navPage == null)
                return;

            navPage.AppFrame.Navigate(pageType, viewModel);
        }

        public static void NavigateToSettings()
        {
            NavigationPage navPage = Window.Current.Content as NavigationPage;
            NavigateTo(typeof(SettingsPage), new SettingsPageViewModel(App.DataSource, App.SettingsNotifier, App.SettingsProvider));
        }

        internal static void GoBack()
        {
            NavigationPage navPage = Window.Current.Content as NavigationPage;
            if (navPage == null)
                return;

            navPage.AppFrame.GoBack();
        }

        internal static void GoHome()
        {
            NavigationPage navPage = Window.Current.Content as NavigationPage;
            if (navPage == null)
                return;

            navPage.AppFrame.Navigate(typeof(MainPage));
        }
    }
}
