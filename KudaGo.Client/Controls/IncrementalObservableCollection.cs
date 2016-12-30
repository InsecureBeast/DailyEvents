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
    class IncrementalObservableCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private GetDataDelegate _getData;
        private AddDataDelegate _addData;
        private bool _hasMoreItems = true;
        private string _next;
        private int _count;

        public delegate Task<IResponse> GetDataDelegate(string next);
        public delegate void AddDataDelegate(IResponse response);

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
            get;
            set;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return LoadDataAsync().AsAsyncOperation<LoadMoreItemsResult>();
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
                var dispatcher = Window.Current.Dispatcher;
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    _addData(result);
                });
                _hasMoreItems = !string.IsNullOrEmpty(result.Next);
            }
            Busy = false;

            return new LoadMoreItemsResult { Count = (uint)_count }; ;
        }
    }
}
