﻿using DailyEvents.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Search;
using DailyEvents.Client.Common;
using DailyEvents.Client.Extensions;
using HtmlAgilityPack;

namespace DailyEvents.Client.ViewModels.Search
{
    class SearchNodeViewModel : NodeViewModel
    {
        private string _image;

        public SearchNodeViewModel(ISearchResult result)
        {
            Type = result.CType;
            Id = result.Id;
            Title = result.Title.GetNormalString();
            var descriptionHtml = result.Description.GetNormalString();
            Description = descriptionHtml.StripHtmlTags();

            Place = result.Address;
            Categories = string.Empty;

            Dates = EventNodeViewModel.GetDates(result.Dates);
            Times = EventNodeViewModel.GetTimes(result.Dates);

            var image = result.FirstImage;
            if (image.Image != null)
                Image = image.Thumbnail.Normal;
            else
                LoadImage(result.ItemUrl);
        }

        private async void LoadImage(string itemUrl)
        {
            var image = await ImageLoader.LoadImage(itemUrl);
            Image = image;
        }

        public override long Id { get; protected set; }
        public CType Type { get; private set; }
        public string Place { get; private set; }
        public string Categories { get; private set; }
        public bool IsFree { get { return false; } }
        public string Dates { get; private set; }
        public string Times { get; private set; }

        public string Image
        {
            get { return _image; }
            protected set
            {
                _image = value;
                NotifyOfPropertyChanged(() => Image);
            }
        }
        public string Description { get; protected set; }

        public override string ToString()
        {
            return Title + " : type = " + Type;
        }
    }
}
