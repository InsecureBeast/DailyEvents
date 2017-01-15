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
        private readonly IPlace _place;

        public MapPageViewModel(IPlace place)
        {
            _place = place;
        }

        public ICoordinates Location
        {
            get { return _place.Coords; }
        }

        public string Title
        {
            get { return _place.Title; }
        }
    }
}
