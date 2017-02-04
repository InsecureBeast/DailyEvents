using KudaGo.Client.Command;
using KudaGo.Client.Common;
using KudaGo.Client.Helpers;
using KudaGo.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KudaGo.Client.ViewModels
{
    internal class MainPageViewModel : PropertyChangedBase, ISettingsChangeListener
    {
        private NewsViewModel _newsViewModel;
        private EventsViewModel _eventsViewModel;
        private SelectionsViewModel _selectionsViewModel;
        private MoviesViewModel _moviesViewModel;
        private readonly CategoryPageViewModel _categoryPageViewModel;
        private readonly DelegateCommand _eventFilterCommand;
        private readonly NavigationViewModel _navigationViewModel;
        private readonly IDataSource _dataSource;

        public MainPageViewModel()
        {
            _dataSource = App.DataSource;
            var notifier = App.SettingsNotifier;
            notifier.Subscribe(this);

            _newsViewModel = new NewsViewModel(_dataSource);
            _eventsViewModel = new EventsViewModel(_dataSource);
            _categoryPageViewModel = new CategoryPageViewModel(_dataSource, _eventsViewModel);
            _eventsViewModel.SetCategoryNameProvider(_categoryPageViewModel);

            _selectionsViewModel = new SelectionsViewModel(_dataSource);
            _moviesViewModel = new MoviesViewModel(_dataSource);

            _navigationViewModel = new NavigationViewModel(_dataSource);
            
            _eventFilterCommand = new DelegateCommand(Filter);
        }

        public ICommand EventFilterCommand
        {
            get { return _eventFilterCommand; }
        }

        public NewsViewModel NewsViewModel
        {
            get { return _newsViewModel; }
            set
            {
                _newsViewModel = value;
                NotifyOfPropertyChanged(() => NewsViewModel);
            }
        }

        public EventsViewModel EventsViewModel
        {
            get { return _eventsViewModel; }
            set
            {
                _eventsViewModel = value;
                NotifyOfPropertyChanged(() => EventsViewModel);
            }
        }

        public SelectionsViewModel SelectionsViewModel
        {
            get { return _selectionsViewModel; }
            set
            {
                _selectionsViewModel = value;
                NotifyOfPropertyChanged(() => SelectionsViewModel);
            }
        }

        public MoviesViewModel MoviesViewModel
        {
            get { return _moviesViewModel; }
            set
            {
                _moviesViewModel = value;
                NotifyOfPropertyChanged(() => MoviesViewModel);
            }
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
        }

        private void Filter(object obj)
        {
            NavigationHelper.NavigateTo(typeof(CategoryPage), _categoryPageViewModel);
        }

        public void UpdateSettings()
        {
            EventsViewModel = new EventsViewModel(_dataSource);
            EventsViewModel.SetCategoryNameProvider(_categoryPageViewModel);

            NewsViewModel = new NewsViewModel(_dataSource);
            MoviesViewModel = new MoviesViewModel(_dataSource);
            SelectionsViewModel = new SelectionsViewModel(_dataSource);
        }
    }
}
