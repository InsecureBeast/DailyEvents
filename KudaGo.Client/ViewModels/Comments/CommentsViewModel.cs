﻿using DailyEvents.Client.Controls;
using DailyEvents.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Core.Data;
using DailyEvents.Core.Comments;
using DailyEvents.Client.Model;

namespace DailyEvents.Client.ViewModels.Comments
{
    class CommentsViewModel : PropertyChangedBase
    {
        private readonly IncrementalObservableCollection<CommentNodeViewModel> _items;
        private bool _isBusy;
        protected readonly IDataSource _dataSource;
        protected readonly long _id;

        public CommentsViewModel(long id, IDataSource dataSource)
        {
            _id = id;
            _dataSource = dataSource;
            _items = new IncrementalObservableCollection<CommentNodeViewModel>(GetComments, AddComments, LoadCommentFailed);
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

        public bool HasComments
        {
            get { return Items.Any(); }
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

            NotifyOfPropertyChanged(() => HasComments);
        }

        protected virtual Task<IResponse> GetComments(string next)
        {
            return null;
        }

        private void LoadCommentFailed(Exception e)
        {

        }
    }
}
