using DailyEvents.Client.Extensions;
using DailyEvents.Client.Helpers;
using DailyEvents.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.ViewModels.Nodes
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
            {
                Image = image.Thumbnail.Normal;
                NotifyOfPropertyChanged(() => Image);
            }

            Id = @event.Id;
            Title = @event.Title.GetNormalString();
            Description = @event.Description;
            Categories = ResourcesHelper.GetLocalizationString("EventOfDay");

            var dates = @event.DateRange;
            if (dates == null)
                return;


            Dates = EventNodeViewModel.GetDates(@event.DateRange);
            Times = EventNodeViewModel.GetTimes(@event.DateRange);
        }

        public string Categories { get; private set; }
    }
}
