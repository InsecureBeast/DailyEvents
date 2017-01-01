using KudaGo.Client.Controls;
using KudaGo.Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    internal class MainPageViewModel : PropertyChangedBase
    {
        private readonly NewsViewModel _newsViewModel;
        private readonly EventsViewModel _eventsViewModel;
        private readonly SelectionsViewModel _selectionsViewModel;
        private readonly MoviesViewModel _moviesViewModel;

        public MainPageViewModel()
        {
            var dataSource = App.DataSource;
            _newsViewModel = new NewsViewModel(dataSource);
            _eventsViewModel = new EventsViewModel(dataSource);
            _selectionsViewModel = new SelectionsViewModel(dataSource);
            _moviesViewModel = new MoviesViewModel(dataSource);
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
    }
}
