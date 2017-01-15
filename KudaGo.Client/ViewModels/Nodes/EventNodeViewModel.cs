using KudaGo.Client.Extensions;
using KudaGo.Core.Data;
using KudaGo.Core.Events;
using System;
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
        public EventNodeViewModel(IEventListResult result)
        {
            if (result == null)
                return;

            var image = result.Images.FirstOrDefault();
            if (image != null)
                Image = image.Thumbnail.Normal;

            Id = result.Id;
            Title = result.Title.GetNormalString();
            Description = result.Description;
            Age = result.AgeRestriction;

            if (result.Place != null)
                Place = result.Place.Title.GetNormalString();

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

        public string Place { get; private set; }
        public string Age { get; private set; }
        public string Categories { get; private set; }
    }
}
