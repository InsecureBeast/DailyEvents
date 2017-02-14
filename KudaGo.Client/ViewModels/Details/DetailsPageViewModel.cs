using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KudaGo.Client.ViewModels.Comments;

namespace KudaGo.Client.ViewModels.Details
{
    class DetailsPageViewModel : PropertyChangedBase
    {
        protected readonly IDataSource _dataSource;
        private readonly NavigationViewModel _navigationViewModel;
        private readonly CommentsViewModel _commentsViewModel;
        protected ObservableCollection<string> _images;
        protected readonly long _id;
        private string _title;
        private string _bodyText;
        private bool _isBusy;
        private string _description;

        public DetailsPageViewModel(long id, IDataSource dataSource)
        {
            _id = id;
            _dataSource = dataSource;
            _images = new ObservableCollection<string>();
            _navigationViewModel = new NavigationViewModel(dataSource);
            _commentsViewModel = CreateCommentsViewModel();
            IsBusy = true;
            Task.Run(async () =>
            {
                await LoadDetails(id);
                LayoutHelper.InvokeFromUiThread(() => IsBusy = false);
            });
        }

        protected virtual CommentsViewModel CreateCommentsViewModel()
        {
            return new CommentsViewModel(_id, _dataSource);
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

        public string Title
        {
            get { return _title; }
            protected set
            {
                _title = value;
                NotifyOfPropertyChanged(() => Title);
            }
        }

        public string BodyText
        {
            get { return _bodyText; }
            protected set
            {
                _bodyText = value;
                NotifyOfPropertyChanged(() => BodyText);
            }
        }

        public string Description
        {
            get { return _description; }
            protected set
            {
                _description = value;
                NotifyOfPropertyChanged(() => Description);
            }
        }

        public ObservableCollection<string> Images
        {
            get { return _images; }
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
        }

        public CommentsViewModel CommentsViewModel
        {
            get { return _commentsViewModel; }
        }

        protected virtual async Task LoadDetails(long id)
        {
        }
    }
}
