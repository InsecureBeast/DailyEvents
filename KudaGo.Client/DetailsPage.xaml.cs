using KudaGo.Client.ViewModels;
using KudaGo.Client.ViewModels.Details;
using KudaGo.Client.ViewModels.Nodes;
using KudaGo.Client.ViewModels.Search;
using KudaGo.Core.Search;
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
            if (vm is EventNodeViewModel)
            {
                SetTemplate(CType.Event, vm);
            }
            if (vm is EventOfTheDayNodeViewModel)
            {
                SetTemplate(CType.Event, vm);
            }
            if (vm is NewsNodeViewModel)
            {
                SetTemplate(CType.News, vm);
            }
            if (vm is SelectionNodeViewModel)
            {
                SetTemplate(CType.List, vm);
            }
            if (vm is MovieNodeViewModel)
            {
                SetTemplate(CType.Movie, vm);
            }
            if (vm is SearchNodeViewModel)
            {
                SetTemplate(((SearchNodeViewModel)vm).Type, vm);
            }
        }

        private void SetTemplate(CType type, NodeViewModel vm)
        {
            DataTemplate template = null;
            if (type == CType.Event)
            {
                var detailsViewModel = new EventDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = detailsViewModel;
                template = Resources["EventDetailsDataTemplate"] as DataTemplate;
            }
            if (type == CType.News)
            {
                var newsViewModel = new NewsDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = newsViewModel;
                template = Resources["NewsDetailsDataTemplate"] as DataTemplate;
            }
            if (type == CType.List)
            {
                var viewModel = new SelectionDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = viewModel;
                template = Resources["SelectionDetailsDataTemplate"] as DataTemplate;
            }
            if (type == CType.Place)
            {
                var viewModel = new PlaceDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = viewModel;
                template = Resources["PlaceDetailsDataTemplate"] as DataTemplate;
            }
            if (type == CType.Movie)
            {
                var viewModel = new MovieDetailsPageViewModel(vm.Id, App.DataSource);
                DataContext = viewModel;
                template = Resources["MovieDetailsDataTemplate"] as DataTemplate;
            }
            contentPresenter.ContentTemplate = template;
        }
    }
}
