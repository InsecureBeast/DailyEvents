using DailyEvents.Client.Command;
using DailyEvents.Client.Helpers;
using DailyEvents.Client.Model;
using DailyEvents.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DailyEvents.Client.ViewModels
{
    class MapPageViewModel : PropertyChangedBase
    {
        private readonly NavigationViewModel _navigationViewModel;
        private long _id;
        private readonly DelegateCommand _mapCommand;

        public MapPageViewModel(ICoordinates coords, string title, string metro, long id, IDataSource dataSource)
        {
            _navigationViewModel = new NavigationViewModel(dataSource);
            _mapCommand = new DelegateCommand(PlaceOpen);

            Location = coords;
            Title = title;
            Metro = metro;
            _id = id;
        }

        public ICoordinates Location
        {
            get;
            private set;
        }

        public string Title
        {
            get;
            private set;
        }

        public string Metro
        {
            get;
            private set;
        }

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
        }

        public ICommand MapCommand
        {
            get { return _mapCommand; }
        }

        private void PlaceOpen(object obj)
        {
            NavigationHelper.NavigateTo(typeof(DetailsPage), _id);
        }
    }
}
