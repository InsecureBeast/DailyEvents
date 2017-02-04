using KudaGo.Client.Model;
using KudaGo.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KudaGo.Client.ViewModels
{
    class MapPageViewModel : PropertyChangedBase
    {
        private readonly NavigationViewModel _navigationViewModel;

        public MapPageViewModel(ICoordinates coords, string title, IDataSource dataSource)
        {
            _navigationViewModel = new NavigationViewModel(dataSource);

            Location = coords;
            Title = title;
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

        public NavigationViewModel NavigationViewModel
        {
            get { return _navigationViewModel; }
        }
    }
}
