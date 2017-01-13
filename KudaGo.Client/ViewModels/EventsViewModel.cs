using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using KudaGo.Core.Data;
using KudaGo.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class EventsViewModel : SectionViewModel
    {
        private readonly DataSource _dataSource;
        private IEventsOfTheDayResult _eventOfTheDay;

        public EventsViewModel(DataSource dataSource)
        {
            _dataSource = dataSource;
            LayoutHelper.InvokeFromUiThread(async () =>
            {
                var events = await _dataSource.GetEventOfTheDay(null);
                _eventOfTheDay = events.Results.FirstOrDefault();
                //Items.Insert(0, new EventfTheDayNodeViewModel(_eventOfTheDay));
            });
        }

        protected override void AddData(IResponse response)
        {
            var res = response as IEventListResponse;
            if (res == null)
                return;

            foreach (var result in res.Results)
            {
                Items.Add(new EventNodeViewModel(result));
            }
            IsBusy = false;
        }

        protected override async Task<IResponse> GetData(string next)
        {
            IsBusy = true;
            return await _dataSource.GetEvents(next);
        }
    }
}
