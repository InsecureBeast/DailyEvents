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

            Title = GetNormalString(result.Title);
            Description = result.Description;
            Age = result.AgeRestriction;

            if (result.Place != null)
                Place = GetNormalString(result.Place.Title);

            //TODO
            Categories = result.Categories.FirstOrDefault();
            
            var dates = result.Dates.LastOrDefault();
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
        }

        private string GetNormalString(string title)
        {
            if (string.IsNullOrEmpty(title))
                return title;

            var normalTitle = title.ToCharArray();
            if (Char.IsLower(title[0]))
            {
                normalTitle[0] = Char.ToUpper(title[0]);
                return new string(normalTitle);
            }

            return title;
        }

        public string Image { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Place { get; private set; }
        public string Age { get; private set; }
        public string Dates { get; private set; }
        public string Times { get; private set; }
        public string Categories { get; private set; }
    }
}
