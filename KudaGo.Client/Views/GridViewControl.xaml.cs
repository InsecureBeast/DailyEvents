using KudaGo.Client.Helpers;
using KudaGo.Client.ViewModels;
using KudaGo.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace KudaGo.Client.Views
{
    public sealed partial class GridViewControl : UserControl
    {
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(object), typeof(GridViewControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(GridViewControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemTemplateSelectorProperty =
            DependencyProperty.Register("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(GridViewControl), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderVisibilityProperty =
            DependencyProperty.Register("HeaderVisibility", typeof(Visibility), typeof(GridViewControl), new PropertyMetadata(Visibility.Collapsed, HeaderVisibilityChanged));

        public GridViewControl()
        {
            this.InitializeComponent();
            Loaded += GridViewControl_Loaded;
        }

        private void GridViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            gridView.SetBinding(GridView.ItemsSourceProperty, new Binding() { Path = new PropertyPath("Items"), Source = this });
            if (ItemTemplateSelector != null)
            {
                gridView.ItemTemplate = null;
                gridView.ItemTemplateSelector = ItemTemplateSelector;
            }
            else
            {
                gridView.ItemTemplate = ItemTemplate;
                gridView.ItemTemplateSelector = null;
            }

            gridViewHeader.Visibility = HeaderVisibility;

            if (gridView.SelectedItem != null)
                gridView.ScrollIntoView(gridView.SelectedItem);
        }

        public object Items
        {
            get { return GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
            set { SetValue(ItemTemplateSelectorProperty, value); }
        }

        public Visibility HeaderVisibility
        {
            get { return (Visibility)GetValue(HeaderVisibilityProperty); }
            set { SetValue(HeaderVisibilityProperty, value); }
        }

        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var columns = Math.Truncate(e.NewSize.Width / 300);
            var width = Math.Truncate(e.NewSize.Width / columns);
            var offset = columns == 1 ? 0 : 1;
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemWidth = width - offset;
        }

        private void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var parent = VisualHelper.FindParent<Page>(sender as FrameworkElement);
            if (parent == null)
                return;

            parent.Frame.Navigate(typeof(DetailsPage), e.ClickedItem);
        }

        private void Image_ImageExOpened(object sender, Microsoft.Toolkit.Uwp.UI.Controls.ImageExOpenedEventArgs e)
        {
            FadeBorder.Visibility = Visibility.Visible;
            HeaderTitle.Visibility = Visibility.Visible;
        }

        private void gridViewHeader_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var parent = VisualHelper.FindParent<Page>(sender as FrameworkElement);
            if (parent == null)
                return;

            //todo пока только под events
            var element = sender as FrameworkElement;
            var viewModel = element.DataContext as EventsViewModel;
            if (viewModel == null)
                return;

            parent.Frame.Navigate(typeof(DetailsPage), viewModel.EventOfTheDay);
        }

        private static void HeaderVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as GridViewControl;
            control.gridViewHeader.Visibility = (Visibility) e.NewValue;
        }

    }
}
