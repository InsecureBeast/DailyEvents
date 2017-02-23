using DailyEvents.Client.Helpers;
using DailyEvents.Client.Model;
using DailyEvents.Client.ViewModels.Nodes;
using DailyEvents.Client.ViewModels.Search;
using DailyEvents.Core.Data;
using DailyEvents.Core.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyEvents.Client.ViewModels
{
    class SearchPageViewModel : SectionViewModel
    {
        private readonly IDataSource _dataSource;
        private readonly NavigationViewModel _navigationViewModel;

        public SearchPageViewModel(IDataSource dataSource, string q)
        {
            _dataSource = dataSource;
            _navigationViewModel = new NavigationViewModel(dataSource);
            _navigationViewModel.InSearchMode = true;
            _navigationViewModel.SearchString = q;

            Task.Factory.StartNew(async () =>
            {
                await Load();
            });
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
        }

        protected override void AddData(IResponse response)
        {
            var res = response as ISearchResponse;
            if (res == null)
                return;

            foreach (var result in res.Results)
            {
                Items.Add(new SearchNodeViewModel(result));
                if (Items.Count == AdvHelper.GetAdvItemIndex())
                    Items.Add(new AdvNodeViewModel());
            }

            base.AddData(response);
        }

        protected override async Task<IResponse> GetData(string next)
        {
            return await _dataSource.Search(_navigationViewModel.SearchString, next);
        }
    }
}
