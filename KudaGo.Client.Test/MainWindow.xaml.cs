using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using KudaGo.Client.Test.Annotations;
using KudaGo.Core;
using KudaGo.Core.Data;
using KudaGo.Core.Events;
using KudaGo.Core.Search;

namespace KudaGo.Client.Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            Items = new ObservableCollection<EventViewModel>();
            Loaded += OnLoaded; 
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadEventOfTheDay();

            var request = new EventListRequest();
            request.Lang = "ru";
            request.Expand = string.Format("{0},{1}", EventListRequest.ExpandFields.IMAGES, EventListRequest.ExpandFields.PLACE);

            var fieldBuilder = new FieldsBuilder();
            request.Fields = fieldBuilder.WithField(EventFields.BODY_TEXT)
                .WithField(EventFields.COMMENTS_COUNT)
                .WithField(EventFields.DESCRIPTION)
                .WithField(EventFields.ID)
                .WithField(EventFields.IMAGES)
                .WithField(EventFields.PLACE)
                .WithField(EventFields.PUBLICATION_DATE)
                .WithField(EventFields.PRICE)
                .WithField(EventFields.TITLE)
                .WithField(EventFields.SITE_URL)
                .WithField(EventFields.SLUG).Build();
            request.ActualSince = DateTime.Today;
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();

            foreach (var result in res.Results)
            {
                Items.Add(new EventViewModel(result.Images.First().Thumbnail.Small, result.Title, result.Place));
            }
        }

        public ObservableCollection<EventViewModel> Items { get; set; }
        public EventViewModel EventOfTheDay { get; private set; }

        private async Task LoadEventOfTheDay()
        {
            var request = new EventsOfTheDayRequest();
            request.TextFormat = TextFormatEnum.Text;
            request.Location = Location.Spb;

            var res = await request.ExecuteAsync();
            var eventOfTheDay = res.Results.First().Event;

            var details = new EventDetailsRequest();
            details.EventId = eventOfTheDay.Id;
            var detailsRes = await details.ExecuteAsync();

            //var place = new PlaceRequest();
            //TODO
            
            EventOfTheDay = new EventViewModel(eventOfTheDay.FirstImage.Thumbnail.Small, eventOfTheDay.Title, null);
            OnPropertyChanged("EventOfTheDay");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class EventViewModel
    {
        public EventViewModel(string image, string name, IPlace place)
        {
            Image = image;
            Name = name;

            if (place == null)
                return;

            Place = place.Title;
        }
        public string Image { get; private set; }
        public string Name { get; private set; }
        public string Place { get; private set; }
    }
}
