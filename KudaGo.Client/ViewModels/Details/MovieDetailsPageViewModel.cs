using System;
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
    class MovieDetailsPageViewModel : DetailsPageViewModel
    {
        private string _age;
        private string _country;
        private string _language;
        private string _year;
        private string _runningTime;
        private string _trailer;
        private string _actors;
        private string _url;
        private string _director;

        public MovieDetailsPageViewModel(long id, IDataSource dataSource) : base(id, dataSource)
        {
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

        public string Country
        {
            get { return _country; }
            private set
            {
                _country = value;
                NotifyOfPropertyChanged(() => Country);
            }
        }

        public string Year
        {
            get { return _year; }
            private set
            {
                _year = value;
                NotifyOfPropertyChanged(() => Year);
            }
        }

        public string Language
        {
            get { return _language; }
            private set
            {
                _language = value;
                NotifyOfPropertyChanged(() => Language);
            }
        }

        public string RunningTime
        {
            get { return _runningTime; }
            private set
            {
                _runningTime = value;
                NotifyOfPropertyChanged(() => RunningTime);
            }
        }

        public string Trailer
        {
            get { return _trailer; }
            private set
            {
                _trailer = value;
                NotifyOfPropertyChanged(() => Trailer);
            }
        }

        public string Actors
        {
            get { return _actors; }
            private set
            {
                _actors = value;
                NotifyOfPropertyChanged(() => Actors);
            }
        }

        public string Url
        {
            get { return _url; }
            private set
            {
                _url = value;
                NotifyOfPropertyChanged(() => Url);
            }
        }

        public string Director
        {
            get { return _director; }
            private set
            {
                _director = value;
                NotifyOfPropertyChanged(() => Director);
            }
        }

        protected override async Task LoadDetails(long id)
        {
            var rs = await _dataSource.GetMovieDetails(id);
            if (rs == null)
                return;

            LayoutHelper.InvokeFromUiThread(async () =>
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description.GetNormalString();
                BodyText = rs.BodyText.GetNormalString();

                foreach (var image in rs.Images)
                {
                    _images.Add(image.Image);
                }

                
                Age = rs.AgeRestriction;
                Country = rs.Country;
                Year = rs.Year;
                Language = rs.Language;
                var format = ResourcesHelper.GetLocalizationString("MinutesStringFormat");
                RunningTime = string.Format(format, rs.RunningTime);
                Trailer = rs.Trailer;
                Actors = rs.Stars;
                Url = rs.Url;
                Director = rs.Director;

                await CommentsViewModel.Load();
            });
        }

        protected override CommentsViewModel CreateCommentsViewModel()
        {
            return new MovieCommentsViewModel(_id, _dataSource);
        }
    }
}
