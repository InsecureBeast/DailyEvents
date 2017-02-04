using KudaGo.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KudaGo.Client.Helpers
{
    static class NavigationHelper
    {
        public static void NavigateTo(Type pageType, object viewModel)
        {
            var frame = Window.Current.Content as Frame;
            if (frame == null)
                return;

            frame.Navigate(pageType, viewModel);
        }

        public static void NavigateToSettings()
        {
            NavigateTo(typeof(SettingsPage), new SettingsPageViewModel(App.DataSource, App.SettingsNotifier, App.SettingsProvider));
        }

        internal static void GoBack()
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            rootFrame.GoBack();
        }
    }
}
