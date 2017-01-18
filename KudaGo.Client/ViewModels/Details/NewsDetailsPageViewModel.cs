using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.Model;
using KudaGo.Client.Helpers;
using KudaGo.Client.Extensions;
using System.Collections.ObjectModel;

namespace KudaGo.Client.ViewModels.Details
{
    class NewsDetailsPageViewModel : DetailsPageViewModel
    {
        private Uri _source;
        private string _date;

        public NewsDetailsPageViewModel(long id, DataSource dataSource) : base(id, dataSource)
        {
            
        }

        public Uri Source
        {
            get { return _source; }
            private set
            {
                _source = value;
                NotifyOfPropertyChanged(() => Source);
            }
        }

        public string Date
        {
            get { return _date; }
            private set
            {
                _date = value;
                NotifyOfPropertyChanged(() => Date);
            }
        }

        protected override async Task LoadDetails(long id)
        {
            var rs = await _dataSource.GetNewsDetails(id);
            if (rs == null)
                return;

            LayoutHelper.InvokeFromUiThread(() =>
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description;
                BodyText = rs.BodyText;
                Source = new Uri(rs.SiteUrl);

                foreach (var image in rs.Images)
                {
                    _images.Add(image.Image);
                }

                //if (rs.Place != null)
                //{
                //    Place = rs.Place.Title.GetNormalString();
                //    Metro = rs.Place.Subway;
                //    Location = rs.Place.Coords;
                //}

                if (rs.PublicationDate.HasValue)
                {
                    var format = ResourcesHelper.GetLocalizationString("PublishedAtStringFormat");
                    Date = string.Format(format, rs.PublicationDate.Value.ToString("g"));
                }

                //await EventCommentsViewModel.Load();
            });
        }
    }
}
