using KudaGo.Client.Controls;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using KudaGo.Core.Data;
using KudaGo.Core.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class SectionViewModel : PropertyChangedBase
    {
        private readonly IncrementalObservableCollection<NodeViewModel> _items;
        private bool _isBusy;

        public SectionViewModel()
        {
            _items = new IncrementalObservableCollection<NodeViewModel>(GetData, AddData);
        }

        public ObservableCollection<NodeViewModel> Items
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

        public async Task Load()
        {
            await _items.LoadMoreItemsAsync(0);
        }

        public virtual async Task Update()
        {
            if (Items.Count == 0)
                return;

            Items.Clear();
            await Load();
        }

        protected virtual void AddData(IResponse response)
        {
        }

        protected virtual Task<IResponse> GetData(string next)
        {
            return null;
        }
    }
}
