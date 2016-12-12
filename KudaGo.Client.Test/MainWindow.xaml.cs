using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KudaGo.Core;
using KudaGo.Core.Events;

namespace KudaGo.Client.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Items = new ObservableCollection<IconViewModel>();
            Loaded += OnLoaded; 
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            var request = new EventListRequest();
            request.Lang = "ru";
            request.Expand = string.Format("{0},{1}", EventListRequest.ExpandFields.IMAGES, EventListRequest.ExpandFields.PLACE);

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder.WithField(FieldsBuilder.BODY_TEXT)
                .WithField(FieldsBuilder.COMMENTS_COUNT)
                .WithField(FieldsBuilder.DESCRIPTION)
                .WithField(FieldsBuilder.ID)
                .WithField(FieldsBuilder.IMAGES)
                .WithField(FieldsBuilder.BODY_TEXT)
                .WithField(FieldsBuilder.PLACE)
                .WithField(FieldsBuilder.PUBLICATION_DATE)
                .WithField(FieldsBuilder.PRICE)
                .WithField(FieldsBuilder.SHORT_TITLE)
                .WithField(FieldsBuilder.SITE_URL)
                .WithField(FieldsBuilder.SLUG).Build();
            request.ActualSince = DateTime.Today;
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();

            foreach (var result in res.Results)
            {
                Items.Add(new IconViewModel(result.Images.First().Thumbnails._640x384, result.Short_Title));
            }
        }

        public ObservableCollection<IconViewModel> Items { get; set; }
    }

    public class IconViewModel
    {
        public IconViewModel(string image, string name)
        {
            Image = image;
            Name = name;
        }
        public string Image { get; private set; }
        public string Name { get; private set; }
    }
}
