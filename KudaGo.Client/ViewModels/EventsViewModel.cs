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
        private FilterDefinition _filterDefinition;

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
            Items.Clear();
            await Load();
        }

        public override async Task Update()
        {
            LoadEventOfDay();
            await base.Update();
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
