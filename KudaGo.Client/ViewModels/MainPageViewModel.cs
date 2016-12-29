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
        private readonly DataSource _dataSource;

        public MainPageViewModel()
        {
            _dataSource = new DataSource();
            _newsViewModel = new NewsViewModel(_dataSource);
            _eventsViewModel = new EventsViewModel(_dataSource);
        }

        public NewsViewModel NewsViewModel
        {
            get { return _newsViewModel; }
        }

        public EventsViewModel EventsViewModel
        {
            get { return _eventsViewModel; }
        }
    }
}
