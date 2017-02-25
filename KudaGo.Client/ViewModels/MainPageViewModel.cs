using DailyEvents.Client.Command;
using DailyEvents.Client.Common;
using DailyEvents.Client.Helpers;
using DailyEvents.Client.Model;
using DailyEvents.Client.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DailyEvents.Client.ViewModels
{
    internal class MainPageViewModel : PropertyChangedBase, ISettingsChangeListener, ITitleProvider
    {
        private NewsViewModel _newsViewModel;
        private EventsViewModel _eventsViewModel;
        private SelectionsViewModel _selectionsViewModel;
        private MoviesViewModel _moviesViewModel;
        private readonly CategoryPageViewModel _categoryPageViewModel;
        private readonly DelegateCommand _eventFilterCommand;
        private readonly IDataSource _dataSource;
        private string _city;
        private readonly ISettingsProvider _settings;

        public MainPageViewModel()
        {
            _dataSource = App.DataSource;
            _settings = App.SettingsProvider;
            var notifier = App.SettingsNotifier;
            notifier.Subscribe(this);

            _newsViewModel = new NewsViewModel(_dataSource);
            _eventsViewModel = new EventsViewModel(_dataSource);
            _categoryPageViewModel = new CategoryPageViewModel(_dataSource, _eventsViewModel);
            _eventsViewModel.SetCategoryNameProvider(_categoryPageViewModel);

            _selectionsViewModel = new SelectionsViewModel(_dataSource);
            _moviesViewModel = new MoviesViewModel(_dataSource);
            _eventFilterCommand = new DelegateCommand(Filter);

            UpdateCity();
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

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                NotifyOfPropertyChanged(() => City);
            }
        }

        public string Title
        {
            get
            {
                return ResourcesHelper.GetLocalizationString("MainTitle");
            }
        }

        public async void UpdateSettings()
        {
            NewsViewModel = new NewsViewModel(_dataSource);
            MoviesViewModel = new MoviesViewModel(_dataSource);
            SelectionsViewModel = new SelectionsViewModel(_dataSource);

            UpdateCity();

            await _eventsViewModel.Update();
        }

        private void Filter(object obj)
        {
            NavigationHelper.NavigateTo(typeof(CategoryPage), _categoryPageViewModel);
        }

        private void UpdateCity()
        {
            var location = _settings.GetLocation();
            City = LocationHelper.GetCity(location);
        }
    }
}
