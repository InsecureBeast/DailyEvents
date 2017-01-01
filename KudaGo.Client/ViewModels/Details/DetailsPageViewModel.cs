using KudaGo.Client.Model;
using KudaGo.Client.ViewModels.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels.Details
{
    class DetailsPageViewModel : PropertyChangedBase
    {
        protected readonly DataSource _dataSource;
        private string _title;
        private string _bodyText;

        public DetailsPageViewModel(long id, DataSource dataSource)
        {
            _dataSource = dataSource;
            Task.Run(async () => await LoadDetails(id));
        }

        protected virtual Task LoadDetails(long id)
        {
            return null;
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
    }
}
