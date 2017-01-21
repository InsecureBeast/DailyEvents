using KudaGo.Client.Common;
using KudaGo.Client.Controls;
using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using KudaGo.Core.Data;
using KudaGo.Core.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class EventsViewModel : SectionViewModel, IFilterListener
    {
        private readonly DataSource _dataSource;
        private EventOfTheDayNodeViewModel _eventOfTheDay;
        private GetDataDelegate _getData;
        private string _filter;
        private bool? _isFree = null;

        public EventsViewModel(DataSource dataSource)
        {
            _dataSource = dataSource;
            _getData = GetEventData;

            LayoutHelper.InvokeFromUiThread(async () =>
            {
                var events = await _dataSource.GetEventOfTheDay(null);
                var eventOfTheDay = events.Results.FirstOrDefault();
                if (eventOfTheDay == null)
                    return;

                EventOfTheDay = new EventOfTheDayNodeViewModel(eventOfTheDay);
            });
        }

        public EventOfTheDayNodeViewModel EventOfTheDay
        {
            get { return _eventOfTheDay; }
            set
            {
                _eventOfTheDay = value;
                NotifyOfPropertyChanged(() => EventOfTheDay);
            }
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
            return await _getData(next);
        }

        public async void Update(CategoryPageViewModel categoryViewModel)
        {
            _filter = categoryViewModel.SelectedItem.Slug;

            if (categoryViewModel.SelectedItem is AllCategoryNodeViewModel)
                _getData = GetEventData;
            else
                _getData = GetDataWithFilter;

            _isFree = categoryViewModel.IsFree;
            Items.Clear();
            await Load();
        }

        private async Task<IResponse> GetDataWithFilter(string next)
        {
            return await _dataSource.GetEventsWithFilter(next, _filter, _isFree);
        }

        protected async Task<IResponse> GetEventData(string next)
        {
            return await _dataSource.GetEvents(next, _isFree);
        }
    }
}
