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
        private FilterDefinition _filterDefinition;

        public EventsViewModel(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _getData = GetEventData;
            _filterDefinition = new FilterDefinition();
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
            {
                IsEmpty = true;
                return;
            }

            foreach (var result in res.Results)
            {
                Items.Add(new EventNodeViewModel(result, _categoryNameProvider));
            }

            base.AddData(response);
        }

        protected override async Task<IResponse> GetData(string next)
        {
            return await _getData(next);
        }

        public async void Update(CategoryPageViewModel categoryViewModel)
        {
            _filterDefinition = categoryViewModel.FilterDefinition;
            _getData = GetEventData;
            Items.Clear();
            await Load();
        }

        public override async Task Update()
        {
            LoadEventOfDay();
            await base.Update();
        }

        protected async Task<IResponse> GetEventData(string next)
        {
            return await _dataSource.GetEvents(next, _filterDefinition);
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
