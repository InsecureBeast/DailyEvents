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
    interface ICategoryNameProvider
    {
        string GetName(string slug);
    }

    class EventsViewModel : SectionViewModel, IFilterListener
    {
        private readonly IDataSource _dataSource;
        private ICategoryNameProvider _categoryNameProvider;
        private EventOfTheDayNodeViewModel _eventOfTheDay;
        private GetDataDelegate _getData;
        private string _filter;
        private bool? _isFree = null;

        public EventsViewModel(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _getData = GetEventData;
            LoadEventOfDay();
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

        public void SetCategoryNameProvider(ICategoryNameProvider provider)
        {
            _categoryNameProvider = provider;
        }

        protected override void AddData(IResponse response)
        {
            var res = response as IEventListResponse;
            if (res == null)
                return;

            foreach (var result in res.Results)
            {
                Items.Add(new EventNodeViewModel(result, _categoryNameProvider));
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

        public override async Task Update()
        {
            LoadEventOfDay();
            await base.Update();
        }

        private async Task<IResponse> GetDataWithFilter(string next)
        {
            return await _dataSource.GetEventsWithFilter(next, _filter, _isFree);
        }

        protected async Task<IResponse> GetEventData(string next)
        {
            return await _dataSource.GetEvents(next, _isFree);
        }

        private void LoadEventOfDay()
        {
            LayoutHelper.InvokeFromUiThread(async () =>
            {
                var events = await _dataSource.GetEventOfTheDay(null);
                var eventOfTheDay = events.Results.FirstOrDefault();
                EventOfTheDay = new EventOfTheDayNodeViewModel(eventOfTheDay);
            });
        }
    }
}
