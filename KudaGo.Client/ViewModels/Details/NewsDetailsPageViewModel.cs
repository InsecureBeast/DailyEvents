﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Client.Model;
using DailyEvents.Client.Helpers;
using DailyEvents.Client.Extensions;
using DailyEvents.Client.ViewModels.Comments;

namespace DailyEvents.Client.ViewModels.Details
{
    class NewsDetailsPageViewModel : DetailsPageViewModel
    {
        private string _date;

        public NewsDetailsPageViewModel(long id, string title, IDataSource dataSource) : base(id, title, dataSource)
        {
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

            LayoutHelper.InvokeFromUiThread(async () =>
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description.GetNormalString();
                BodyText = rs.BodyText.GetNormalString();
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

                await CommentsViewModel.Load();
            });
        }

        protected override CommentsViewModel CreateCommentsViewModel()
        {
            return new NewsCommentsViewModel(_id, _dataSource);
        }
    }
}
