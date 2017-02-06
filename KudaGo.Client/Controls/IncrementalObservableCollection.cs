using KudaGo.Client.Helpers;
using KudaGo.Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace KudaGo.Client.Controls
{
    public delegate Task<IResponse> GetDataDelegate(string next);
    public delegate void AddDataDelegate(IResponse response);

    class IncrementalObservableCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private GetDataDelegate _getData;
        private AddDataDelegate _addData;
        private bool _hasMoreItems = true;
        private string _next;
        private int _count;
        private bool _isBusy;

        public event EventHandler<IsBusyEventArgs> IsBusyChanged;

        public IncrementalObservableCollection(GetDataDelegate getData, AddDataDelegate addData)
        {
            _getData = getData;
            _addData = addData;
        }

        public bool HasMoreItems
        {
            get { return _hasMoreItems; }
        }

        public bool Busy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                LayoutHelper.InvokeFromUiThread(() =>
                {
                    IsBusyChanged?.Invoke(this, new IsBusyEventArgs(value));
                });
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return LoadDataAsync().AsAsyncOperation<LoadMoreItemsResult>();
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            _hasMoreItems = true;
            _next = string.Empty;
        }

        private async Task<LoadMoreItemsResult> LoadDataAsync()
        {
            if (Busy)
                return new LoadMoreItemsResult { Count = (uint)Count };

            Busy = true;
            var result = await _getData(_next);

            if (_hasMoreItems)
            {
                _next = result.Next;
                _count = result.Count;
                LayoutHelper.InvokeFromUiThread(() => 
                {
                    _addData(result);
                });
                _hasMoreItems = !string.IsNullOrEmpty(result.Next);
            }
            Busy = false;

            return new LoadMoreItemsResult { Count = (uint)_count }; ;
        }
    }

    class IsBusyEventArgs : EventArgs
    {
        public IsBusyEventArgs(bool isBusy)
        {
            IsBusy = isBusy;
        }

        public bool IsBusy { get; private set; }
    }
}
