using KudaGo.Client.ViewModels;
using KudaGo.Client.ViewModels.Details;
using KudaGo.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KudaGo.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                // If we have pages in our in-app backstack and have opted in to showing back, do so
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if there are no pages in our in-app back stack
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }

            SetTemplate(e.Parameter as NodeViewModel);
        }

        //TemplateSelector not working!!! что за херня!!!
        private void SetTemplate(NodeViewModel vm)
        {
            DataTemplate template = null;
            if (vm is EventNodeViewModel)
            {
                var detailsViewModel = new EventDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = detailsViewModel;
                template = Resources["EventDetailsDataTemplate"] as DataTemplate;
            }
            if (vm is EventfTheDayNodeViewModel)
            {
                var detailsViewModel = new EventDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = detailsViewModel;
                template = Resources["EventDetailsDataTemplate"] as DataTemplate;
            }
            if (vm is NewsNodeViewModel)
            {
                var newsViewModel = new NewsDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = newsViewModel;
                template = Resources["NewsDetailsDataTemplate"] as DataTemplate;
            }
            if (vm is SelectionNodeViewModel)
            {
                var viewModel = new SelectionDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = viewModel;
                template = Resources["SelectionDetailsDataTemplate"] as DataTemplate;
            }
            if (vm is MovieNodeViewModel)
            {
                var viewModel = new MovieDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = viewModel;
                template = Resources["MovieDetailsDataTemplate"] as DataTemplate;
            }
            contentPresenter.ContentTemplate = template;
        }
    }
}
