using KudaGo.Client.Command;
using KudaGo.Client.Controls;
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
    internal class MainPageViewModel : PropertyChangedBase
    {
        private readonly NewsViewModel _newsViewModel;
        private readonly EventsViewModel _eventsViewModel;
        private readonly SelectionsViewModel _selectionsViewModel;
        private readonly MoviesViewModel _moviesViewModel;
        private readonly CategoryPageViewModel _categoryPageViewModel;
        private readonly DelegateCommand _eventFilterCommand;
        private readonly NavigationViewModel _navigationViewModel;

        public MainPageViewModel()
        {
            var dataSource = App.DataSource;
            _newsViewModel = new NewsViewModel(dataSource);
            _eventsViewModel = new EventsViewModel(dataSource);
            _categoryPageViewModel = new CategoryPageViewModel(dataSource, _eventsViewModel);
            _eventsViewModel.SetCategoryNameProvider(_categoryPageViewModel);

            _selectionsViewModel = new SelectionsViewModel(dataSource);
            _moviesViewModel = new MoviesViewModel(dataSource);

            _navigationViewModel = new NavigationViewModel(dataSource);
            
            _eventFilterCommand = new DelegateCommand(Filter);
        }

        public ICommand EventFilterCommand
        {
            get { return _eventFilterCommand; }
        }

        public NewsViewModel NewsViewModel
        {
            get { return _newsViewModel; }
        }

        public EventsViewModel EventsViewModel
        {
            get { return _eventsViewModel; }
        }

        public SelectionsViewModel SelectionsViewModel
        {
            get { return _selectionsViewModel; }
        }

        public MoviesViewModel MoviesViewModel
        {
            get { return _moviesViewModel; }
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
        }

        private void Filter(object obj)
        {
            NavigationHelper.NavigateTo(typeof(CategoryPage), _categoryPageViewModel);
        }
    }
}
