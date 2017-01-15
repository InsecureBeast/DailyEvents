using KudaGo.Client.Extensions;
using KudaGo.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels.Nodes
{
    class EventOfTheDayNodeViewModel : EventBaseNodeViewModel
    {
        public EventOfTheDayNodeViewModel(IEventsOfTheDayResult result)
        {
            if (result == null)
                return;

            var @event = result.Event;

            var image = @event.FirstImage;
            if (image != null)
                Image = image.Thumbnail.Normal;

            Id = @event.Id;
            Title = @event.Title.GetNormalString();
            Description = @event.Description;

            var dates = @event.DateRange;
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
    }
}
