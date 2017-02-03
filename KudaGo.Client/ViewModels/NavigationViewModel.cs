using KudaGo.Client.Command;
using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KudaGo.Client.ViewModels
{
    class NavigationViewModel : PropertyChangedBase
    {
        private readonly IDataSource _dataSource;
        private readonly DelegateCommand _searchCommand;
        private readonly DelegateCommand _settingsCommand;
        private bool _inSearchMode = false;
        private string _searchString;

        public NavigationViewModel(IDataSource dataSource)
        {
            _dataSource = dataSource;
            _searchCommand = new DelegateCommand(Search);
            _settingsCommand = new DelegateCommand(Settings);
        }

        public ICommand SearchCommand
        {
            get { return _searchCommand; }
        }

        public ICommand SettingsCommand
        {
            get { return _settingsCommand; }
        }

        public bool InSearchMode
        {
            get { return _inSearchMode; }
            set
            {
                _inSearchMode = value;
                NotifyOfPropertyChanged(() => InSearchMode);
            }
        }
        
        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                NotifyOfPropertyChanged(() => SearchString);
            }
        }


        private void Search(object obj)
        {
            if (InSearchMode)
            {
                if (string.IsNullOrEmpty(SearchString))
                    return;

                NavigationHelper.NavigateTo(typeof(SearchPage), new SearchPageViewModel(_dataSource, SearchString));
                return;
            }

            InSearchMode = true;
        }

        private void Settings(object obj)
        {
            NavigationHelper.NavigateToSettings();
        }
    }
}
