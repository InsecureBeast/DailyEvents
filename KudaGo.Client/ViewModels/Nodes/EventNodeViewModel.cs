using KudaGo.Client.Extensions;
using KudaGo.Core.Data;
using KudaGo.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KudaGo.Client.ViewModels.Nodes
{
    class EventBaseNodeViewModel : NodeViewModel
    {
        public override string Title { get; protected set; }
        public override long Id { get; protected set; }
        public string Image { get; protected set; }
        public string Description { get; protected set; }
        public string Dates { get; protected set; }
        public string Times { get; protected set; }
    }

    class EventNodeViewModel : EventBaseNodeViewModel
    {
        public EventNodeViewModel(IEventListResult result, ICategoryNameProvider categoryNameProvider)
        {
            if (result == null)
                return;

            if (categoryNameProvider == null)
                throw new ArgumentNullException("categoryNameProvider");

            var image = result.Images.FirstOrDefault();
            if (image != null)
                Image = image.Thumbnail.Normal;

            Id = result.Id;
            Title = result.Title.GetNormalString();
            Description = result.Description;
            Age = result.AgeRestriction;
            IsFree = result.IsFree;

            if (result.Place != null)
                Place = result.Place.Title.GetNormalString();

            var firstCat = result.Categories.FirstOrDefault();
            var catName = categoryNameProvider.GetName(firstCat);
            if (string.IsNullOrEmpty(catName))
                Categories = firstCat;
            else
                Categories = catName;

            var dates = result.Dates.ToArray();
            if (dates == null)
                return;

            Dates = GetDates(dates);
            Times = GetTimes(dates);
        }

        public string Place { get; private set; }
        public string Age { get; private set; }
        public string Categories { get; private set; }
        public bool IsFree { get; private set; }


        public static string GetDates(IDate date)
        {
            return GetDates(new [] {date});
        }

        public static string GetDates(IEnumerable<IDate> dateList)
        {
            var datesStr = string.Empty;
            var dates = GetClosureDate(dateList);
            if (dates == null)
                return datesStr;

            if (dates.Start.HasValue && dates.End.HasValue)
            {
                var start = dates.Start.Value;
                var end = dates.End.Value;
                datesStr = string.Format("{0} - {1}", start.ToString("D"), end.ToString("D"));

                if (start.Date == end.Date)
                {
                    datesStr = start.ToString("D");
                }

                if (start.Date == new DateTime(0001, 01, 3))
                {
                    datesStr = string.Format("{0} - {1}", DateTime.Now.ToString("D"), end.ToString("D"));
                }

                if (start.Date == new DateTime(0001, 01, 3) && end.Date >= new DateTime(9999, 1, 1))
                {
                    datesStr = string.Empty;
                }
            }

            return datesStr;
        }

        public static string GetTimes(IDate date)
        {
            return GetTimes(new[] {date});
        }

        public static string GetTimes(IEnumerable<IDate> dateList)
        {
            var times = string.Empty;
            var dates = GetClosureDate(dateList);
            if (dates == null)
                return times;

            if (dates.Start.HasValue && dates.End.HasValue)
            {
                var start = dates.Start.Value;
                var end = dates.End.Value;
                times = string.Format("{0} - {1}", start.ToString("t"), end.ToString("t"));

                if (start.TimeOfDay == end.TimeOfDay)
                    times = start.ToString("t");

                if (start.TimeOfDay == TimeSpan.FromHours(0))
                    times = string.Empty;

                if (end.TimeOfDay == new TimeSpan(23, 59, 00))
                    times = string.Empty;

                if (start.Date == new DateTime(0001, 01, 3))
                    times = string.Empty;

                if (start.Date == new DateTime(0001, 01, 3) && end.Date >= new DateTime(3000, 1, 1))
                    times = string.Empty;
            }

            return times;
        }

        private static IDate GetClosureDate(IEnumerable<IDate> dateList)
        {
            var today = DateTime.Today;
            var dates = dateList.ToArray();
            var closure = dates.FirstOrDefault(d => d.Start >= today);
            return closure ?? dates.LastOrDefault();
        }
    }
}
