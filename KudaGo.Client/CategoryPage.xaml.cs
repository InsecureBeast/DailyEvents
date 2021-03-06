﻿using DailyEvents.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using DailyEvents.Client.ViewModels.Nodes;
using DailyEvents.Client.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DailyEvents.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryPage : Page
    {
        public CategoryPage()
        {
            this.InitializeComponent();
            Loaded += CategoryPage_Loaded;
        }

        private void CategoryPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (listView.SelectedItem != null)
                listView.ScrollIntoView(listView.SelectedItem);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NavigationPage navPage = Window.Current.Content as NavigationPage;
            if (navPage.AppFrame.CanGoBack)
            {
                // If we have pages in our in-app backstack and have opted in to showing back, do so
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if there are no pages in our in-app back stack
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }

            var vm = e.Parameter as CategoryPageViewModel;
            DataContext = vm;
            vm.Load();

            base.OnNavigatedTo(e);
        }

        private void ListView_OnItemClick(object sender, ItemClickEventArgs e)
        {
            var viewModel = DataContext as CategoryPageViewModel;
            if (viewModel == null)
                return;

            viewModel.SelectedItem = null;
        }
    }
}
