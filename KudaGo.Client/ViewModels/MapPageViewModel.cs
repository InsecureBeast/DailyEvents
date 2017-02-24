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
using DailyEvents.Client.Common;

namespace DailyEvents.Client.ViewModels
{
    class MapPageViewModel : PropertyChangedBase
    {
        private long _id;
        private readonly DelegateCommand _mapCommand;

        public event EventHandler<TitleChangedEventArgs> TitleChanged;

        public MapPageViewModel(ICoordinates coords, string title, string metro, long id, IDataSource dataSource, INavigationProvider provider)
        {
            _mapCommand = new DelegateCommand(PlaceOpen);

            Location = coords;
            Title = title;
            Metro = metro;
            _id = id;
            provider.SetTitle(Title);
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
