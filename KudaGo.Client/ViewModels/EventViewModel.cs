using KudaGo.Core.Data;
using KudaGo.Core.Events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class EventViewModel
    {
        public EventViewModel(IEventListResult result)
        {
            if (result == null)
                return;

            var image = result.Images.FirstOrDefault();
            if (image != null)
                Image = image.Thumbnail.Normal;

            Title = result.Title;
            Description = result.Description;
            Age = result.AgeRestriction;

            if (result.Place != null)
                Place = result.Place.Title;

            var dates = result.Dates.LastOrDefault();
            if (dates == null)
                return;

            if (dates.Start.HasValue && dates.End.HasValue)
            {
                Dates = string.Format("{0} - {1}", dates.Start.Value.ToString("D"), dates.End.Value.ToString("D"));
                Times = string.Format("{0} - {1}", dates.Start.Value.ToString("t"), dates.End.Value.ToString("t"));
            }
        }

        public string Image { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Place { get; private set; }
        public string Age { get; private set; }
        public string Dates { get; private set; }
        public string Times { get; private set; }
    }
}
