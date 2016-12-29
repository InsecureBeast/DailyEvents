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

        public static readonly DependencyProperty ItemsDataTemplateProperty =
            DependencyProperty.Register("ItemsDataTemplate", typeof(DataTemplate), typeof(GridViewControl), new PropertyMetadata(null));


        public GridViewControl()
        {
            this.InitializeComponent();
            Loaded += GridViewControl_Loaded;
        }

        private void GridViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            gridView.SetBinding(GridView.ItemsSourceProperty, new Binding() { Path = new PropertyPath("Items"), Source = this });
            gridView.ItemTemplate = ItemsDataTemplate;
        }

        public object Items
        {
            get { return GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public DataTemplate ItemsDataTemplate
        {
            get { return (DataTemplate)GetValue(ItemsDataTemplateProperty); }
            set { SetValue(ItemsDataTemplateProperty, value); }
        }

        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var columns = Math.Truncate(e.NewSize.Width / 300);
            var width = Math.Truncate(e.NewSize.Width / columns);
            ((ItemsWrapGrid)gridView.ItemsPanelRoot).ItemWidth = width - 1;
        }
    }
}
