using DailyEvents.Client.Helpers;
using DailyEvents.Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace DailyEvents.Client.Controls
{
    public delegate Task<IResponse> GetDataDelegate(string next);
    public delegate void AddDataDelegate(IResponse response);

    class IncrementalObservableCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private readonly GetDataDelegate _getData;
        private readonly AddDataDelegate _addData;
        private readonly Action<Exception> _failed;
        private bool _hasMoreItems = true;
        private string _next;
        private int _count;
        private bool _isBusy;

        public event EventHandler<IsBusyEventArgs> IsBusyChanged;

        public IncrementalObservableCollection(GetDataDelegate getData, AddDataDelegate addData, Action<Exception> failed)
        {
            _getData = getData;
            _addData = addData;
            _failed = failed;
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
            _hasMoreItems = count >= 0;
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
            try
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
            }
            catch (Exception e)
            {
                LayoutHelper.InvokeFromUiThread(() =>
                {
                    _failed(e);
                });
                _hasMoreItems = false;
            }
            Busy = false;
            return new LoadMoreItemsResult { Count = (uint)_count };
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
