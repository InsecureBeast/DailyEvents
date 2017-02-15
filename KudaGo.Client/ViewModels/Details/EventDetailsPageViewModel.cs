using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Client.Helpers;
using KudaGo.Client.Extensions;
using KudaGo.Core.Data;
using KudaGo.Client.ViewModels.Comments;
using System.Windows.Input;
using KudaGo.Client.Command;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using KudaGo.Client.ViewModels.Nodes;

namespace KudaGo.Client.ViewModels.Details
{
    class EventDetailsPageViewModel : DetailsPageViewModel
    {
        private string _age;
        private string _dates;
        private string _times;
        private string _place;
        private bool _isFree;
        private string _price;
        private string _metro;
        private ICoordinates _location;
        private readonly DelegateCommand _mapCommand;

        public EventDetailsPageViewModel(long eventId, IDataSource dataSource) : base(eventId, dataSource)
        {
            _mapCommand = new DelegateCommand(MapOpen);
        }

        public string Age
        {
            get { return _age; }
            private set
            {
                _age = value;
                NotifyOfPropertyChanged(() => Age);
            }
        }

        public string Dates
        {
            get { return _dates; }
            private set
            {
                _dates = value;
                NotifyOfPropertyChanged(() => Dates);
            }
        }

        public string Times
        {
            get { return _times; }
            private set
            {
                _times = value;
                NotifyOfPropertyChanged(() => Times);
            }
        }

        public string Place
        {
            get { return _place; }
            private set
            {
                _place = value;
                NotifyOfPropertyChanged(() => Place);
            }
        }

        public string Metro
        {
            get { return _metro; }
            private set
            {
                _metro = value;
                NotifyOfPropertyChanged(() => Metro);
            }
        }

        public string Price
        {
            get { return _price; }
            private set
            {
                _price = value;
                NotifyOfPropertyChanged(() => Price);
            }
        }

        public bool IsFree
        {
            get { return _isFree; }
            private set
            {
                _isFree = value;
                NotifyOfPropertyChanged(() => IsFree);
            }
        }

        public ICoordinates Location
        {
            get { return _location; }
            private set
            {
                _location = value;
                NotifyOfPropertyChanged(() => Location);
            }
        }

        public ICommand MapCommand
        {
            get { return _mapCommand; }
        }

        protected override async Task LoadDetails(long id)
        {
            var rs = await _dataSource.GetEventDetails(id);
            if (rs == null)
                return;

            LayoutHelper.InvokeFromUiThread(async () => 
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description.GetNormalString();
                BodyText = rs.BodyText.GetNormalString();
                Age = rs.AgeRestriction;
                IsFree = rs.IsFree;
                Price = rs.Price;

                foreach (var image in rs.Images)
                {
                    _images.Add(image.Image);
                }

                if (rs.Place != null)
                {
                    Place = rs.Place.Title.GetNormalString();
                    Metro = rs.Place.Subway;
                    Location = rs.Place.Coords;
                }

                var dates = rs.Dates;
                if (dates != null)
                {
                    Dates = EventNodeViewModel.GetDates(dates);
                    Times = EventNodeViewModel.GetTimes(dates);
                }

                await CommentsViewModel.Load();
            });
        }

        protected override CommentsViewModel CreateCommentsViewModel()
        {
            return new EventCommentsViewModel(_id, _dataSource);
        }

        private void MapOpen(object obj)
        {
            var frame = Window.Current.Content as Frame;
            if (frame == null)
                return;

            frame.Navigate(typeof(MapPage), new MapPageViewModel(Location, Place, _dataSource));
        }
    }
}
