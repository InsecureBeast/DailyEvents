using DailyEvents.Client.Common;
using DailyEvents.Client.Controls;
using DailyEvents.Client.Helpers;
using DailyEvents.Client.Model;
using DailyEvents.Client.ViewModels.Nodes;
using DailyEvents.Core.Data;
using DailyEvents.Core.Events;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DailyEvents.Client.ViewModels
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
        private FilterDefinition _filterDefinition;
        private string _filterName;

        public EventsViewModel(IDataSource dataSource)
        {
            _dataSource = dataSource;
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

        public string FilterName
        {
            get { return _filterName; }
            set
            {
                _filterName = value;
                NotifyOfPropertyChanged(() => FilterName);
            }
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
                if (Items.Count == AdvHelper.GetAdvItemIndex())
                    Items.Add(new AdvNodeViewModel());
            }

            base.AddData(response);
        }

        protected override async Task<IResponse> GetData(string next)
        {
            return await _dataSource.GetEvents(next, _filterDefinition);
        }

        public async void Update(CategoryPageViewModel categoryViewModel)
        {
            _filterDefinition = categoryViewModel.FilterDefinition;
            FilterName = _categoryNameProvider.GetName(_filterDefinition.Categories);
            Items.Clear();
            await Load();
        }

        public override async Task Update()
        {
            LoadEventOfDay();
            await base.Update();
        }

        protected override void Repeat(object obj)
        {
            base.Repeat(obj);
            LoadEventOfDay();
        }

        private void LoadEventOfDay()
        {
            LayoutHelper.InvokeFromUiThread(async () =>
            {
                try
                {
                    var events = await _dataSource.GetEventOfTheDay(null);
                    var eventOfTheDay = events.Results.FirstOrDefault();
                    EventOfTheDay = new EventOfTheDayNodeViewModel(eventOfTheDay);
                }
                catch (Exception e)
                {
                    LoadFailed(e);
                }
            });
        }
    }
}
