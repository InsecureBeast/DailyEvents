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
        }

        private static void InitializeStatusBar()
        {
            var accentBrush = Application.Current.Resources["ApplicationAccentBrush"] as SolidColorBrush;
            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = accentBrush.Color;
                    titleBar.ButtonForegroundColor = Colors.White;
                    titleBar.BackgroundColor = accentBrush.Color;
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
                    statusBar.BackgroundColor = accentBrush.Color;
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
                default:
                    break;
            }
        }
    }
}
