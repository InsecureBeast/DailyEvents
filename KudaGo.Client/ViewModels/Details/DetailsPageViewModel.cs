using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels.Details
{
    public class DetailsPageViewModel : PropertyChangedBase
    {
        protected readonly DataSource _dataSource;
        private string _title;
        private string _bodyText;
        private bool _isBusy;

        public DetailsPageViewModel(long id, DataSource dataSource)
        {
            _dataSource = dataSource;
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

        protected virtual Task LoadDetails(long id)
        {
            return null;
        }
    }
}
