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
    class MovieDetailsPageViewModel : DetailsPageViewModel
    {
        private string _age;
        private string _country;
        private string _language;
        private string _year;
        private string _runningTime;
        private string _budget;
        private string _actors;
        private string _writer;
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

        public string Budget
        {
            get { return _budget; }
            private set
            {
                _budget = value;
                NotifyOfPropertyChanged(() => Budget);
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

        public string Writer
        {
            get { return _writer; }
            private set
            {
                _writer = value;
                NotifyOfPropertyChanged(() => Writer);
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

            LayoutHelper.InvokeFromUiThread(() =>
            {
                Title = rs.Title.GetNormalString();
                Description = rs.Description;
                BodyText = rs.BodyText;

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
                Budget = rs.Budget;
                Actors = rs.Stars;
                Writer = rs.Writer;
                Director = rs.Director;

                //await EventCommentsViewModel.Load();
            });
        }
    }
}
