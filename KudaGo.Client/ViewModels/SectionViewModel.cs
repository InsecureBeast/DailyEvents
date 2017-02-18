using KudaGo.Client.Command;
using KudaGo.Client.Controls;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using KudaGo.Core.Data;
using KudaGo.Core.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KudaGo.Client.ViewModels
{
    class SectionViewModel : PropertyChangedBase
    {
        private readonly IncrementalObservableCollection<NodeViewModel> _items;
        private readonly DelegateCommand _repeatCommand;
        private bool _isBusy = false;
        private bool _isEmpty = false;

        public SectionViewModel()
        {
            _items = new IncrementalObservableCollection<NodeViewModel>(GetData, AddData, LoadFailed);
            _items.IsBusyChanged += IsBusyChanged;
            _repeatCommand = new DelegateCommand(Repeat);
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

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                NotifyOfPropertyChanged(() => IsEmpty);
            }
        }

        public bool IsConnected
        {
            get
            {
                if (Items.Any())
                    return true;

                return NetworkInterface.GetIsNetworkAvailable();
            }
        }

        public ICommand RepeatCommand
        {
            get { return _repeatCommand; }
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
            IsEmpty = Items.Count == 0;
        }

        protected virtual Task<IResponse> GetData(string next)
        {
            return null;
        }

        protected void LoadFailed(Exception e)
        {
            if (e is HttpRequestException || e is WebException)
            {
                IsBusy = false;
                IsEmpty = false;
                NotifyOfPropertyChanged(() => IsConnected);
                return;
            }

            throw e;
        }

        private void IsBusyChanged(object sender, IsBusyEventArgs e)
        {
            IsBusy = e.IsBusy;
        }

        private async void Repeat(object obj)
        {
            await Load();
        }
    }
}
