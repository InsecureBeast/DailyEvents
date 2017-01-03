using KudaGo.Client.Controls;
using KudaGo.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Core.Data;
using KudaGo.Core.Comments;
using KudaGo.Client.Model;

namespace KudaGo.Client.ViewModels.Comments
{
    class CommentsViewModel : PropertyChangedBase
    {
        private IncrementalObservableCollection<CommentNodeViewModel> _items;
        private bool _isBusy;
        protected readonly DataSource _dataSource;
        protected readonly long _id;

        public CommentsViewModel(long id, DataSource dataSource)
        {
            _id = id;
            _dataSource = dataSource;
            _items = new IncrementalObservableCollection<CommentNodeViewModel>(GetComments, AddComments);
            
        }

        public IncrementalObservableCollection<CommentNodeViewModel> Items
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

        protected virtual void AddComments(IResponse response)
        {
            var res = response as ICommentsResponse;
            if (res == null)
                return;

            foreach (var result in res.Results)
            {
                Items.Add(new CommentNodeViewModel(result));
            }
            IsBusy = false;
        }

        protected virtual Task<IResponse> GetComments(string next)
        {
            return null;
        }
    }
}
