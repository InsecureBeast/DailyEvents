﻿using KudaGo.Client.Command;
using KudaGo.Client.Controls;
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
using System.Windows.Input;

namespace KudaGo.Client.ViewModels
{
    class EventsViewModel : SectionViewModel, IFilterListener
    {
        private readonly DataSource _dataSource;
        private EventOfTheDayNodeViewModel _eventOfTheDay;
        private readonly DelegateCommand _filterCommand;
        private GetDataDelegate _getData;
        private string _filter;

        public EventsViewModel(DataSource dataSource)
        {
            _dataSource = dataSource;
            _filterCommand = new DelegateCommand(Filter);
            _getData = GetEventData;

            LayoutHelper.InvokeFromUiThread(async () =>
            {
                var events = await _dataSource.GetEventOfTheDay(null);
                var eventOfTheDay = events.Results.FirstOrDefault();
                if (eventOfTheDay == null)
                    return;

                EventOfTheDay = new EventOfTheDayNodeViewModel(eventOfTheDay);
                //Items.Insert(0, EventOfTheDay);
            });
        }

        public ICommand FilterCommand
        {
            get { return _filterCommand; }
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

        private void Filter(object obj)
        {
            NavigationHelper.NavigateTo(typeof(CategoryPage), new CategoryPageViewModel(_dataSource, this));
        }

        public async void Update(CategoryPageViewModel categoryViewModel)
        {
            _filter = categoryViewModel.SelectedItem.Slug;
            _getData = GetDataWithFilter;
            Items.Clear();
            await Load();
        }

        private async Task<IResponse> GetDataWithFilter(string next)
        {
            return await _dataSource.GetEventsWithFilter(next, _filter);
        }

        protected async Task<IResponse> GetEventData(string next)
        {
            return await _dataSource.GetEvents(next);
        }
    }
}
