using KudaGo.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    internal class MainPageViewModel
    {
        private readonly ObservableCollection<EventViewModel> _items;
        private readonly DataSource _dataSource;

        public MainPageViewModel()
        {
            _items = new ObservableCollection<EventViewModel>();
            _dataSource = new DataSource();
            Load();
        }

        public ObservableCollection<EventViewModel> Items
        {
            get { return _items; }
        }

        private async Task Load()
        {
            var res = await _dataSource.GetEvents();
            foreach (var result in res.Results)
            {
                Items.Add(new EventViewModel(result));
            }
        }
    }
}
