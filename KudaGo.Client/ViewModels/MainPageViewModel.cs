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
    internal class MainPageViewModel : PropertyChangedBase
    {
        private readonly IncrementalObservableCollection<EventViewModel> _items;
        private readonly DataSource _dataSource;
        private bool _isBusy;

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

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChanged(() => IsBusy);
            }
        }

        private async Task Load()
        {
            await _items.LoadMoreItemsAsync(0);
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
            IsBusy = false;
        }

        private async Task<IResponse> GetData(string next)
        {
            IsBusy = true;
            return await _dataSource.GetEvents(next);
        }
    }
}
