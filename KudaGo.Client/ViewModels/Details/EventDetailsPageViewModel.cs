﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Client.Model;
using DailyEvents.Client.Helpers;
using DailyEvents.Client.Extensions;
using DailyEvents.Core.Data;
using DailyEvents.Client.ViewModels.Comments;
using System.Windows.Input;
using DailyEvents.Client.Command;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DailyEvents.Client.ViewModels.Nodes;
using DailyEvents.Client.Views;

namespace DailyEvents.Client.ViewModels.Details
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

        public EventDetailsPageViewModel(long eventId, string title, IDataSource dataSource) : base(eventId, title, dataSource)
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

        public long _placeId { get; private set; }

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
                if (!string.IsNullOrEmpty(rs.SiteUrl))
                    Source = new Uri(rs.SiteUrl);

                foreach (var image in rs.Images)
                {
                    _images.Add(image.Image);
                }

                if (rs.Place != null)
                {
                    Place = rs.Place.Title.GetNormalString();
                    Metro = rs.Place.Subway;
                    Location = rs.Place.Coords;
                    _placeId = rs.Place.Id;
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
            NavigationPage navPage = Window.Current.Content as NavigationPage;
            if (navPage == null)
                return;

            navPage.AppFrame.Navigate(typeof(MapPage), new MapPageViewModel(Location, Place, Metro, _placeId, _dataSource));
        }
    }
}
