using KudaGo.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KudaGo.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            InitializeStatusBar();

            // Caching your main page is good practice, this makes it snappy for the user to return to "home" of your app.
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // This page is always at the top of our in-app back stack.
            // Once it is reached there is no further back so we can always disable the title bar back UI when navigated here.
            // If you want to you can always to the Frame.CanGoBack check for all your pages and act accordingly.
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private static void InitializeStatusBar()
        {
            var accenDarktBrush1 = Application.Current.Resources["ApplicationDarkAccentBrush1"] as SolidColorBrush;
            var accenDarktBrush2 = Application.Current.Resources["ApplicationDarkAccentBrush2"] as SolidColorBrush;
            var accenDarktBrush3 = Application.Current.Resources["ApplicationDarkAccentBrush3"] as SolidColorBrush;

            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = accenDarktBrush1.Color;
                    titleBar.ButtonHoverBackgroundColor = accenDarktBrush2.Color;
                    titleBar.ButtonPressedBackgroundColor = accenDarktBrush3.Color;
                    titleBar.ButtonForegroundColor = Colors.White;
                    titleBar.ButtonHoverForegroundColor = Colors.White;
                    titleBar.ButtonPressedForegroundColor = Colors.White;
                    titleBar.BackgroundColor = accenDarktBrush1.Color;
                    titleBar.ForegroundColor = Colors.White;
                }
            }

            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.HideAsync();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = accenDarktBrush1.Color;
                    statusBar.ForegroundColor = Colors.White;
                }
            }
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as MainPageViewModel;
            if (viewModel == null)
                return;
            switch (pivot.SelectedIndex)
            {
                case 0:
                    if (!viewModel.EventsViewModel.Items.Any())
                        await viewModel.EventsViewModel.Load();
                    break;
                case 1:
                    if (!viewModel.NewsViewModel.Items.Any())
                        await viewModel.NewsViewModel.Load();
                    break;
                case 2:
                    if (!viewModel.SelectionsViewModel.Items.Any())
                        await viewModel.SelectionsViewModel.Load();
                    break;
                case 3:
                    if (!viewModel.MoviesViewModel.Items.Any())
                        await viewModel.MoviesViewModel.Load();
                    break;
                default:
                    break;
            }
        }
    }
}
