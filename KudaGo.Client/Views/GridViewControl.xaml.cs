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
            DependencyProperty.Register("ItemTemplateSelector", typeof(DataTemplateSelector), typeof(GridViewControl), new PropertyMetadata(null, Changed));

        private static void Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

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

        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var columns = Math.Truncate(e.NewSize.Width / 300);
            var width = Math.Truncate(e.NewSize.Width / columns);
            ((VariableSizedWrapGrid)gridView.ItemsPanelRoot).ItemWidth = width - 1;
        }

        private void gridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var parent = VisualHelper.FindParent<Page>(sender as FrameworkElement);
            if (parent == null)
                return;

            parent.Frame.Navigate(typeof(DetailsPage), e.ClickedItem);
        }
    }
}
