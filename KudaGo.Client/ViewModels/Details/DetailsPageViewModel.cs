using DailyEvents.Client.Helpers;
using DailyEvents.Client.Model;
using DailyEvents.Client.ViewModels.Nodes;
using DailyEvents.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Client.ViewModels.Comments;
using System.Net.NetworkInformation;
using System.Net.Http;
using System.Net;
using DailyEvents.Client.Command;
using System.Windows.Input;
using DailyEvents.Client.Controls;

namespace DailyEvents.Client.ViewModels.Details
{
    class DetailsPageViewModel : PropertyChangedBase, ITitleProvider
    {
        protected readonly IDataSource _dataSource;
        private readonly CommentsViewModel _commentsViewModel;
        private readonly DelegateCommand _repeatCommand;
        protected ObservableCollection<string> _images;
        protected readonly long _id;
        private string _title;
        private string _bodyText;
        private bool _isBusy;
        private string _description;
        private Uri _source;
        private bool _isNotFound = false;

        public DetailsPageViewModel(long id, string title, IDataSource dataSource)
        {
            _id = id;
            _dataSource = dataSource;
            _images = new ObservableCollection<string>();
            _commentsViewModel = CreateCommentsViewModel();
            _repeatCommand = new DelegateCommand(Repeat);

            Title = title;
            IsBusy = true;
            Task.Run(async () =>
            {
                await Load();
            });
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                NotifyOfPropertyChanged(() => IsBusy);
                NotifyOfPropertyChanged(() => IsLoading);
            }
        }

        public bool IsLoading
        {
            get { return !_isBusy && IsConnected; }
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
                //NotifyOfPropertyChanged(() => BodyText);
            }
        }

        public string Description
        {
            get { return _description; }
            protected set
            {
                _description = value;
                //NotifyOfPropertyChanged(() => Description);
            }
        }

        public bool IsNotFound
        {
            get { return _isNotFound; }
            protected set
            {
                _isNotFound = value;
                NotifyOfPropertyChanged(() => IsNotFound);
            }
        }

        public Uri Source
        {
            get { return _source; }
            protected set
            {
                _source = value;
                //NotifyOfPropertyChanged(() => Source);
            }
        }

        public bool IsConnected
        {
            get
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
        }

        public ObservableCollection<string> Images
        {
            get { return _images; }
        }

        public CommentsViewModel CommentsViewModel
        {
            get { return _commentsViewModel; }
        }

        public ICommand RepeatCommand
        {
            get { return _repeatCommand; }
        }

        protected virtual CommentsViewModel CreateCommentsViewModel()
        {
            return new CommentsViewModel(_id, _dataSource);
        }

        protected virtual async Task LoadDetails(long id)
        {
        }

        private void UpdateFields()
        {
            LayoutHelper.InvokeFromUiThread(() =>
            {
                NotifyOfPropertyChanged(() => IsConnected);
                IsBusy = false;
            });
        }

        private async Task Load()
        {
            try
            {
                await LoadDetails(_id);
                UpdateFields();
            }
            catch (HttpRequestException)
            {
                UpdateFields();
            }
            catch (WebException)
            {
                UpdateFields();
            }
            catch (DailyEventsNotFoundException)
            {
                LayoutHelper.InvokeFromUiThread(() =>
                {
                    IsNotFound = true;
                });
                UpdateFields();
            }
        }

        private async void Repeat(object obj)
        {
            await Load();
        }
    }
}
