using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels.Details
{
    public class DetailsPageViewModel : PropertyChangedBase
    {
        protected readonly DataSource _dataSource;
        protected ObservableCollection<string> _images;
        private string _title;
        private string _bodyText;
        private bool _isBusy;
        private string _description;

        public DetailsPageViewModel(long id, DataSource dataSource)
        {
            _dataSource = dataSource;
            _images = new ObservableCollection<string>();
            IsBusy = true;
            Task.Run(async () =>
            {
                await LoadDetails(id);
                LayoutHelper.InvokeFromUiThread(() => IsBusy = false);
            });
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

        protected virtual Task LoadDetails(long id)
        {
            return null;
        }
    }
}
