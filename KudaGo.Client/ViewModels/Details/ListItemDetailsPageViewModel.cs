using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyEvents.Client.Model;
using System.Collections.ObjectModel;
using DailyEvents.Client.Extensions;
using System.Net.NetworkInformation;

namespace DailyEvents.Client.ViewModels.Details
{
    class ListItemDetailsPageViewModel : PropertyChangedBase
    {
        private string _title;
        private string _bodyText;
        private Uri _source;
        private readonly NavigationViewModel _navigationViewModel;
        private ObservableCollection<string> _images = new ObservableCollection<string>();

        public ListItemDetailsPageViewModel(SelectionDetailsNodeViewModel node, IDataSource dataSource)
        {
            Title = node.Title.GetNormalString();
            BodyText = node.Description.GetNormalString();
            if (!string.IsNullOrEmpty(node.Source))
                Source = new Uri(node.Source);
            _images.Add(node.Image);
            _navigationViewModel = new NavigationViewModel(dataSource);
        }

        public ObservableCollection<string> Images
        {
            get { return _images; }
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

        public bool IsBusy
        {
            get { return false; }
        }

        public Uri Source
        {
            get { return _source; }
            protected set
            {
                _source = value;
                NotifyOfPropertyChanged(() => Source);
            }
        }

        public bool IsConnected
        {
            get
            {
                return NetworkInterface.GetIsNetworkAvailable();
            }
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
        }
    }
}
