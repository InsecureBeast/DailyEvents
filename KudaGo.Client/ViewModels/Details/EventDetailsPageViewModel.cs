﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Client.Helpers;
using KudaGo.Client.Extensions;
using KudaGo.Core.Data;
using System.Collections.ObjectModel;
using KudaGo.Client.ViewModels.Nodes;
using KudaGo.Client.Controls;
using KudaGo.Client.ViewModels.Comments;

namespace KudaGo.Client.ViewModels.Details
{
    class EventDetailsPageViewModel : DetailsPageViewModel
    {
        private string _description;
        private ObservableCollection<string> _images;
        private string _age;
        private string _dates;
        private string _times;
        private string _place;
        private bool _isFree;
        private string _price;
        private string _metro;
        private ICoordinates _location;
        private readonly EventCommentsViewModel _eventCommentsViewModel;

        public EventDetailsPageViewModel(long eventId, DataSource dataSource) : base(eventId, dataSource)
        {
            _eventCommentsViewModel = new EventCommentsViewModel(eventId, dataSource);
            _images = new ObservableCollection<string>();
        }

        public string Description
        {
            get { return _description; }
            private set
            {
                _description = value;
                NotifyOfPropertyChanged(() => Description);
            }
        }

        public ObservableCollection<string> Images
        {
            get { return _images; }
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

        public EventCommentsViewModel EventCommentsViewModel
        {
            get { return _eventCommentsViewModel; }
        }

        protected override async Task LoadDetails(long id)
        {
            var rs = await _dataSource.GetEventDetails(id);
            if (rs == null)
                return;

            LayoutHelper.InvokeFromUiThread(async () => 
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description;
                BodyText = rs.BodyText;
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

                var dates = rs.Dates.LastOrDefault();
                if (dates != null)
                {
                    if (dates.Start.HasValue && dates.End.HasValue)
                    {
                        var start = dates.Start.Value;
                        var end = dates.End.Value;
                        var datesStr = string.Format("{0} - {1}", start.ToString("D"), end.ToString("D"));
                        var times = string.Format("{0} - {1}", start.ToString("t"), end.ToString("t"));
                        if (start == end)
                        {
                            datesStr = start.ToString("D");
                            times = start.ToString("t");
                        }
                        Dates = datesStr;
                        Times = times;
                    }
                }

                await EventCommentsViewModel.Load();
            });
        }
    }
}
