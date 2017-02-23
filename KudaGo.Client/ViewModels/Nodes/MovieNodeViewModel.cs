using DailyEvents.Client.Extensions;
using DailyEvents.Client.Helpers;
using DailyEvents.Core.Movies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.ViewModels.Nodes
{
    class MovieNodeViewModel : NodeViewModel
    {
        public MovieNodeViewModel(IMovieListResult result)
        {
            if (result == null)
                return;

            var poster = result.Images.FirstOrDefault();
            if (poster != null)
                Poster = poster.Thumbnail.Normal;

            Id = result.Id;
            Title = result.Title.GetNormalString();
            Age = result.AgeRestriction;
            Year = result.Year;
            Country = result.Country;
            var format = ResourcesHelper.GetLocalizationString("MinutesStringFormat");
            RunningTime = string.Format(format, result.RunningTime);
        }


        public override long Id { get; protected set; }
        public string Poster { get; private set; }
        public override string Title { get; protected set; }
        public string Age { get; private set; }
        public string Year { get; private set; }
        public string Country { get; private set; }
        public string RunningTime { get; private set; }
    }
}
