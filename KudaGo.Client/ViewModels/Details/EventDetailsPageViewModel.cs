using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Client.Helpers;
using KudaGo.Client.Extensions;

namespace KudaGo.Client.ViewModels.Details
{
    class EventDetailsPageViewModel : DetailsPageViewModel
    {
        private string _description;
        private string _image;
        private string _age;
        private string _dates;
        private string _times;
        private string _place;
        private bool _isFree;
        private string _price;

        public EventDetailsPageViewModel(long eventId, DataSource dataSource) : base(eventId, dataSource)
        {
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

        public string Image
        {
            get { return _image; }
            private set
            {
                _image = value;
                NotifyOfPropertyChanged(() => Image);
            }
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

        protected override async Task LoadDetails(long id)
        {
            var rs = await _dataSource.GetEventDetails(id);
            if (rs == null)
                return;

            LayoutHelper.InvokeFromUiThread(() => 
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description;
                BodyText = rs.BodyText;
                Age = rs.AgeRestriction;
                IsFree = rs.IsFree;
                Price = rs.Price;

                var image = rs.Images.FirstOrDefault();
                if (image != null)
                    Image = image.Image;

                if (rs.Place != null)
                    Place = rs.Place.Title.GetNormalString();

                var dates = rs.Dates.LastOrDefault();
                if (dates == null)
                    return;

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
            });
            
        }
    }
}
