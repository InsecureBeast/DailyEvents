using DailyEvents.Client.ViewModels;
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

namespace DailyEvents.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            // Caching your main page is good practice, this makes it snappy for the user to return to "home" of your app.
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // This page is always at the top of our in-app back stack.
            // Once it is reached there is no further back so we can always disable the title bar back UI when navigated here.
            // If you want to you can always to the Frame.CanGoBack check for all your pages and act accordingly.
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            base.OnNavigatedTo(e);
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = DataContext as MainPageViewModel;
            if (viewModel == null)
                return;

            BottomCommandBar.Visibility = Visibility.Collapsed;
            FilterPanel.Visibility = Visibility.Collapsed;
            switch (pivot.SelectedIndex)
            {
                case 0:
                    if (!viewModel.EventsViewModel.Items.Any())
                        await viewModel.EventsViewModel.Load();
                    BottomCommandBar.Visibility = Visibility.Visible;
                    FilterPanel.Visibility = Visibility.Visible;
                    break;
                case 1:
                    if (!viewModel.NewsViewModel.Items.Any())
                        await viewModel.NewsViewModel.Load();
                    break;
                case 2:
                    if (!viewModel.MoviesViewModel.Items.Any())
                        await viewModel.MoviesViewModel.Load();
                    break;
                case 3:
                    if (!viewModel.SelectionsViewModel.Items.Any())
                        await viewModel.SelectionsViewModel.Load();
                    break;
                default:
                    break;
            }
        }
    }
}
