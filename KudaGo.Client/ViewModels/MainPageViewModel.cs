using KudaGo.Client.Controls;
using KudaGo.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data;
using KudaGo.Core.Events;

namespace KudaGo.Client.ViewModels
{
    internal class MainPageViewModel
    {
        private readonly IncrementalObservableCollection<EventViewModel> _items;
        private readonly DataSource _dataSource;

        public MainPageViewModel()
        {
            _items = new IncrementalObservableCollection<EventViewModel>(GetData, AddData);
            _dataSource = new DataSource();
            Load();
        }

        public ObservableCollection<EventViewModel> Items
        {
            get { return _items; }
        }

        private async Task Load()
        {
            await _items.LoadMoreItemsAsync(0);
            //var res = await _dataSource.GetEvents(string.Empty);
            //AddData(res);
        }

        private void AddData(IResponse response)
        {
            var res = response as IEventListResponse;
            if (res == null)
                return;

            foreach (var result in res.Results)
            {
                Items.Add(new EventViewModel(result));
            }
        }

        private async Task<IResponse> GetData(string next)
        {
            return await _dataSource.GetEvents(next);
        }
    }
}
